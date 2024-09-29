using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.ContextCallables;

internal sealed class ResultGetterOfFuncWithData<TIn, TOut> : IResultGetter<TOut> where TIn : notnull where TOut : notnull
{
    private readonly TIn _data;
    private readonly Func<TIn, TOut> _func;

    public ResultGetterOfFuncWithData(TIn data, Func<TIn, TOut> func)
    {
        _data = data;
        _func = func;
    }

    public Result<TOut> Get() => Result<TOut>.Ok(_func(_data));
}