
using Context.ResultsContext.CallableGenerators;
using ResultAndOption.Results;
using ResultAndOption.Results.Getters;

namespace Context.ResultsContext.ContextCallables;

internal sealed class FuncResultGetter<TOut> : IResultGetter<TOut> where TOut : notnull
{
    private readonly Func<Result<TOut>> _func;

    public FuncResultGetter(Func<Result<TOut>> func)
    {
        _func = func;
    }
    public Result<TOut> Get() => _func();
}

internal sealed class FuncResultGetterGenerator<TOut> : IResultGetterGenerator<TOut> where TOut : notnull
{
    private readonly Func<Result<TOut>> _func;

    public FuncResultGetterGenerator(Func<Result<TOut>> func)
    {
        _func = func;
    }

    public IResultGetter<TOut> Generate() => new FuncResultGetter<TOut>(_func);
}