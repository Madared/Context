using Context.ResultsContext.ContextCallables;
using ResultAndOption.Results;
using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.ContextCommands;

public interface IUndoableCommand : IResultCommand
{
    void Undo();
}

public interface ICommandWithInput<in T> where T : notnull
{
    Result Call(T data);
    void Undo(T data);
}


public interface ICommandWithCallInput<in T> where T : notnull
{
    Result Call(T data);
    void Undo();
}