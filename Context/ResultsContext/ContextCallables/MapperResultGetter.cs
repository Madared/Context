using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextResults;
using ResultAndOption.Results;
using ResultAndOption.Results.Getters;
using ResultAndOption.Results.Mappers;

namespace Context.ResultsContext.ContextCallables;

internal sealed class MapperResultGetter<TIn, TOut> : IResultGetter<TOut> where TOut : notnull where TIn : notnull
{
    private readonly Result<TIn> _data;
    private readonly IMapper<TIn, TOut> _mapper;
    public MapperResultGetter(Result<TIn> data, IMapper<TIn, TOut> mapper)
    {
        _data = data;
        _mapper = mapper;
    }
    public Result<TOut> Get() => _data.Failed ? Result<TOut>.Fail(_data.Error) : _mapper.Map(_data.Data);
}

internal sealed class MapperResultGetterGenerator<TIn, TOut> : IResultGetterGenerator<TOut>
    where TIn : notnull where TOut : notnull
{
    private readonly ResultSubscriber<TIn> _resultSubscriber;
    private readonly IMapper<TIn, TOut> _mapper;

    public MapperResultGetterGenerator(ResultSubscriber<TIn> resultSubscriber, IMapper<TIn, TOut> mapper)
    {
        _resultSubscriber = resultSubscriber;
        _mapper = mapper;
    }
    public IResultGetter<TOut> Generate() => new MapperResultGetter<TIn, TOut>(_resultSubscriber.Result, _mapper);
} 