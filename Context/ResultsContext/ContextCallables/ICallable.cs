using Results;

namespace Context.ResultsContext.ContextCallables;

public interface ICallable
{
    Result Call();
}

public interface ICallable<TOut> : ICallable where TOut : notnull
{
    Result ICallable.Call()
    {
        return Call().ToSimpleResult();
    }

    new Result<TOut> Call();
}