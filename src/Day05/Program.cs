using System;
using System.IO;
using System.Linq;

namespace Day05
{
    internal class Program
    {
        internal static int Main()
        {
            var fileContents = File.ReadAllLines("input.txt").Select(l => l.ToCharArray()).ToArray();
            DoTask1(fileContents);
            Console.WriteLine();
            DoTask2(fileContents);

            Console.ReadLine();

            return 0;
        }

        public static void DoTask1(char[][] fileContents)
        {
            int length = fileContents.Length;
            uint highestId = 0u;

            for (int i = 0; i < length; i++)
            {
                var lineLength = fileContents[i]
                   .Length;
                char[] line = fileContents[i];
                var rowColumnIndex = GetRowAndColumnViaBinaryTree(line, lineLength);
                uint seatId = GetSeatId(rowColumnIndex);
                if (highestId < seatId)
                {
                    highestId = seatId;
                }
            }

            Console.WriteLine($"Highest Id: {highestId}");
        }

        public static void DoTask2(char[][] fileContents)
        {
            int length = fileContents.Length;
            uint[] seats = new uint[128<<3];

            for (int i = 0; i < length; i++)
            {
                int lineLength = fileContents[i].Length;

                char[] line = fileContents[i];
                var rowColumnIndex = GetRowAndColumnViaBinaryTree(line, lineLength);
                uint seatId = GetSeatId(rowColumnIndex);
                seats[seatId] = seatId;
            }

            var offsetStart =
                seats.Length
              - seats
                   .SkipWhile(v => v == default)
                   .Count();

            var offsetEnd =
                seats.Length
              - seats
                   .Reverse()
                   .SkipWhile(v => v == default)
                   .Count();

            var actualAvailableSeats = seats[offsetStart..^offsetEnd];
            (_, int idx) =
                actualAvailableSeats
                   .Select((s, i) => (s, i))
                   .Single(s => s.s == default);

            int actualIndex = idx + offsetStart;
            uint row = (uint)(actualIndex / 8);
            uint col = (uint)(actualIndex % 8);
            var mySeatId = GetSeatId((row, col));

            Console.WriteLine($"My seat Id: {mySeatId}");
        }

        public static uint GetSeatId((uint row, uint col) index)
        {
            (uint row, uint col) = index;
            return (row << 3) + col;
        }

        public static (uint row, uint column) GetRowAndColumnViaBinaryTree(char[] input, int length)
        {
            uint rowIndex = BuildBinaryIndex(input, 0, length - 3, 'B');
            uint columnIndex = BuildBinaryIndex(input, length - 3, 3, 'R');

            return (rowIndex, columnIndex);
        }

        public static uint BuildBinaryIndex
        (
            char[] input,
            int start,
            int length,
            char incrementIndicator
        )
        {
            if (length > 32)
            {
                throw new NotSupportedException();
            }

            int maxShift = length - 1;
            uint index = 0;
            for (int charIndex = 0; charIndex < length; charIndex++)
            {
                if (input[start + charIndex] == incrementIndicator)
                {
                    index |= 0b1u << (maxShift - charIndex);
                }
            }

            return index;
        }
    }
}
