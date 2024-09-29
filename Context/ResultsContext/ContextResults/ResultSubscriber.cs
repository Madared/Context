using ResultAndOption.Results;

namespace Context.ResultsContext.ContextResults;

public sealed class ResultSubscriber<T> where T : notnull
{
    public Result<T> Result { get; private set; }
    public ResultSubscriber(Result<T> result)
    {
        Result = result;
    }
    public void Update(Result<T> result)
    {
        Result = result;
    }
}

public sealed class DataSubscriber<T> where T : notnull
{
    public T Data { get; private set; }

    public DataSubscriber(T initial)
    {
        Data = initial;
    }
    public void Update(T data)
    {
        Data = data;
    }
}

public sealed class DataEmitter<T> where T : notnull
{
    private readonly IEnumerable<DataSubscriber<T>> _subscribers;

    public DataEmitter(IEnumerable<DataSubscriber<T>> subscribers)
    {
        _subscribers = subscribers;
    }
    public void Emit(T data)
    {
        foreach (DataSubscriber<T> subscriber in _subscribers)
        {
            subscriber.Update(data);
        }
    }
}