using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.ActionCallables;

internal sealed class ActionCommand : ICommand 
{
    private readonly Action _action;

    public ActionCommand(Action action)
    {
        _action = action;
    }
    public void Do() => _action();
}