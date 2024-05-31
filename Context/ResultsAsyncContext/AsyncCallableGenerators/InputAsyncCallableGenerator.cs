using Context.ResultsAsyncContext.AsyncCallables;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Results;

namespace Context.ResultsAsyncContext.AsyncCallableGenerators;

internal sealed class InputAsyncCallableGenerator<TIn, TOut> : IAsyncCallableGenerator<TOut>
    where TIn : notnull where TOut : notnull
{
    private readonly Func<TIn, Task<TOut>> _func;
    private readonly ResultSubscriber<TIn> _subscriber;

    public InputAsyncCallableGenerator(Func<TIn, Task<TOut>> func, ResultSubscriber<TIn> subscriber)
    {
        _func = func;
        _subscriber = subscriber;
    }

    public IAsyncCallable<TOut> Generate()
    {
        Result<TIn> result = _subscriber.Result;
        if (result.Failed)
        {
            return new SimpleAsyncCallable<TOut>(() => Task.FromResult(Result<TOut>.Fail(result.Error)));
        }

        return new AsyncCallableOfValueWithInput<TIn, TOut>(_func, result.Data);
    }
}