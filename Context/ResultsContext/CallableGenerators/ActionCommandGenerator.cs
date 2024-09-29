using Context.ResultsContext.ActionCallables;
using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.CallableGenerators;

internal sealed class ActionCommandGenerator : ICommandGenerator
{
    private readonly Action _action;

    public ActionCommandGenerator(Action action)
    {
        _action = action;
    }

    public ICommand Generate() => new ActionCommand(_action);
}