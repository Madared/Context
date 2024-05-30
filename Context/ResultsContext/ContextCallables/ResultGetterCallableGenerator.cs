using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextResults;

namespace Context.ResultsContext.ContextCallables;

internal sealed class ResultGetterCallableGenerator<TOut> : ICallableGenerator<TOut> where TOut : notnull
{
    private readonly ResultSubscriber<TOut> _resultSubscriber;

    public ResultGetterCallableGenerator(ResultSubscriber<TOut> resultSubscriber)
    {
        _resultSubscriber = resultSubscriber;
    }

    public ICallable<TOut> Generate()
    {
        return new GetterCallable<TOut>(_resultSubscriber.Result);
    }
}