using System.Collections.Immutable;

namespace Day06
{
    public class CustomsSheet
    {
        public CustomsSheet(IImmutableSet<char> answers)
        {
            Answers = answers;
        }

        public IImmutableSet<char> Answers { get; }

        public override string ToString()
            => string.Join(string.Empty, Answers);
    }
}
