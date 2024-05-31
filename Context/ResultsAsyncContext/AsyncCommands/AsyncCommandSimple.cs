using ResultAndOption.Results;

namespace Context.ResultsAsyncContext.AsyncCommands;

internal sealed class AsyncCommandSimple : IAsyncCommand
{
    private readonly Func<Task<Result>> _doer;
    private readonly Func<Task> _undoer;

    public AsyncCommandSimple(Func<Task<Result>> doer, Func<Task> undoer)
    {
        _doer = doer;
        _undoer = undoer;
    }

    public Task<Result> Do() => _doer();
    public Task Undo() => _undoer();
}