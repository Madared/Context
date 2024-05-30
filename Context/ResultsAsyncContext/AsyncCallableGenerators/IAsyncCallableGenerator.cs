using Context.ResultsAsyncContext.AsyncCallables;

namespace Context.ResultsAsyncContext.AsyncCallableGenerators;

public interface IAsyncCallableGenerator<T> where T : notnull
{
    IAsyncCallable<T> Generate();
}