using Context.ResultsContext.ActionCallables;
using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextCallables;
using Context.ResultsContext.ContextCommands;
using ResultAndOption.Options;
using ResultAndOption.Results;
using ResultAndOption.Results.Getters;
using ICommandGenerator = Context.ResultsContext.ActionCallables.ICommandGenerator;
using SimpleResultCommandGenerator = Context.ResultsContext.CallableGenerators.SimpleResultCommandGenerator;

namespace Context.ResultsContext.ContextResults.ContextResultExtensions;

public static class Running
{
    public static IContextResult RunAndGetContext(this Func<Result> action)
    {
        IResultCommandGenerator doGenerator = new SimpleResultCommandGenerator(action);
        ICommandGenerator undoGenerator = new ActionCommandGenerator(Nothing.DoNothing);
        ContextCommands.ICommandGenerator commandGenerator = new CallableCommandGenerator(doGenerator, undoGenerator);
        IUndoableCommand undoableCommand = commandGenerator.Generate();
        return new ContextResultOfAction(
            Option<IContextResult>.None(),
            undoableCommand,
            commandGenerator,
            undoableCommand.Do());
    }

    public static IContextResult<TOut> RunAndGetContext<TOut>(this Func<Result<TOut>> function) where TOut : notnull
    {
        IResultGetterGenerator<TOut> resultGetterGenerator = new FuncResultGetterGenerator<TOut>(function);
        IResultGetter<TOut> getter = resultGetterGenerator.Generate();
        return new ContextResultOfCallable<TOut>(
            getter,
            Option<IContextResult>.None(),
            getter.Get(),
            resultGetterGenerator,
            new ResultEmitter<TOut>());
    }
}