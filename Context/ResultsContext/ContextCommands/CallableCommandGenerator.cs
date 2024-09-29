using Context.ResultsContext.ActionCallables;
using Context.ResultsContext.CallableGenerators;

namespace Context.ResultsContext.ContextCommands;

internal sealed class CallableCommandGenerator : ICommandGenerator
{
    private readonly IResultCommandGenerator _resultCommandGenerator;
    private readonly ActionCallables.ICommandGenerator _undoGenerator;

    public CallableCommandGenerator(IResultCommandGenerator resultCommandGenerator, ActionCallables.ICommandGenerator undoGenerator)
    {
        _resultCommandGenerator = resultCommandGenerator;
        _undoGenerator = undoGenerator;
    }

    public IUndoableCommand Generate()
    {
        return new CommandWrapUndoableCommand(_resultCommandGenerator.Generate(), _undoGenerator.Generate());
    }
}

internal sealed class CommandWrapper : ICommandGenerator
{
    private readonly IUndoableCommand _undoableCommand;

    public CommandWrapper(IUndoableCommand undoableCommand)
    {
        _undoableCommand = undoableCommand;
    }

    public IUndoableCommand Generate()
    {
        return _undoableCommand;
    }
}