using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Day06
{
    public class CustomsSheetReader
    {
        public IEnumerable<CustomsSheetGroup> ReadFromFile(string file)
        {
            using var reader = new StreamReader(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

            var currentSheets = Enumerable.Empty<CustomsSheet>();
            while (!reader.EndOfStream)
            {
                string readLine;
                while (!reader.EndOfStream && !string.IsNullOrEmpty(readLine = reader.ReadLine()))
                {
                    var answers =
                        readLine
                           .ToCharArray()
                           .Distinct();

                    var customsSheet = new CustomsSheet(answers.ToImmutableHashSet());
                    currentSheets = currentSheets.Append(customsSheet);
                }

                yield return new CustomsSheetGroup(currentSheets.ToImmutableList());
                currentSheets = Enumerable.Empty<CustomsSheet>();
            }
        }
    }
}
