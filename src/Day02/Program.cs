using System;
using System.IO;

namespace Day02
{
    internal class Program
    {
        internal static int Main()
        {
            string[] inputs = File.ReadAllLines("input.txt");

            int task1ValidPasswordsAmount = DoTask1(inputs);
            Console.WriteLine($"Number of valid inputs: {task1ValidPasswordsAmount} of {inputs.Length}");

            int task2ValidPasswordsAmount = DoTask2(inputs);
            Console.WriteLine($"Number of valid inputs: {task2ValidPasswordsAmount} of {inputs.Length}");

            return 0;
        }

        public static int DoTask1
        (
            string[] inputs
        )
        {
            IInputParser inputParser = new PasswordCharAmountPolicyInputParser();
            IPasswordValidator passwordValidator = new PasswordValidator();
            IPasswordValidationLogic passwordValidationLogic = new PasswordValidationLogic(inputParser, passwordValidator);
            return passwordValidationLogic.GetValidPasswords(inputs);
        }

        public static int DoTask2
        (
            string[] inputs
        )
        {
            IInputParser inputParser = new PasswordCharInOnePositionPolicyInputParser();
            IPasswordValidator passwordValidator = new PasswordValidator();
            IPasswordValidationLogic passwordValidationLogic = new PasswordValidationLogic(inputParser, passwordValidator);
            return passwordValidationLogic.GetValidPasswords(inputs);
        }
    }
}
