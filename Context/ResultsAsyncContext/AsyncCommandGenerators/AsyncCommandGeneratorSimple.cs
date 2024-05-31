using Context.ResultsAsyncContext.AsyncCommands;
using ResultAndOption.Results;

namespace Context.ResultsAsyncContext.AsyncCommandGenerators;

internal sealed class AsyncCommandGeneratorSimple : IAsyncCommandGenerator
{
    private readonly Func<Task<Result>> _doer;
    private readonly Func<Task> _undoer;

    public AsyncCommandGeneratorSimple(Func<Task<Result>> doer, Func<Task> undoer)
    {
        _doer = doer;
        _undoer = undoer;
    }
    public IAsyncCommand Generate() => new AsyncCommandSimple(_doer, _undoer);
}