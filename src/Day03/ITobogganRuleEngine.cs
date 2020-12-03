namespace Day03
{
    public interface ITobogganRuleEngine
    {
        State ExecuteCommand(ITobogganCommand command, State currentState);
    }
}
