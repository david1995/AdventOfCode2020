using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    internal class Program
    {
        internal static int Main()
        {
            string[] rawTopography = File.ReadAllLines("input.txt");

            ITopographyParser topographyParser = new LinqTopographyParser();
            var topography = topographyParser.ParseTopography(rawTopography);

            DoTask1(topography);
            DoTask2(topography);

            return 0;
        }

        public static void DoTask2(Topography topography)
        {
            var moveCommandsToCheck = new (int dx, int dy)[]
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };

            ITobogganRuleEngine tobogganRuleEngine = new TobogganRuleEngine();

            long result = GenerateTask2Results(
                    topography,
                    moveCommandsToCheck,
                    tobogganRuleEngine
                )
               .Aggregate(1L, (acc, t) => acc * t);


            Console.WriteLine($"Multiplied Result: {result}");
        }

        private static IEnumerable<int> GenerateTask2Results
        (
            Topography topography,
            (int dx, int dy)[] moveCommandsToCheck,
            ITobogganRuleEngine tobogganRuleEngine
        )
        {
            foreach (var moveCommand in moveCommandsToCheck)
            {
                ITobogganCommandProvider commandProvider = new TobogganMoveCommandProvider(new TobogganMoveCommand(moveCommand.dx, moveCommand.dy));
                ITaskExecutionLogic taskExecutionLogic = new TaskExecutionLogic(
                    tobogganRuleEngine,
                    commandProvider
                );
                State result = taskExecutionLogic.ExecuteForTopography(topography);
                int hitTrees =
                    result.Topography.Fields.Count(f => f.Value is DestroyedTree);
                yield return hitTrees;
            }
        }

        public static void DoTask1(Topography topography)
        {
            ITobogganRuleEngine tobogganRuleEngine = new TobogganRuleEngine();
            ITobogganCommandProvider commandProvider = new TobogganMoveCommandProvider(new TobogganMoveCommand(3, 1));
            ITaskExecutionLogic taskExecutionLogic = new TaskExecutionLogic(
                tobogganRuleEngine,
                commandProvider
            );
            State result = taskExecutionLogic.ExecuteForTopography(topography);
            int hitTrees =
                result.Topography.Fields.Count(f => f.Value is DestroyedTree);

            Console.WriteLine($"Hit trees: {hitTrees}");
            Console.WriteLine();
        }
    }
}
