namespace Day02
{
    public interface IPasswordValidator
    {
        bool IsPasswordCompliant
        (
            IPasswordPolicy passwordPolicy,
            string password
        );
    }
}
