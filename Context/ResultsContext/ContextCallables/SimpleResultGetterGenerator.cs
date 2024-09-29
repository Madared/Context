using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.ContextCallables;

internal sealed class SimpleResultGetterGenerator<TOut> : IResultGetterGenerator<TOut> where TOut : notnull
{
    private readonly ResultSubscriber<TOut> _resultSubscriber;

    public SimpleResultGetterGenerator(ResultSubscriber<TOut> resultSubscriber)
    {
        _resultSubscriber = resultSubscriber;
    }
    public IResultGetter<TOut> Generate() => new SimpleResultGetter<TOut>(_resultSubscriber.Result);
}