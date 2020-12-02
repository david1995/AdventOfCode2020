namespace Day02
{
    public class PasswordCharInOnePositionPolicy
        : IPasswordPolicy
    {
        public PasswordCharInOnePositionPolicy
        (
            char c,
            int position1,
            int position2
        )
        {
            Char = c;
            Position1 = position1;
            Position2 = position2;
        }

        public char Char { get; }

        public int Position1 { get; }

        public int Position2 { get; }
    }
}
