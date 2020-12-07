using System;
using System.Globalization;

namespace Day04
{
    public readonly struct Color
        : IEquatable<Color>
    {
        public Color
        (
            byte red,
            byte green,
            byte blue
        )
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public static Color? FromHexString(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)
             || input.Length != 7
             || input[0] != '#')
            {
                return default;
            }

            bool hasRed = byte.TryParse(input[1..3], NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out byte r);

            bool hasGreen = byte.TryParse(input[3..5], NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out byte g);

            bool hasBlue = byte.TryParse(input[1..3], NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out byte b);

            if (!hasRed || !hasGreen || !hasBlue)
            {
                return default;
            }

            return new Color(
                r,
                g,
                b
            );
        }

        public readonly byte Red;

        public readonly byte Green;

        public readonly byte Blue;

        public bool Equals(Color other)
            => Red == other.Red
             && Green == other.Green
             && Blue == other.Blue;

        public override bool Equals(object? obj)
            => obj is Color other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(
                Red,
                Green,
                Blue
            );

        public static bool operator ==(Color left, Color right)
            => left.Equals(right);

        public static bool operator !=(Color left, Color right)
            => !left.Equals(right);
    }
}
