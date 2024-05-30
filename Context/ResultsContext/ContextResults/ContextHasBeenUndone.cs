
using Results;

namespace Context.ResultsContext.ContextResults;

public record ContextHasBeenUndone : IError
{
    public string Message => "Context has been undone";
}