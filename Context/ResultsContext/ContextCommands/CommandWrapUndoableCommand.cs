using Context.ResultsContext.ActionCallables;
using Context.ResultsContext.ContextCallables;
using ResultAndOption.Results;
using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.ContextCommands;

internal sealed class CommandWrapUndoableCommand : IUndoableCommand
{
    private readonly IResultCommand _callable;
    private readonly ICommand _undoer;

    public CommandWrapUndoableCommand(IResultCommand callable, ICommand undoer)
    {
        _callable = callable;
        _undoer = undoer;
    }

    public Result Do()
    {
        return _callable.Do();
    }

    public void Undo()
    {
        _undoer.Do();
    }
}