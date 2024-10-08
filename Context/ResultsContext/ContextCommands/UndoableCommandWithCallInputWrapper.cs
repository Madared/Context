using ResultAndOption.Results;

namespace Context.ResultsContext.ContextCommands;

internal sealed class UndoableCommandWithCallInputWrapper<T> : IUndoableCommand where T : notnull
{
    private readonly ICommandWithCallInput<T> _commandWithCallInput;
    private readonly Result<T> _result;

    public UndoableCommandWithCallInputWrapper(ICommandWithCallInput<T> commandWithCallInput, Result<T> result)
    {
        _commandWithCallInput = commandWithCallInput;
        _result = result;
    }

    public Result Do()
    {
        return _result.Failed ? Result.Fail(_result.Error) : _commandWithCallInput.Call(_result.Data);
    }

    public void Undo()
    {
        _commandWithCallInput.Undo();
    }
}