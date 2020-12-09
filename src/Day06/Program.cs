using System;
using System.Collections.Generic;
using System.Linq;

namespace Day06
{
    internal class Program
    {
        internal static int Main()
        {
            var customsSheetReader = new CustomsSheetReader();
            var customsSheetGroups =
                customsSheetReader
                   .ReadFromFile("input.txt")
                   .ToArray();

            DoTask1(customsSheetGroups);
            DoTask2(customsSheetGroups);

            Console.ReadLine();
            return 0;
        }

        public static void DoTask1(IEnumerable<CustomsSheetGroup> customsSheetGroups)
        {
            Console.WriteLine();
            Console.WriteLine("Task 1");

            int sum =
                customsSheetGroups.Sum(
                    g =>
                        g.CustomsSheets
                           .SelectMany(s => s.Answers)
                           .Distinct()
                           .Count()
                );

            Console.WriteLine($"Sum: {sum}");
        }

        public static void DoTask2(IEnumerable<CustomsSheetGroup> customsSheetGroups)
        {
            Console.WriteLine();
            Console.WriteLine("Task 2");

            var allAnswersPerGroupCounts =
                from g in customsSheetGroups
                let allAnswersInGroup =
                    g.CustomsSheets
                       .SelectMany(s => s.Answers)
                       .Distinct()
                let intersected = g.CustomsSheets.Aggregate(allAnswersInGroup, (acc, s) => acc.Intersect(s.Answers))
                let count = intersected.Count()
                select count;

            int sum = allAnswersPerGroupCounts.Sum();

            Console.WriteLine($"Sum: {sum}");
        }
    }
}
