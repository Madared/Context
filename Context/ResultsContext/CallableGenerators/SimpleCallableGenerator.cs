using Context.ResultsContext.ContextCallables;
using ResultAndOption.Results;
using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.CallableGenerators;

internal sealed class SimpleResultCommandGenerator : IResultCommandGenerator
{
    private readonly Func<Result> _action;

    public SimpleResultCommandGenerator(Func<Result> action)
    {
        _action = action;
    }
    public IResultCommand Generate() => new ContextCallables.SimpleResultCommandGenerator(_action);
}