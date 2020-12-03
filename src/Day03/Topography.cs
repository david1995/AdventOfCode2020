using System.Collections.Immutable;
using System.Linq;

namespace Day03
{
    public class Topography
    {
        public Topography(IImmutableDictionary<(int x, int y), IField> fields)
        {
            Fields = fields;
            Height =
                fields.Keys
                   .Select(xy => xy.y)
                   .Max()
              + 1;

            PatternWidth = fields.Keys
                   .Select(xy => xy.x)
                   .Max()
              + 1;
        }

        public int PatternWidth { get; }

        public int Height { get; }

        public IImmutableDictionary<(int x, int y), IField> Fields { get; }

        public override string ToString()
        {
            var rowGroups = Fields
               .GroupBy(f => f.Key.y)
               .OrderBy(g => g.Key)
               .Select(g => g.OrderBy(f => f.Key.x));

            return string.Join("\r\n", rowGroups.Select(g => string.Join("", g.Select(f => f.Value))));
        }
    }
}
