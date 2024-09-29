using Context.ResultsContext.ActionCallables;
using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextCallables;
using Context.ResultsContext.ContextCommands;
using ResultAndOption.Results;
using ICommandGenerator = Context.ResultsContext.ActionCallables.ICommandGenerator;
using SimpleResultCommandGenerator = Context.ResultsContext.CallableGenerators.SimpleResultCommandGenerator;

namespace Context.ResultsContext.ContextResults.ContextResultExtensions;

// Doing must be revamped to use commands only
public static class Doing
{
    public static IContextResult Do(this IContextResult context, Action action)
    {
        IResultCommandGenerator doGenerator = new SimpleResultCommandGenerator(action.WrapInResult());
        ICommandGenerator undoGenerator = new ActionCommandGenerator(Nothing.DoNothing);
        ContextCommands.ICommandGenerator commandGenerator = new CallableCommandGenerator(doGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult Do(this IContextResult context, Func<Result> action)
    {
        IResultCommandGenerator doGenerator = new SimpleResultCommandGenerator(action);
        ICommandGenerator undoGenerator = new ActionCommandGenerator(Nothing.DoNothing);
        ContextCommands.ICommandGenerator commandGenerator = new CallableCommandGenerator(doGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult<T> Do<T>(this IContextResult<T> context, Action action) where T : notnull
    {
        IResultCommandGenerator resultCommandGenerator = new SimpleResultCommandGenerator(action.WrapInResult());
        ICommandGenerator undoGenerator = new ActionCommandGenerator(Nothing.DoNothing);
        ContextCommands.ICommandGenerator commandGenerator = new CallableCommandGenerator(resultCommandGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult<T> Do<T>(this IContextResult<T> context, Func<Result> action) where T : notnull
    {
        IResultCommandGenerator resultCommandGenerator = new SimpleResultCommandGenerator(action);
        ICommandGenerator undoGenerator = new ActionCommandGenerator(Nothing.DoNothing);
        ContextCommands.ICommandGenerator commandGenerator = new CallableCommandGenerator(resultCommandGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult<T> Do<T>(this IContextResult<T> context, Func<T, Result> action) where T : notnull
    {
        ResultSubscriber<T> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        IResultCommandGenerator resultCommandGenerator = new SubscriberDependentActionResultCommandGenerator<T>(action, subscriber);
        ICommandGenerator undoGenerator = new ActionCommandGenerator(Nothing.DoNothing);
        ContextCommands.ICommandGenerator commandGenerator = new CallableCommandGenerator(resultCommandGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult<T> Do<T>(this IContextResult<T> context, Action<T> action) where T : notnull
    {
        ResultSubscriber<T> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        Func<T, Result> actionResult = action.WrapInResult();
        IResultCommandGenerator resultCommandGenerator = new SubscriberDependentActionResultCommandGenerator<T>(actionResult, subscriber);
        ICommandGenerator undoGenerator = new ActionCommandGenerator(Nothing.DoNothing);
        ContextCommands.ICommandGenerator commandGenerator = new CallableCommandGenerator(resultCommandGenerator, undoGenerator);
        return context.Do(commandGenerator);
    }

    public static IContextResult Do(this IContextResult context, IUndoableCommand undoableCommand)
    {
        ContextCommands.ICommandGenerator callableGenerator = new CommandWrapper(undoableCommand);
        return context.Do(callableGenerator);
    }

    public static IContextResult<TOut> Do<TOut>(this IContextResult<TOut> context, IUndoableCommand undoableCommand)
        where TOut : notnull
    {
        ContextCommands.ICommandGenerator commandGenerator = new CommandWrapper(undoableCommand);
        return context.Do(commandGenerator);
    }

    public static IContextResult<TOut> Do<TOut>(
        this IContextResult<TOut> context,
        ICommandWithInput<TOut> commandWithInput) where TOut : notnull
    {
        ResultSubscriber<TOut> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        ContextCommands.ICommandGenerator commandGenerator = new CommandWithInputWrapperGenerator<TOut>(subscriber, commandWithInput);
        return context.Do(commandGenerator);
    }

    public static IContextResult<TOut> Do<TOut>(
        this IContextResult<TOut> context,
        ICommandWithCallInput<TOut> commandWithCallInput) where TOut : notnull
    {
        ResultSubscriber<TOut> subscriber = new(context.StripContext());
        context.Emitter.Subscribe(subscriber);
        ContextCommands.ICommandGenerator commandGenerator =
            new CommandWithCallInputWrapperGenerator<TOut>(subscriber, commandWithCallInput);
        return context.Do(commandGenerator);
    }
    
}