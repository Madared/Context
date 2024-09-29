using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsAsyncContext.AsyncCallables;

internal sealed class SimpleAsyncCallable<T> : IAsyncResultGetter<T> where T : notnull
{
    private readonly Func<Task<Result<T>>> _func;

    public SimpleAsyncCallable(Func<Task<Result<T>>> func)
    {
        _func = func;
    }

    public Task<Result<T>> Get(CancellationToken? token) => _func();
}