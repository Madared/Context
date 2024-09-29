using ResultAndOption.Results;

namespace Context.ResultsContext.ContextCommands;

internal sealed class UndoableCommandWithInputWrapper<T> : IUndoableCommand where T : notnull
{
    private readonly ICommandWithInput<T> _command;
    private readonly Result<T> _result;

    public UndoableCommandWithInputWrapper(Result<T> result, ICommandWithInput<T> command)
    {
        _result = result;
        _command = command;
    }

    public Result Do()
    {
        return _result.Failed ? Result.Fail(_result.Error) : _command.Call(_result.Data);
    }

    public void Undo()
    {
        if (_result.Failed) return;
        _command.Undo(_result.Data);
    }
}