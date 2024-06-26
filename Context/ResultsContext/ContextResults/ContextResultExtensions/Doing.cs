using Context.ResultsContext.ActionCallables;
using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextCallables;
using Context.ResultsContext.ContextCommands;
using ResultAndOption.Results;

namespace Context.ResultsContext.ContextResults.ContextResultExtensions;

public static class Doing
{
    public static IContextResult Do(this IContextResult context, Action action)
    {
        ICallableGenerator doGenerator = new SimpleCallableGenerator(action.WrapInResult());
        IActionCallableGenerator undoGenerator = new ActionCallableGenerator(Nothing.DoNothing);
        ICommandGenerator commandGenerator = new CallableCommandGenerator(doGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult Do(this IContextResult context, Func<Result> action)
    {
        ICallableGenerator doGenerator = new SimpleCallableGenerator(action);
        IActionCallableGenerator undoGenerator = new ActionCallableGenerator(Nothing.DoNothing);
        ICommandGenerator commandGenerator = new CallableCommandGenerator(doGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult<T> Do<T>(this IContextResult<T> context, Action action) where T : notnull
    {
        ICallableGenerator callableGenerator = new SimpleCallableGenerator(action.WrapInResult());
        IActionCallableGenerator undoGenerator = new ActionCallableGenerator(Nothing.DoNothing);
        ICommandGenerator commandGenerator = new CallableCommandGenerator(callableGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult<T> Do<T>(this IContextResult<T> context, Func<Result> action) where T : notnull
    {
        ICallableGenerator callableGenerator = new SimpleCallableGenerator(action);
        IActionCallableGenerator undoGenerator = new ActionCallableGenerator(Nothing.DoNothing);
        ICommandGenerator commandGenerator = new CallableCommandGenerator(callableGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult<T> Do<T>(this IContextResult<T> context, Func<T, Result> action) where T : notnull
    {
        ResultSubscriber<T> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        ICallableGenerator callableGenerator = new CallableGeneratorWithSimpleOutput<T>(action, subscriber);
        IActionCallableGenerator undoGenerator = new ActionCallableGenerator(Nothing.DoNothing);
        ICommandGenerator commandGenerator = new CallableCommandGenerator(callableGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult<T> Do<T>(this IContextResult<T> context, Action<T> action) where T : notnull
    {
        ResultSubscriber<T> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        ICallableGenerator callableGenerator =
            new CallableGeneratorWithSimpleOutput<T>(action.WrapInResult(), subscriber);
        IActionCallableGenerator undoGenerator = new ActionCallableGenerator(Nothing.DoNothing);
        ICommandGenerator commandGenerator = new CallableCommandGenerator(callableGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult Do(this IContextResult context, ICommand command)
    {
        ICommandGenerator callableGenerator = new CommandWrapper(command);
        return context.Do(callableGenerator);
    }

    public static IContextResult<TOut> Do<TOut>(this IContextResult<TOut> context, ICommand command)
        where TOut : notnull
    {
        ICommandGenerator commandGenerator = new CommandWrapper(command);
        return context.Do(commandGenerator);
    }

    public static IContextResult<TOut> Do<TOut>(this IContextResult<TOut> context,
        ICommandWithInput<TOut> commandWithInput) where TOut : notnull
    {
        ResultSubscriber<TOut> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        ICommandGenerator commandGenerator = new CommandWithInputWrapperGenerator<TOut>(subscriber, commandWithInput);
        return context.Do(commandGenerator);
    }

    public static IContextResult<TOut> Do<TOut>(this IContextResult<TOut> context,
        ICommandWithCallInput<TOut> commandWithCallInput) where TOut : notnull
    {
        ResultSubscriber<TOut> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        ICommandGenerator commandGenerator =
            new CommandWithCallInputWrapperGenerator<TOut>(subscriber, commandWithCallInput);
        return context.Do(commandGenerator);
    }
}