using System.Text.RegularExpressions;

namespace Day02
{
    public class PasswordCharAmountPolicyInputParser : IInputParser
    {
        public static readonly Regex InputParseRegex =
            new Regex(@"^(?<min>[1-9][0-9]*)-(?<max>[1-9][0-9]*)\s(?<char>[a-z]):\s(?<password>[a-z]+)$");

        public (IPasswordPolicy policy, string password)? ParseInput(string input)
        {
            var match = InputParseRegex.Match(input);
            return match switch
            {
                { Success: true } when
                    uint.TryParse(match.Groups["min"].Value, out uint min)
                 && uint.TryParse(match.Groups["max"].Value, out uint max) =>

                    (new PasswordCharAmountPolicy(
                         match.Groups["char"].Value[0],
                         min,
                         max
                     ),
                     match.Groups["password"].Value),

                _ => null
            };
        }
    }
}
