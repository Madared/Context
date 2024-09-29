using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Results.Commands;

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

    public IUndoableCommand Generate()
    {
        return new UndoableCommandWithInputWrapper<T>(_subscriber.Result, _command);
    }
}