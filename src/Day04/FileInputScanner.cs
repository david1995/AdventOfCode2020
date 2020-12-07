using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04
{
    public class FileInputScanner
    {
        public const string BirthYearName = @"byr";
        public const string IssueYearName = @"iyr";
        public const string ExpirationYearName = @"eyr";
        public const string HeightName = @"hgt";
        public const string HairColorName = @"hcl";
        public const string EyeColorName = @"ecl";
        public const string PassportIdName = @"pid";
        public const string CountryIdName = @"cid";
        private readonly string _fileName;

        public FileInputScanner(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerable<ScannedPassportInfo> Scan()
        {
            using var reader = new StreamReader(
                File.Open(
                    _fileName,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite
                )
            );

            var readLines = new List<string>();

            var startIndex = 0;
            for (var lineIndex = 0; !reader.EndOfStream; lineIndex++)
            {
                string? readLine = reader.ReadLine();
                readLines.Add(readLine!);

                if (!string.IsNullOrWhiteSpace(readLine))
                {
                    continue;
                }

                yield return ParseInfoFromArray(readLines.Skip(startIndex));

                startIndex = lineIndex + 1;
            }

            if (readLines.Count != startIndex)
            {
                yield return ParseInfoFromArray(readLines.Skip(startIndex));
            }
        }

        public ScannedPassportInfo ParseInfoFromArray(IEnumerable<string> input)
        {
            var passportFields =
                input
                   .SelectMany(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                   .Select(s => s.Split(':'))
                   .ToDictionary(s => s[0], s => s[1]);

            int? birthYear =
                int.TryParse(GetFieldValueOrNull(passportFields, BirthYearName), out int byr)
                    ? (int?)byr
                    : default;

            int? issueYear = int.TryParse(GetFieldValueOrNull(passportFields, IssueYearName), out int iyr)
                ? (int?)iyr
                : default;

            int? expirationYear = int.TryParse(GetFieldValueOrNull(passportFields, ExpirationYearName), out int eyr)
                ? (int?)eyr
                : default;

            string? heightString = GetFieldValueOrNull(passportFields, HeightName);
            Length? height = Length.FromString(heightString);

            var hairColor = Color.FromHexString(GetFieldValueOrNull(passportFields, HairColorName));
            var eyeColor = Enum.TryParse<EyeColor>(GetFieldValueOrNull(passportFields, EyeColorName), true, out var ecl)
                ? (EyeColor?)ecl
                : EyeColor.Unknown;

            var passportId = GetFieldValueOrNull(passportFields, PassportIdName);

            int? countryId = int.TryParse(GetFieldValueOrNull(passportFields, CountryIdName), out int cid)
                ? (int?)cid
                : default;

            return new ScannedPassportInfo(
                birthYear,
                issueYear,
                expirationYear,
                height,
                hairColor,
                eyeColor,
                passportId,
                countryId
            );
        }

        public string? GetFieldValueOrNull(IDictionary<string, string> dic, string key)
        {
            return dic.ContainsKey(key)
                ? dic[key]
                : null;
        }
    }
}
