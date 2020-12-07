using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day04
{
    internal class Program
    {
        internal static int Main()
        {
            var fileInputScannerTask1 = new FileInputScanner("input.txt");
            var injectedPassportValidatorTask1 = new InjectedPassportValidator();

            RunScan(fileInputScannerTask1, injectedPassportValidatorTask1);
            Console.WriteLine();

            var fileInputScannerTask2 = new FileInputScanner("input.txt");
            var injectedPassportValidatorTask2 = new InjectedImprovedPassportValidator();

            RunScan(fileInputScannerTask2, injectedPassportValidatorTask2);
            Console.ReadLine();

            return 0;
        }

        public static void RunScan(FileInputScanner inputScanner, IPassportValidator passportValidator)
        {
            var passports = inputScanner.Scan();

            var validatedPassports =
                passports
                   .Select(p => (passport: p, isValid: passportValidator.IsPassportValid(p)))
                   .ToArray();

            Console.WriteLine($"Valid passports: {validatedPassports.Count(p => p.isValid)}");
            Console.WriteLine($"Invalid passports: {validatedPassports.Count(p => !p.isValid)}");
        }
    }

    public interface IPassportValidator
    {
        bool IsPassportValid(ScannedPassportInfo passport);
    }

    public class InjectedPassportValidator
        : IPassportValidator
    {
        public bool IsPassportValid(ScannedPassportInfo passport)
        {
            return passport.BirthYear != null
             && passport.ExpirationYear != null
             && passport.EyeColor != null
             && passport.HairColor != null
             && passport.Height != null
             && passport.IssueYear != null
             && passport.PassportId != null;
        }
    }

    public class InjectedImprovedPassportValidator
        : IPassportValidator
    {
        private static readonly Regex PassportIdRegex = new Regex(@"^[0-9]{9}$");

        public bool IsPassportValid(ScannedPassportInfo passport)
        {
            return passport.BirthYear >= 1920
                && passport.BirthYear <= 2002
             && passport.IssueYear >= 2010
                && passport.IssueYear <= 2020
             && passport.ExpirationYear >= 2020
                && passport.ExpirationYear <= 2030
             && passport.Height != null
             && (passport.Height.Value.Unit == Unit.Cm && passport.Height.Value.Value >= 150 && passport.Height.Value.Value <= 193
                || passport.Height.Value.Unit == Unit.In && passport.Height.Value.Value >= 59 && passport.Height.Value.Value <= 76)
             && passport.HairColor != null
             && passport.EyeColor > 0
             && passport.PassportId != null
                && PassportIdRegex.IsMatch(passport.PassportId);
        }
    }
}
