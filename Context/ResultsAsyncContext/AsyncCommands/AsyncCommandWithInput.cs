using ResultAndOption.Results;

namespace Context.ResultsAsyncContext.AsyncCommands;

internal sealed class AsyncCommandWithInput<TIn> : IAsyncCommand where TIn : notnull
{
    private readonly TIn _data;
    private readonly Func<TIn, Task<Result>> _doer;
    private readonly Func<TIn, Task> _undoer;

    public AsyncCommandWithInput(TIn data, Func<TIn, Task<Result>> doer, Func<TIn, Task> undoer)
    {
        _data = data;
        _doer = doer;
        _undoer = undoer;
    }

    public Task<Result> Do() => _doer(_data);
    public Task Undo() => _undoer(_data);
}