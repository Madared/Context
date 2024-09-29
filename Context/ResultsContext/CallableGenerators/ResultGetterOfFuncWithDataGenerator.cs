using Context.ResultsContext.ContextCallables;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.CallableGenerators;

internal class ResultGetterOfFuncWithDataGenerator<TIn, TOut> : IResultGetterGenerator<TOut> where TIn : notnull where TOut : notnull
{
    private readonly Func<TIn, TOut> _func;
    private readonly ResultSubscriber<TIn> _subscriber;

    public ResultGetterOfFuncWithDataGenerator(ResultSubscriber<TIn> subscriber, Func<TIn, TOut> func)
    {
        _subscriber = subscriber;
        _func = func;
    }

    public IResultGetter<TOut> Generate()
    {
        Result<TIn> result = _subscriber.Result;
        return result.Failed
            ? new FuncResultGetter<TOut>(() => Result<TOut>.Fail(result.Error))
            : new ResultGetterOfFuncWithData<TIn, TOut>(result.Data, _func);
    }
}