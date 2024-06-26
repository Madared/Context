using Context.ResultsContext.ContextResults;

namespace Context.ResultsContext.ContextCommands;

internal sealed class CommandWithInputWrapperGenerator<T> : ICommandGenerator where T : notnull
{
    private readonly ICommandWithInput<T> _command;
    private readonly ResultSubscriber<T> _subscriber;

    public CommandWithInputWrapperGenerator(ResultSubscriber<T> subscriber, ICommandWithInput<T> command)
    {
        _subscriber = subscriber;
        _command = command;
    }

    public ICommand Generate()
    {
        return new CommandWithInputWrapper<T>(_subscriber.Result, _command);
    }
}