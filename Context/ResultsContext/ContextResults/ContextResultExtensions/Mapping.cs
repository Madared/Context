using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextCallables;
using ResultAndOption.Results;

namespace Context.ResultsContext.ContextResults.ContextResultExtensions;

public static class Mapping
{
    public static IContextResult<TOut> Map<TOut>(this IContextResult context, Func<Result<TOut>> mapper)
        where TOut : notnull
    {
        IResultGetterGenerator<TOut> resultGetterGenerator = new FuncResultGetterGenerator<TOut>(mapper);
        return context.Map(resultGetterGenerator);
    }

    public static IContextResult<TOut> Map<TIn, TOut>(this IContextResult<TIn> context, Func<TIn, Result<TOut>> mapper)
        where TIn : notnull where TOut : notnull
    {
        ResultSubscriber<TIn> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        IResultGetterGenerator<TOut> resultGetterGenerator = new GetterOfGenerator<TIn, TOut>(subscriber, mapper);
        return context.Map(resultGetterGenerator);
    }

    public static IContextResult<TOut> Map<TIn, TOut>(this IContextResult<TIn> context, Func<TIn, TOut> mapper)
        where TIn : notnull where TOut : notnull
    {
        ResultSubscriber<TIn> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        IResultGetterGenerator<TOut> resultGetterGenerator = new ResultGetterOfFuncWithDataGenerator<TIn, TOut>(subscriber, mapper);
        return context.Map(resultGetterGenerator);
    }

    public static IContextResult<TOut> Map<TOut>(this IContextResult context, Func<TOut> mapper) where TOut : notnull
    {
        IResultGetterGenerator<TOut> resultGetterGenerator = new FuncResultGetterGenerator<TOut>(mapper.WrapInResult());
        return context.Map(resultGetterGenerator);
    }
}