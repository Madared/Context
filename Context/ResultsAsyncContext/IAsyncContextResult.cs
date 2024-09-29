using Context.ResultsAsyncContext.AsyncCallableGenerators;
using Context.ResultsAsyncContext.AsyncCommandGenerators;
using ResultAndOption.Results;

namespace Context.ResultsAsyncContext;

public interface IAsyncContextResult : IResult
{
    Task<IAsyncContextResult> Retry();
    Task Undo();
    Task<IAsyncContextResult<T>> Map<T>(IAsyncResultGetterGenerator<T> asyncResultGetterGenerator) where T : notnull;
    IAsyncContextResult Do(IAsyncCommandGenerator asyncCommandGenerator);
}

public interface IAsyncContextResult<T> : IAsyncContextResult, IResult<T> where T : notnull
{
    async Task<IAsyncContextResult> IAsyncContextResult.Retry() => await Retry();
    new Task<IAsyncContextResult<T>> Retry();
}