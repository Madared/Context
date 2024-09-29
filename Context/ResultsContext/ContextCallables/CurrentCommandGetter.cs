
using ResultAndOption.Results;
using ResultAndOption.Results.Commands;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.ContextCallables;

internal sealed class CurrentCommandGetter<TOut> : IResultGetter<TOut> where TOut : notnull
{
    private readonly IResultCommand _callable;
    private readonly Result<TOut> _result;

    public CurrentCommandGetter(Result<TOut> result, IResultCommand callable)
    {
        _result = result;
        _callable = callable;
    }

    public Result<TOut> Get()
    {
        Result called = _callable.Do();
        return called.Failed ? Result<TOut>.Fail(called.Error) : _result;
    }
}