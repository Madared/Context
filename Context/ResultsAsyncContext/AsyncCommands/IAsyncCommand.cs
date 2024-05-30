using Results;

namespace Context.ResultsAsyncContext.AsyncCommands;

public interface IAsyncCommand
{
    Task<Result> Do();
    Task Undo();
}