using Context.ResultsContext.ContextCallables;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.CallableGenerators;

internal class GetterOfGenerator<TIn, TOut> : IResultGetterGenerator<TOut>
    where TIn : notnull where TOut : notnull
{
    private readonly Func<TIn, Result<TOut>> _func;
    private readonly ResultSubscriber<TIn> _subscriber;

    public GetterOfGenerator(ResultSubscriber<TIn> subscriber, Func<TIn, Result<TOut>> func)
    {
        _subscriber = subscriber;
        _func = func;
    }

    public IResultGetter<TOut> Generate() => new GetterOf<TIn, TOut>(_subscriber.Result, _func);
}