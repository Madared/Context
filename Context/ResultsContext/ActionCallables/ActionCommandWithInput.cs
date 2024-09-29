using ResultAndOption.Results;
using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.ActionCallables;

internal sealed class ActionCommandWithInput<TIn> : ICommand where TIn : notnull
{
    private readonly Action<TIn> _action;
    private readonly Result<TIn> _result;

    public ActionCommandWithInput(Result<TIn> result, Action<TIn> action)
    {
        _result = result;
        _action = action;
    }

    public void Do()
    {
        if (_result.Failed) return;
        _action(_result.Data);
    }
}