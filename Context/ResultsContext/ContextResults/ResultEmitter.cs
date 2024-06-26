using ResultAndOption.Results;

namespace Context.ResultsContext.ContextResults;

public sealed class ResultEmitter<T> where T : notnull
{
    private readonly List<ResultSubscriber<T>> _subscribers;

    public ResultEmitter()
    {
        _subscribers = new List<ResultSubscriber<T>>();
    }

    public void Emit(Result<T> result) => _subscribers.ForEach(sub => sub.Update(result));
    public void Subscribe(ResultSubscriber<T> subscriber) => _subscribers.Add(subscriber);
}