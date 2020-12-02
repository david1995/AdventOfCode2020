namespace Day02
{
    public class PasswordCharAmountPolicy
        : IPasswordPolicy
    {
        public PasswordCharAmountPolicy
        (
            char c,
            uint minimumAmount,
            uint maximumAmount
        )
        {
            Char = c;
            MinimumAmount = minimumAmount;
            MaximumAmount = maximumAmount;
        }

        public char Char { get; }

        public uint MinimumAmount { get; }

        public uint MaximumAmount { get; }
    }
}
