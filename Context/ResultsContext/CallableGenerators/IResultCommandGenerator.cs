using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.CallableGenerators;

public interface IResultCommandGenerator
{
    IResultCommand Generate();
}