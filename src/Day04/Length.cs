using System;
using System.Text.RegularExpressions;

namespace Day04
{
    public readonly struct Length
    {
        public Length(int value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }

        public readonly int Value;

        public readonly Unit Unit;

        public static Length? FromString(string? lengthToParse)
        {
            if (lengthToParse is null)
            {
                return default;
            }

            return Regex.Match(lengthToParse, @"^(?<value>[0-9]+)(?<unit>cm|in)$") switch
            {
                { Success: true } match => new Length(
                    int.Parse(
                        match.Groups["value"]
                           .Value
                    ),
                    Enum.Parse<Unit>(
                        match.Groups["unit"]
                           .Value,
                        true
                    )
                ),
                _ => default
            };
        }
    }
}
