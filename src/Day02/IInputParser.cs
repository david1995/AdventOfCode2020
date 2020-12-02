namespace Day02
{
    public interface IInputParser
    {
        (IPasswordPolicy policy, string password)? ParseInput(string input);
    }
}
