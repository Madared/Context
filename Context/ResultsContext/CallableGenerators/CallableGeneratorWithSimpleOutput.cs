using Context.ResultsContext.ContextCallables;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Results;

namespace Context.ResultsContext.CallableGenerators;

internal sealed class CallableGeneratorWithSimpleOutput<TIn> : ICallableGenerator where TIn : notnull
{
    private readonly Func<TIn, Result> _action;
    private readonly ResultSubscriber<TIn> _subscriber;

    public CallableGeneratorWithSimpleOutput(Func<TIn, Result> action, ResultSubscriber<TIn> subscriber)
    {
        _action = action;
        _subscriber = subscriber;
    }

    public ICallable Generate()
    {
        Result<TIn> result = _subscriber.Result;
        return result.Failed
            ? new NoInputSimpleCallable(() => Result.Fail(result.Error))
            : new NoOutputCallable<TIn>(result.Data, _action);
    }
}