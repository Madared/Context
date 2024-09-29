using ResultAndOption.Results;
using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.ContextCallables;

internal sealed class FuncAndDataResultCommandGenerator<TIn> : IResultCommand where TIn : notnull
{
    private readonly TIn _data;
    private readonly Func<TIn, Result> _func;

    public FuncAndDataResultCommandGenerator(TIn data, Func<TIn, Result> func)
    {
        _data = data;
        _func = func;
    }

    public Result Do() => _func(_data);
}