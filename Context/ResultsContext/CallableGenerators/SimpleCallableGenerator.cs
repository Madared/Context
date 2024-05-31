using Context.ResultsContext.ContextCallables;
using ResultAndOption.Results;

namespace Context.ResultsContext.CallableGenerators;

internal sealed class SimpleCallableGenerator : ICallableGenerator
{
    private readonly Func<Result> _action;

    public SimpleCallableGenerator(Func<Result> action)
    {
        _action = action;
    }

    public ICallable Generate()
    {
        return new NoInputSimpleCallable(_action);
    }
}