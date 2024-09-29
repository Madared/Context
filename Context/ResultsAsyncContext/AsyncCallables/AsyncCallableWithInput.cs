
using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsAsyncContext.AsyncCallables;

internal sealed class AsyncCallableWithInput<TIn, TOut> : IAsyncResultGetter<TOut>
    where TIn : notnull where TOut : notnull
{
    private readonly Func<TIn, Task<Result<TOut>>> _func;
    private readonly TIn _data;

    public AsyncCallableWithInput(Func<TIn, Task<Result<TOut>>> func, TIn data)
    {
        _func = func;
        _data = data;
    }

    public Task<Result<TOut>> Get(CancellationToken? token) => _func(_data);
}