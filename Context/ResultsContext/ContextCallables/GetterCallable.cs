using Results;

namespace Context.ResultsContext.ContextCallables;

internal sealed class GetterCallable<TOut> : ICallable<TOut> where TOut : notnull
{
    private readonly Result<TOut> _result;

    public GetterCallable(Result<TOut> result)
    {
        _result = result;
    }

    public Result<TOut> Call()
    {
        return _result;
    }
}