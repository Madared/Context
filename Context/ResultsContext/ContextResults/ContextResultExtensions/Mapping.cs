using Context.ResultsContext.CallableGenerators;
using ResultAndOption.Results;

namespace Context.ResultsContext.ContextResults.ContextResultExtensions;

public static class Mapping
{
    public static IContextResult<TOut> Map<TOut>(this IContextResult context, Func<Result<TOut>> mapper)
        where TOut : notnull
    {
        ICallableGenerator<TOut> callableGenerator = new CallableGeneratorWithSimpleInput<TOut>(mapper);
        return context.Map(callableGenerator);
    }

    public static IContextResult<TOut> Map<TIn, TOut>(this IContextResult<TIn> context, Func<TIn, Result<TOut>> mapper)
        where TIn : notnull where TOut : notnull
    {
        ResultSubscriber<TIn> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        ICallableGenerator<TOut> callableGenerator = new InputCallableGeneratorOfResult<TIn, TOut>(subscriber, mapper);
        return context.Map(callableGenerator);
    }

    public static IContextResult<TOut> Map<TIn, TOut>(this IContextResult<TIn> context, Func<TIn, TOut> mapper)
        where TIn : notnull where TOut : notnull
    {
        ResultSubscriber<TIn> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        ICallableGenerator<TOut> callableGenerator = new InputCallableGenerator<TIn, TOut>(subscriber, mapper);
        return context.Map(callableGenerator);
    }

    public static IContextResult<TOut> Map<TOut>(this IContextResult context, Func<TOut> mapper) where TOut : notnull
    {
        ICallableGenerator<TOut> callableGenerator = new CallableGeneratorWithSimpleInput<TOut>(mapper.WrapInResult());
        return context.Map(callableGenerator);
    }
}