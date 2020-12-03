using System.Collections.Immutable;

namespace Day03
{
    public class TaskExecutionLogic
        : ITaskExecutionLogic
    {
        private readonly ITobogganCommandProvider _commandProvider;
        private readonly ITobogganRuleEngine _ruleEngine;

        public TaskExecutionLogic
        (
            ITobogganRuleEngine ruleEngine,
            ITobogganCommandProvider commandProvider
        )
        {
            _ruleEngine = ruleEngine;
            _commandProvider = commandProvider;
        }

        public State ExecuteForTopography(Topography topography)
        {
            var initialState = new State(
                topography,
                ImmutableList.Create<(int, int)>(),
                0,
                0
            );

            return RunCommands(initialState);
        }

        public State RunCommands(State currentState)
        {
            var result = ExecuteUntilNothingCanBeDone(currentState);

            if (result.isFinished)
            {
                return currentState;
            }

            return RunCommands(result.nextState);
        }

        public (State nextState, bool isFinished) ExecuteUntilNothingCanBeDone(State currentState)
        {
            var maybeNextCommand = _commandProvider.GetNextCommand(currentState);

            return maybeNextCommand is { } cmd
                ? (_ruleEngine.ExecuteCommand(cmd, currentState), false)
                : (currentState, true);
        }
    }
}
