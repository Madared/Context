using Context.ResultsContext.ContextResults;
using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.ActionCallables;

internal sealed class CommandWithInputGenerator<TIn> : ICommandGenerator where TIn : notnull
{
    private readonly Action<TIn> _action;
    private readonly ResultSubscriber<TIn> _subscriber;

    public CommandWithInputGenerator(ResultSubscriber<TIn> subscriber, Action<TIn> action)
    {
        _subscriber = subscriber;
        _action = action;
    }

    public ICommand Generate() => new ActionCommandWithInput<TIn>(_subscriber.Result, _action);
}