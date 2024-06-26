using Context.ResultsContext.ContextCallables;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Results;

namespace Context.ResultsContext.CallableGenerators;

internal class InputCallableGeneratorOfResult<TIn, TOut> : ICallableGenerator<TOut>
    where TIn : notnull where TOut : notnull
{
    private readonly Func<TIn, Result<TOut>> _func;
    private readonly ResultSubscriber<TIn> _subscriber;

    public InputCallableGeneratorOfResult(ResultSubscriber<TIn> subscriber, Func<TIn, Result<TOut>> func)
    {
        _subscriber = subscriber;
        _func = func;
    }

    public ICallable<TOut> Generate()
    {
        return new CallableOf<TIn, TOut>(_subscriber.Result, _func);
    }
}