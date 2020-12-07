namespace Day04
{
    public class ScannedPassportInfo
    {
        public ScannedPassportInfo
        (
            int? birthYear,
            int? issueYear,
            int? expirationYear,
            Length? height,
            Color? hairColor,
            EyeColor? eyeColor,
            string? passportId,
            int? countryId
        )
        {
            BirthYear = birthYear;
            IssueYear = issueYear;
            ExpirationYear = expirationYear;
            Height = height;
            HairColor = hairColor;
            EyeColor = eyeColor;
            PassportId = passportId;
            CountryId = countryId;
        }

        public int? BirthYear { get; }

        public int? IssueYear { get; }

        public int? ExpirationYear { get; }

        public Length? Height { get; }

        public Color? HairColor { get; }

        public EyeColor? EyeColor { get; }

        public string? PassportId { get; }

        public int? CountryId { get; }
    }
}
