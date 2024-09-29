using ResultAndOption.Results.Getters;

namespace Context.ResultsAsyncContext.AsyncCallableGenerators;

internal sealed class SimpleAsyncResultGetterGenerator<T> : IAsyncResultGetterGenerator<T> where T : notnull
{
    private readonly IAsyncResultGetter<T> _getter;
    public SimpleAsyncResultGetterGenerator(IAsyncResultGetter<T> getter)
    {
        _getter = getter;
    }
    public IAsyncResultGetter<T> Generate() => _getter;
}