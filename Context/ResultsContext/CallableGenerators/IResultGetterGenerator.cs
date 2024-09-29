using Context.ResultsContext.ContextCallables;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.CallableGenerators;

public interface IResultGetterGenerator<T> where T : notnull
{
    IResultGetter<T> Generate();
}