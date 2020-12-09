using System.Collections.Immutable;

namespace Day06
{
    public class CustomsSheetGroup
    {
        public CustomsSheetGroup(IImmutableList<CustomsSheet> customsSheets)
        {
            CustomsSheets = customsSheets;
        }

        public IImmutableList<CustomsSheet> CustomsSheets { get; }
    }
}
