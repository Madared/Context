using System.Runtime;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.CallableGenerators;

internal sealed class PassThroughResultGetterGenerator<TOut> : IResultGetterGenerator<TOut> where TOut : notnull
{
    private readonly IResultGetter<TOut> _resultGetter;

    public PassThroughResultGetterGenerator(IResultGetter<TOut> resultGetter)
    {
        _resultGetter = resultGetter;
    }
    public IResultGetter<TOut> Generate() => _resultGetter;
}