using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextCallables;
using ResultAndOption.Results.Commands;

namespace Context.ResultsContext.ContextCommands;

public interface ICommandGenerator : IResultCommandGenerator
{
    IResultCommand IResultCommandGenerator.Generate()
    {
        return Generate();
    }

    new IUndoableCommand Generate();
}