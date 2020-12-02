using System;
using System.Collections.Generic;
using System.Linq;

namespace Day01
{
    public class SumResultFinder
    {
        public IEnumerable<(int summand1, int summand2)> FindSummandPairsFor(int sumToFind, int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            for (int j = i + 1; j < numbers.Length; j++)
            {
                if (numbers[i] + numbers[j] == sumToFind)
                {
                    yield return (numbers[i], numbers[j]);
                }
            }
        }

        public IEnumerable<(int summand1, int summand2, int summand3)> FindSummandTripletsFor
        (
            int sumToFind,
            int[] numbers
        )
        {
            for (int i = 0; i < numbers.Length; i++)
            for (int j = i + 1; j < numbers.Length; j++)
            for (int k = j + 1; k < numbers.Length; k++)
            {
                if (numbers[i] + numbers[j] + numbers[k] == sumToFind)
                {
                    yield return (numbers[i], numbers[j], numbers[k]);
                }
            }
        }

        public unsafe IEnumerable<(int summand1, int summand2)> FindSummandPairsForOptimized(int sumToFind, int[] numbers)
        {
            unchecked
            {
                Span<ulong> pairs = stackalloc ulong[100];
                int pairsLength = 0;

                fixed (int* fixedNumbers = numbers)
                {
                    for (int i = 0; i < numbers.Length; i++)
                    for (int j = i + 1; j < numbers.Length; j++)
                    {
                        if (fixedNumbers[i] + fixedNumbers[j] == sumToFind)
                        {
                            pairs[pairsLength] = ((ulong)fixedNumbers[i] << (sizeof(int) * 8)) | (ulong)fixedNumbers[j];
                            pairsLength++;
                        }
                    }
                }

                return pairs
                   .ToArray()
                   .Take(pairsLength)
                   .Select(l => ((int)(l >> (sizeof(int) * 8)), (int)l));
            }
        }

        public unsafe IEnumerable<(int summand1, int summand2, int summand3)> FindSummandTripletsForOptimized(int sumToFind, int[] numbers)
        {
            unchecked
            {
                Span< (int, int, int)> pairs = stackalloc (int, int, int)[100];
                int pairsLength = 0;

                fixed (int* fixedNumbers = numbers)
                {
                    for (int i = 0; i < numbers.Length; i++)
                    for (int j = i + 1; j < numbers.Length; j++)
                    for (int k = j + 1; k < numbers.Length; k++)
                    {
                        if (fixedNumbers[i] + fixedNumbers[j] + fixedNumbers[k] == sumToFind)
                        {
                            pairs[pairsLength] = (fixedNumbers[i], fixedNumbers[j], fixedNumbers[k]);
                            pairsLength++;
                        }
                    }
                }

                return pairs
                   .ToArray()
                   .Take(pairsLength);
            }
        }
    }
}
