using System;
using System.Linq;

namespace Day02
{
    public class PasswordValidator
        : IPasswordValidator
    {
        public bool IsPasswordCompliant
        (
            IPasswordPolicy passwordPolicy,
            string password
        )
        {
            return passwordPolicy switch
            {
                PasswordCharAmountPolicy charAmountPolicy => IsPasswordCharAmountPolicyCompliant(charAmountPolicy, password),
                PasswordCharInOnePositionPolicy charInPositionPolicy => IsPasswordCharInRangePolicyCompliant(charInPositionPolicy, password),
                _ => throw new NotSupportedException($"Password policy {passwordPolicy.GetType()} unknown")
            };
        }

        public bool IsPasswordCharAmountPolicyCompliant
        (
            PasswordCharAmountPolicy passwordCharAmountPolicy,
            string password
        )
        {
            int charCountToCheck = password.Count(c => c == passwordCharAmountPolicy.Char);
            return
                charCountToCheck >= passwordCharAmountPolicy.MinimumAmount
             && charCountToCheck <= passwordCharAmountPolicy.MaximumAmount;
        }

        public bool IsPasswordCharInRangePolicyCompliant
        (
            PasswordCharInOnePositionPolicy policy,
            string password
        )
        {
            int zeroBasedFirst = policy.Position1 - 1;
            int zeroBasedLast = policy.Position2 - 1;

            return
                password[zeroBasedFirst] == policy.Char
              ^ password[zeroBasedLast] == policy.Char;
        }
    }
}
