using Context.ResultsAsyncContext.AsyncCommands;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Results;

namespace Context.ResultsAsyncContext.AsyncCommandGenerators;

internal sealed class AsyncCommandGeneratorWithInput<TIn> : IAsyncCommandGenerator where TIn : notnull
{
    private readonly ResultSubscriber<TIn> _subscriber;
    private readonly Func<TIn, Task<Result>> _doer;
    private readonly Func<TIn, Task> _undoer;

    public AsyncCommandGeneratorWithInput(
        ResultSubscriber<TIn> subscriber,
        Func<TIn, Task<Result>> doer,
        Func<TIn, Task> undoer)
    {
        _subscriber = subscriber;
        _doer = doer;
        _undoer = undoer;
    }

    public IAsyncCommand Generate()
    {
        Result<TIn> result = _subscriber.Result;
        return result.Failed
            ? new AsyncCommandOfFailure(result.Error)
            : new AsyncCommandWithInput<TIn>(_subscriber.Result.Data, _doer, _undoer);
    }
}