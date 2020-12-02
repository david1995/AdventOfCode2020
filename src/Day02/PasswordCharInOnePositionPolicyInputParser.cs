using System.Text.RegularExpressions;

namespace Day02
{
    public class PasswordCharInOnePositionPolicyInputParser : IInputParser
    {
        public static readonly Regex InputParseRegex =
            new Regex(@"^(?<pos1>[1-9][0-9]*)-(?<pos2>[1-9][0-9]*)\s(?<char>[a-z]):\s(?<password>[a-z]+)$");

        public (IPasswordPolicy policy, string password)? ParseInput(string input)
        {
            var match = InputParseRegex.Match(input);
            return match switch
            {
                { Success: true } when
                    int.TryParse(match.Groups["pos1"].Value, out int pos1)
                 && int.TryParse(match.Groups["pos2"].Value, out int pos2) =>

                    (new PasswordCharInOnePositionPolicy(
                         match.Groups["char"].Value[0],
                         pos1,
                         pos2
                     ),
                     match.Groups["password"].Value),

                _ => null
            };
        }
    }
}
