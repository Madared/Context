using Context.ResultsContext.ContextResults;

namespace Context.ResultsContext.ContextCommands;

internal sealed class CommandWithCallInputWrapperGenerator<T> : ICommandGenerator where T : notnull
{
    private readonly ICommandWithCallInput<T> _commandWithCallInput;
    private readonly ResultSubscriber<T> _subscriber;

    public CommandWithCallInputWrapperGenerator(ResultSubscriber<T> subscriber,
        ICommandWithCallInput<T> commandWithCallInput)
    {
        _subscriber = subscriber;
        _commandWithCallInput = commandWithCallInput;
    }

    public IUndoableCommand Generate()
    {
        return new UndoableCommandWithCallInputWrapper<T>(_commandWithCallInput, _subscriber.Result);
    }
}