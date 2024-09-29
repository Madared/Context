using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsAsyncContext.AsyncCallables;

internal sealed class AsyncCallableOfValueWithInput<TIn, TOut> : IAsyncResultGetter<TOut>
    where TIn : notnull where TOut : notnull
{
    private readonly Func<TIn, Task<TOut>> _func;
    private readonly TIn _data;

    public AsyncCallableOfValueWithInput(Func<TIn, Task<TOut>> func, TIn data)
    {
        _func = func;
        _data = data;
    }

    public async Task<Result<TOut>> Get(CancellationToken? token) => Result<TOut>.Ok(await _func(_data));
}