using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Errors;
using ResultAndOption.Results;
using ResultAndOption.Results.GenericResultExtensions;
using ResultAndOption.Results.Getters;
using ResultAndOption.Transformers;

namespace Context.ResultsContext.ContextCallables;

internal sealed class ResultTransformerGetter<TIn, TOut> : IResultGetter<TOut> where TIn : notnull where TOut : notnull
{
    private readonly TIn _data;
    private readonly ITransformer<TIn, TOut> _transformer;

    public ResultTransformerGetter(TIn data, ITransformer<TIn, TOut> transformer)
    {
        _data = data;
        _transformer = transformer;
    }

    public Result<TOut> Get() => _transformer
        .Transform(_data)
        .ToResult(new UnknownError());
}

internal sealed class ResultTransformerResultGetterGenerator<TIn, TOut> : IResultGetterGenerator<TOut>
    where TIn : notnull where TOut : notnull
{
    private readonly DataSubscriber<TIn> _subscriber;
    private readonly ITransformer<TIn, TOut> _transformer;

    public ResultTransformerResultGetterGenerator(DataSubscriber<TIn> subscriber, ITransformer<TIn, TOut> transformer)
    {
        _subscriber = subscriber;
        _transformer = transformer;
    }

    public IResultGetter<TOut> Generate() => new ResultTransformerGetter<TIn, TOut>(_subscriber.Data, _transformer);
}