using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextCallables;
using Context.ResultsContext.ContextCommands;
using ResultAndOption.Errors;
using ResultAndOption.Options;
using ResultAndOption.Options.Extensions;
using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.ContextResults;

internal class ContextResultOfAction : IContextResult
{
    private readonly IUndoableCommand _undoableCommand;
    private readonly ICommandGenerator _commandGenerator;
    private readonly Option<IContextResult> _previousContext;
    private readonly Result _result;
    private bool _undone;
    private Result ActiveResult => _undone ? Result.Fail(new ContextHasBeenUndone()) : _result;

    public ContextResultOfAction(Option<IContextResult> previousContext, IUndoableCommand undoableCommand,
        ICommandGenerator commandGenerator, Result result)
    {
        _previousContext = previousContext;
        _undoableCommand = undoableCommand;
        _result = result;
        _commandGenerator = commandGenerator;
    }

    public bool Succeeded => ActiveResult.Succeeded;
    public bool Failed => ActiveResult.Failed;
    public IError Error => ActiveResult.Error;

    public Result StripContext() => ActiveResult;

    public void Undo()
    {
        if (Succeeded) _undoableCommand.Undo();
        if (_previousContext.IsSome()) _previousContext.Data.Undo();
        _undone = true;
    }

    public IContextResult Do(ICommandGenerator commandGenerator)
    {
        IUndoableCommand undoableCommand = commandGenerator.Generate();
        return Failed
            ? new ContextResultOfAction(this.ToOption<IContextResult>(), undoableCommand, commandGenerator, Result.Fail(Error))
            : new ContextResultOfAction(this.ToOption<IContextResult>(), undoableCommand, commandGenerator, undoableCommand.Do());
    }

    public IContextResult<TOut> Map<TOut>(IResultGetterGenerator<TOut> resultGetterGenerator) where TOut : notnull
    {
        IResultGetter<TOut> callable = resultGetterGenerator.Generate();
        return Failed
            ? new ContextResultOfCallable<TOut>(callable, this.ToOption<IContextResult>(), Result<TOut>.Fail(Error),
                resultGetterGenerator, new ResultEmitter<TOut>())
            : new ContextResultOfCallable<TOut>(callable, this.ToOption<IContextResult>(), callable.Get(), resultGetterGenerator,
                new ResultEmitter<TOut>());
    }

    public IContextResult Retry()
    {
        if (Succeeded) return this;
        if (_previousContext.IsNone())
            return new ContextResultOfAction(Option<IContextResult>.None(), _undoableCommand, _commandGenerator, _undoableCommand.Do());
        IContextResult retried = _previousContext.Data.Retry();
        IUndoableCommand undoableCommand = _commandGenerator.Generate();
        return retried.Failed
            ? new ContextResultOfAction(_previousContext, undoableCommand, _commandGenerator, Result.Fail(retried.Error))
            : new ContextResultOfAction(_previousContext, undoableCommand, _commandGenerator, _undoableCommand.Do());
    }
}