using System;

namespace Day03
{
    public class TobogganRuleEngine
        : ITobogganRuleEngine
    {
        public State ExecuteCommand(ITobogganCommand command, State currentState)
        {
            if (!(command is TobogganMoveCommand moveCommand))
            {
                throw new NotSupportedException($"Command {command.GetType()} not supported");
            }

            int newX = (currentState.CurrentX + moveCommand.DistanceX) % currentState.Topography.PatternWidth;
            int newY = currentState.CurrentY + moveCommand.DistanceY;

            var hitField = currentState.Topography.Fields[(newX, newY)];

            var newTopography =
                hitField switch
                {
                    Tree _ => new Topography(currentState.Topography.Fields.SetItem((newX, newY), new DestroyedTree())),
                    EmptyField _ => new Topography(currentState.Topography.Fields.SetItem((newX, newY), new HitEmptyField())),
                    _ => currentState.Topography
                };

            return new State(
                newTopography,
                currentState.MoveHistory.Add((moveCommand.DistanceX, moveCommand.DistanceY)),
                newX,
                newY
            );
        }
    }
}
