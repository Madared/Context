using Context.ResultsContext.ContextResults.ContextResultExtensions;
using ResultAndOption.Results;
using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.ContextCallables;

public static class ResultCommandExtensions
{
    public static IResultCommand EmptyCallable()
    {
        return new SimpleResultCommandGenerator(Nothing.DoNothingResult);
    }

    public static IResultCommand ToResultCommand(this Action action)
    {
        return new SimpleResultCommandGenerator(action.WrapInResult());
    }

    public static IResultCommand ToResultCommand(this Func<Result> action)
    {
        return new SimpleResultCommandGenerator(action);
    }
}

public static class Nothing
{
    public static Result DoNothingResult()
    {
        return Result.Ok();
    }

    public static void DoNothing()
    {
    }
}