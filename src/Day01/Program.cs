using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day01
{
    public class Program
    {
        private static SumResultFinder _sumResultFinder;

        internal static int Main(string[] args)
        {
            _sumResultFinder = new SumResultFinder();

            var inputLines = File.ReadAllLines("input.txt");
            int[] numbers = inputLines
               .Select(int.Parse)
               .ToArray();

            var stopwatch = new Stopwatch();
            FindTuplets(stopwatch, numbers);
            FindTriplets(stopwatch, numbers);

            return 0;
        }

        public static void FindTuplets(Stopwatch stopwatch, int[] numbers)
        {

            int times = 1000;

            DoFullTestRun(
                "C# for loop with Enumerator factory",
                stopwatch,
                (sum, nbs) => _sumResultFinder.FindSummandPairsFor(sum, nbs).ToArray(),
                numbers,
                times
            );

            DoFullTestRun(
                "C# optimized and unsafe",
                stopwatch,
                (sum, nbs) => _sumResultFinder.FindSummandPairsForOptimized(sum, nbs),
                numbers,
                times
            );

            stopwatch.Reset();
            stopwatch.Start();
            var findSummandPairsFor = _sumResultFinder.FindSummandPairsForOptimized(2020, numbers)
               .ToArray();
            stopwatch.Stop();

            Console.WriteLine($"Finished, {stopwatch.Elapsed.TotalMilliseconds}ms elapsed");
            stopwatch.Reset();

            foreach ((int summand1, int summand2) in findSummandPairsFor)
            {
                Console.WriteLine($"{summand1} * {summand2} = {summand1 * summand2}");
            }
        }

        public static void FindTriplets(Stopwatch stopwatch, int[] numbers)
        {

            int times = 1000;

            Console.WriteLine();

            DoFullTestRun(
                "C# for loop with Enumerator factory",
                stopwatch,
                (sum, nbs) => _sumResultFinder.FindSummandTripletsFor(sum, nbs).ToArray(),
                numbers,
                times
            );
            DoFullTestRun(
                "C# optimized and unsafe",
                stopwatch,
                (sum, nbs) => _sumResultFinder.FindSummandTripletsForOptimized(sum, nbs),
                numbers,
                times
            );

            stopwatch.Reset();
            stopwatch.Start();
            var findSummandTripletsFor =
                _sumResultFinder
                   .FindSummandTripletsFor(2020, numbers)
                   .ToArray();
            stopwatch.Stop();


            Console.WriteLine();
            Console.WriteLine($"Finished, {stopwatch.Elapsed.TotalMilliseconds}ms elapsed");
            stopwatch.Reset();

            Console.WriteLine();
            foreach ((int summand1, int summand2, int summand3) in findSummandTripletsFor)
            {
                Console.WriteLine($"{summand1} * {summand2} * {summand3} = {(long)summand1 * summand2 * summand3}");
            }
        }

        private static void DoFullTestRun
        (
            string title,
            Stopwatch stopwatch,
            Action<int, int[]> method,
            int[] numbers,
            int times
        )
        {
            Console.WriteLine();
            Console.WriteLine($"Test started for {times} times for: {title}");
            var total = new Stopwatch();
            total.Start();
            var durationsOptimized =
                Repeat(
                    stopwatch,
                    method,
                    times,
                    numbers
                );

            total.Stop();
            Console.WriteLine($"Done. {total.Elapsed.TotalMilliseconds}ms elapsed");
            var statsOptimized = CalculateStats(durationsOptimized);
            DisplayStats(statsOptimized);
        }

        public static void DisplayStats((double total, double mean, double stdDev, double median, double max, double min) stats)
        {
            (double total, double mean, double stdDev, double median, double max, double min) = stats;
            Console.WriteLine($"Total: {total}ms");
            Console.WriteLine($"Mean: {mean}ms");
            Console.WriteLine($"StdDev: {stdDev}ms");
            Console.WriteLine($"Median: {median}ms");
            Console.WriteLine($"Max: {max}ms");
            Console.WriteLine($"Min: {min}ms");
        }

        public static (double total, double mean, double stdDev, double median, double max, double min) CalculateStats(TimeSpan[] durations)
        {
            var total = durations.Sum(d => d.TotalMilliseconds);
            var mean = durations.Sum(
                    d => d.TotalMilliseconds
                )
              / durations.Length;

            var median =
                durations
                   .OrderBy(d => d)
                   .ElementAt(durations.Length / 2)
                   .TotalMilliseconds;

            var deviations =
                durations
                   .Select(
                        d => Math.Pow(
                            mean - d.TotalMilliseconds,
                            2
                        )
                    )
                   .ToArray();

            var stdDev = deviations.Sum() / deviations.Length;

            var max = durations.Max().TotalMilliseconds;
            var min = durations.Min().TotalMilliseconds;

            return (total, mean, stdDev, median, max, min);
        }

        public static TimeSpan[] Repeat(Stopwatch stopwatch, Action<int, int[]> method, int times, int[] numbers)
        {
            var results = new TimeSpan[times];
            for (int n = 0; n < times; n++)
            {
                stopwatch.Start();
                method(2020, numbers);
                stopwatch.Stop();
                results[n] = stopwatch.Elapsed;
                stopwatch.Reset();
            }

            return results;
        }
    }
}
