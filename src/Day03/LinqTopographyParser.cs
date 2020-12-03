using System.Collections.Immutable;
using System.Linq;

namespace Day03
{
    public class LinqTopographyParser : ITopographyParser
    {
        public Topography ParseTopography(string[] input)
        {
            var cells = input
               .Select((l, y) => (y, chars: l.Select((c, x) => (c, x))))
               .SelectMany(row => row.chars.Select(e => (e.x, row.y, e.c)))
               .Select(TryGetFieldByChar)
               .Where(field => field != null)
               .Select(field => field!.Value)
               .OrderBy(field => field.y)
               .ThenBy(field => field.x)
               .ToImmutableDictionary(field => (field.x, field.y), field => field.field);

            return new Topography(cells);
        }

        public (int x, int y, IField field)? TryGetFieldByChar((int x, int y, char c) input)
            => TryGetFieldByChar(
                input.x,
                input.y,
                input.c
            );

        public (int x, int y, IField field)? TryGetFieldByChar
        (
            int x,
            int y,
            char c
        )
            => c switch
            {
                '#' => (x, y, new Tree()),
                '.' => (x, y, new EmptyField()),
                _ => default
            };
    }
}
