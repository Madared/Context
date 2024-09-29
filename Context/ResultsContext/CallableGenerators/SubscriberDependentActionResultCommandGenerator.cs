using Context.ResultsContext.ContextCallables;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Results;
using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.CallableGenerators;

internal sealed class SubscriberDependentActionResultCommandGenerator<TIn> : IResultCommandGenerator where TIn : notnull
{
    private readonly Func<TIn, Result> _action;
    private readonly ResultSubscriber<TIn> _subscriber;

    public SubscriberDependentActionResultCommandGenerator(Func<TIn, Result> action, ResultSubscriber<TIn> subscriber)
    {
        _action = action;
        _subscriber = subscriber;
    }

    public IResultCommand Generate()
    {
        Result<TIn> result = _subscriber.Result;
        return result.Failed
            ? new ContextCallables.SimpleResultCommandGenerator(() => Result.Fail(result.Error))
            : new FuncAndDataResultCommandGenerator<TIn>(result.Data, _action);
    }
}