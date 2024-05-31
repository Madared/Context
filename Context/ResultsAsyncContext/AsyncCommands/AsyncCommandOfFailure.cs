using ResultAndOption.Errors;
using ResultAndOption.Results;

namespace Context.ResultsAsyncContext.AsyncCommands;

internal sealed class AsyncCommandOfFailure : IAsyncCommand
{
    private readonly IError _error;

    public AsyncCommandOfFailure(IError error)
    {
        _error = error;
    }

    public Task<Result> Do() => Task.FromResult(Result.Fail(_error));

    public async Task Undo()
    {
    }
}