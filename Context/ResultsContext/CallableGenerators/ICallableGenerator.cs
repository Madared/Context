using Context.ResultsContext.ContextCallables;

namespace Context.ResultsContext.CallableGenerators;

public interface ICallableGenerator
{
    ICallable Generate();
}

public interface ICallableGenerator<T> where T : notnull
{
    ICallable<T> Generate();
}