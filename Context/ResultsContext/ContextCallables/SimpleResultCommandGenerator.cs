using ResultAndOption.Results;
using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.ContextCallables;

internal sealed class SimpleResultCommandGenerator : IResultCommand
{
    private readonly Func<Result> _func;

    public SimpleResultCommandGenerator(Func<Result> func)
    {
        _func = func;
    }

    public Result Do()
    {
        return _func();
    }
}