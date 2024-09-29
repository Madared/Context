using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.ContextCallables;

internal sealed class SimpleResultGetter<TOut> : IResultGetter<TOut> where TOut : notnull
{
    private readonly Result<TOut> _result;

    public SimpleResultGetter(Result<TOut> result)
    {
        _result = result;
    }
    public Result<TOut> Get() => _result;
}