using Context.ResultsAsyncContext.AsyncCommands;

namespace Context.ResultsAsyncContext.AsyncCommandGenerators;

public interface IAsyncCommandGenerator
{
    IAsyncCommand Generate();
}