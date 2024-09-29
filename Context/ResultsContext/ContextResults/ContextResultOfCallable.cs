using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextCallables;
using Context.ResultsContext.ContextCommands;
using ResultAndOption.Errors;
using ResultAndOption.Options;
using ResultAndOption.Options.Extensions;
using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.ContextResults;

internal sealed class ContextResultOfCallable<TOut> : IContextResult<TOut> where TOut : notnull
{
    private readonly IResultGetterGenerator<TOut> _resultGetterGenerator;
    private readonly IResultGetter<TOut> _getter;
    private readonly Option<IContextResult> _previousContext;
    private readonly Result<TOut> _result;
    private bool _undone;
    private Result<TOut> ActiveResult => _undone ? Result<TOut>.Fail(new ContextHasBeenUndone()) : _result;

    public ContextResultOfCallable(
        IResultGetter<TOut> getter,
        Option<IContextResult> previousContext,
        Result<TOut> result,
        IResultGetterGenerator<TOut> resultGetterGenerator,
        ResultEmitter<TOut> emitter)
    {
        _getter = getter;
        _previousContext = previousContext;
        _result = result;
        Emitter = emitter;
        _resultGetterGenerator = resultGetterGenerator;
    }

    public ResultEmitter<TOut> Emitter { get; }
    public IError Error => ActiveResult.Error;
    public TOut Data => ActiveResult.Data;
    public bool Succeeded => ActiveResult.Succeeded;
    public bool Failed => ActiveResult.Failed;


    public Result<TOut> StripContext()
    {
        return _result;
    }

    public void Undo()
    {
        if (_previousContext.IsSome()) _previousContext.Data.Undo();
        _undone = true;
    }

    public IContextResult<TOut> Do(ICommandGenerator commandGenerator)
    {
        IUndoableCommand undoableCommand = commandGenerator.Generate();
        ResultSubscriber<TOut> subscriber = new(_result);
        Emitter.Subscribe(subscriber);
        IResultGetterGenerator<TOut> resultGetterGenerator = new SimpleResultGetterGenerator<TOut>(subscriber);
        IContextResult simpleContext = Failed
            ? new ContextResultOfAction(this.ToOption<IContextResult>(), undoableCommand, commandGenerator, Result.Fail(Error))
            : new ContextResultOfAction(this.ToOption<IContextResult>(), undoableCommand, commandGenerator, undoableCommand.Do());
        return simpleContext.Map(resultGetterGenerator);
    }

    public IContextResult<TNext> Map<TNext>(IResultGetterGenerator<TNext> resultGetterGenerator) where TNext : notnull
    {
        IResultGetter<TNext> callable = resultGetterGenerator.Generate();
        return Failed
            ? new ContextResultOfCallable<TNext>(callable, this.ToOption<IContextResult>(), Result<TNext>.Fail(Error),
                resultGetterGenerator, new ResultEmitter<TNext>())
            : new ContextResultOfCallable<TNext>(callable, this.ToOption<IContextResult>(), callable.Get(), resultGetterGenerator,
                new ResultEmitter<TNext>());
    }

    public IContextResult<TOut> Retry()
    {
        if (Succeeded) return this;
        if (_previousContext.IsNone())
            return new ContextResultOfCallable<TOut>(_getter, Option<IContextResult>.None(), _getter.Get(), _resultGetterGenerator,
                new ResultEmitter<TOut>());

        IContextResult retried = _previousContext.Data.Retry();
        IResultGetter<TOut> updatedCalled = _resultGetterGenerator.Generate();
        if (retried.Failed)
            return new ContextResultOfCallable<TOut>(updatedCalled, retried.ToOption(), Result<TOut>.Fail(retried.Error),
                _resultGetterGenerator, new ResultEmitter<TOut>());

        Result<TOut> updatedResult = updatedCalled.Get();
        Emitter.Emit(updatedResult);
        return new ContextResultOfCallable<TOut>(updatedCalled, retried.ToOption(), updatedResult, _resultGetterGenerator,
            new ResultEmitter<TOut>());
    }
}