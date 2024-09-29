using Context.ResultsAsyncContext.AsyncCallables;
using ResultAndOption.Results.Getters;

namespace Context.ResultsAsyncContext.AsyncCallableGenerators;

public interface IAsyncResultGetterGenerator<T> where T : notnull
{
    IAsyncResultGetter<T> Generate();
}