using System.Linq;

namespace Day02
{
    public class PasswordValidationLogic
        : IPasswordValidationLogic
    {
        private readonly IInputParser _inputParser;
        private readonly IPasswordValidator _passwordValidator;

        public PasswordValidationLogic(IInputParser inputParser, IPasswordValidator passwordValidator)
        {
            _inputParser = inputParser;
            _passwordValidator = passwordValidator;
        }

        public int GetValidPasswords(string[] inputs)
        {
            var parsedInputs =
                inputs
                   .Select(_inputParser.ParseInput)
                   .Where(r => r != null)
                   .Select(r => r!.Value)
                   .ToArray();

            return
                parsedInputs
                   .Select(v => _passwordValidator.IsPasswordCompliant(v.policy, v.password))
                   .Count(b => b);
        }
    }
}
