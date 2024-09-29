using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.ActionCallables;

public interface ICommandGenerator
{
    ICommand Generate();
}