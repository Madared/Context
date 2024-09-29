
using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsAsyncContext.AsyncCallables;

internal sealed class AsyncCallableOfValue<T> : IAsyncResultGetter<T> where T : notnull
{
    private readonly Func<Task<T>> _func;

    public AsyncCallableOfValue(Func<Task<T>> func)
    {
        _func = func;
    }

    public async Task<Result<T>> Get(CancellationToken? token) => Result<T>.Ok(await _func());
}