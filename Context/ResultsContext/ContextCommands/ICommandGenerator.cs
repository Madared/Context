using Context.ResultsContext.CallableGenerators;
using Context.ResultsContext.ContextCallables;

namespace Context.ResultsContext.ContextCommands;

public interface ICommandGenerator : ICallableGenerator
{
    ICallable ICallableGenerator.Generate()
    {
        return Generate();
    }

    new ICommand Generate();
}