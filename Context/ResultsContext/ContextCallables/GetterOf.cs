using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.ContextCallables;

internal class GetterOf<TIn, TOut> : IResultGetter<TOut> where TOut : notnull where TIn : notnull
{
    private readonly Func<TIn, Result<TOut>> _func;
    private readonly Result<TIn> _result;

    public GetterOf(Result<TIn> result, Func<TIn, Result<TOut>> func)
    {
        _result = result;
        _func = func;
    }

    public Result<TOut> Get() => _result.Failed
        ? Result<TOut>.Fail(_result.Error)
        : _func(_result.Data);
}