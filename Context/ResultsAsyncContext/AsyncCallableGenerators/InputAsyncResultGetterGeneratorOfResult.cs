using Context.ResultsAsyncContext.AsyncCallables;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsAsyncContext.AsyncCallableGenerators;

internal sealed class InputAsyncResultGetterGeneratorOfResult<TIn, TOut> : IAsyncResultGetterGenerator<TOut>
    where TOut : notnull where TIn : notnull
{
    private readonly Func<TIn, Task<Result<TOut>>> _func;
    private readonly ResultSubscriber<TIn> _subscriber;

    public InputAsyncResultGetterGeneratorOfResult(Func<TIn, Task<Result<TOut>>> func, ResultSubscriber<TIn> subscriber)
    {
        _func = func;
        _subscriber = subscriber;
    }

    public IAsyncResultGetter<TOut> Generate()
    {
        Result<TIn> data = _subscriber.Result;
        if (data.Failed)
        {
            return new SimpleAsyncCallable<TOut>(() => Task.FromResult(Result<TOut>.Fail(data.Error)));
        }
        return new AsyncCallableWithInput<TIn, TOut>(_func, data.Data);
    }
}