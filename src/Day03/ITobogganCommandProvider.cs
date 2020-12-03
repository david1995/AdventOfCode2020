namespace Day03
{
    public interface ITobogganCommandProvider
    {
        ITobogganCommand? GetNextCommand(State state);
    }
}
