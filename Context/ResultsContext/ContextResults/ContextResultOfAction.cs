using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextCallables;
using Context.ResultsContext.ContextCommands;
using ResultAndOption.Errors;
using ResultAndOption.Options;
using ResultAndOption.Options.Extensions;
using ResultAndOption.Results;

namespace Context.ResultsContext.ContextResults;

internal class ContextResultOfAction : IContextResult
{
    private readonly ICommand _command;
    private readonly ICommandGenerator _commandGenerator;
    private readonly Option<IContextResult> _previousContext;
    private readonly Result _result;
    private bool _undone;
    private Result ActiveResult => _undone ? Result.Fail(new ContextHasBeenUndone()) : _result;

    public ContextResultOfAction(Option<IContextResult> previousContext, ICommand command,
        ICommandGenerator commandGenerator, Result result)
    {
        _previousContext = previousContext;
        _command = command;
        _result = result;
        _commandGenerator = commandGenerator;
    }

    public bool Succeeded => ActiveResult.Succeeded;
    public bool Failed => ActiveResult.Failed;
    public IError Error => ActiveResult.Error;

    public Result StripContext() => ActiveResult;

    public void Undo()
    {
        if (Succeeded) _command.Undo();
        if (_previousContext.IsSome()) _previousContext.Data.Undo();
        _undone = true;
    }

    public IContextResult Do(ICommandGenerator commandGenerator)
    {
        ICommand command = commandGenerator.Generate();
        return Failed
            ? new ContextResultOfAction(this.ToOption<IContextResult>(), command, commandGenerator, Result.Fail(Error))
            : new ContextResultOfAction(this.ToOption<IContextResult>(), command, commandGenerator, command.Call());
    }

    public IContextResult<TOut> Map<TOut>(ICallableGenerator<TOut> callableGenerator) where TOut : notnull
    {
        ICallable<TOut> callable = callableGenerator.Generate();
        return Failed
            ? new ContextResultOfCallable<TOut>(callable, this.ToOption<IContextResult>(), Result<TOut>.Fail(Error),
                callableGenerator, new ResultEmitter<TOut>())
            : new ContextResultOfCallable<TOut>(callable, this.ToOption<IContextResult>(), callable.Call(), callableGenerator,
                new ResultEmitter<TOut>());
    }

    public IContextResult Retry()
    {
        if (Succeeded) return this;
        if (_previousContext.IsNone())
            return new ContextResultOfAction(Option<IContextResult>.None(), _command, _commandGenerator, _command.Call());
        IContextResult retried = _previousContext.Data.Retry();
        ICommand command = _commandGenerator.Generate();
        return retried.Failed
            ? new ContextResultOfAction(_previousContext, command, _commandGenerator, Result.Fail(retried.Error))
            : new ContextResultOfAction(_previousContext, command, _commandGenerator, _command.Call());
    }
}