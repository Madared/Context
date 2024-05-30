using Results;

namespace Context.ResultsAsyncContext.AsyncCallables;

public interface IAsyncCallable<T> where T : notnull
{
    Task<Result<T>> Call();
}