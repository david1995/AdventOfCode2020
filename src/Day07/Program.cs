using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    internal class Program
    {
        internal static int Main()
        {
            DoTask1();
            Console.WriteLine();
            DoTask2();

            Console.ReadLine();
            return 0;
        }

        public static void DoTask2()
        {
            Console.WriteLine("Task 2");
            var bagStrings = File.ReadAllLines("input.txt");
            var bagRuleParser = new BagRuleParser();

            IReadOnlyDictionary<Color, BagRule> parsedBagRules =
                bagStrings
                   .Select(bagRuleParser.Parse)
                   .Where(r => r != null)
                   .ToDictionary(r => r!.BagColor)!;

            var shinyGoldBagColor = new Color("shiny gold");
            var rule = parsedBagRules[shinyGoldBagColor]!;
            int result = CountContainedBags(parsedBagRules, rule);

            Console.WriteLine($"Amount: {result}");
        }

        public static int CountContainedBags
        (
            IReadOnlyDictionary<Color, BagRule> allRules,
            BagRule currentRule
        )
        {
            int childSum =
                currentRule
                   .AllowedChildBags
                   .SelectMany(cr => Enumerable.Repeat(allRules[cr.BagColor], cr.Amount))
                   .Select(r => CountContainedBags(allRules, r) + 1)
                   .Sum();

            return childSum;
        }

        public static void DoTask1()
        {
            Console.WriteLine("Task 1");
            var bagStrings = File.ReadAllLines("input.txt");
            var bagRuleParser = new BagRuleParser();

            BagRule[] parsedBagRules =
                bagStrings
                   .Select(bagRuleParser.Parse)
                   .ToArray()!;

            var shinyGoldBagColor = new Color("shiny gold");

            int result = parsedBagRules.Aggregate(
                0,
                (acc, r) => CanHoldBagColor(
                        parsedBagRules,
                        r,
                        shinyGoldBagColor
                    )
                  > 0
                        ? acc + 1
                        : acc
            );

            Console.WriteLine($"Amount: {result}");
        }

        public static int CanHoldBagColor(BagRule[] allRules, BagRule currentRule, Color colorToCheck)
        {
            if (currentRule.AllowedChildBags.Any(b => b.BagColor == colorToCheck))
            {
                return 1;
            }

            return currentRule
               .AllowedChildBags
               .Aggregate(
                    0,
                    (acc, cr) => acc + CanHoldBagColor(allRules, allRules.Single(r => r.BagColor == cr.BagColor), colorToCheck)
                );
        }
    }

    public class BagRuleParser
    {
        private readonly Regex _parentBagRegex = new Regex(@"^(?<BagColor>[\w ]+) bags contain (?<SubBags>[\w\d .\-,]+)$");
        private readonly Regex _childBagRegex = new Regex(@"(?<Amount>([1-9][0-9]*)|(no)) (?<BagColor>[a-z]+( [a-z]+)*) bags?[,.]");

        public BagRule? Parse(string input)
        {
            var parentMatch = _parentBagRegex.Match(input);

            if (!parentMatch.Success)
            {
                return default;
            }

            string bagColorString = parentMatch.Groups["BagColor"].Value;
            var bagColor = new Color(bagColorString);
            var subBagMatches =
                from m in _childBagRegex.Matches(
                    parentMatch.Groups["SubBags"]
                       .Value
                )
                where m.Success
                where m.Groups["BagColor"].Value != "other"
                let amountString = m.Groups["Amount"].Value
                let childBagColorString = m.Groups["BagColor"].Value
                let amount = amountString == "no" ? 0 : int.Parse(amountString)
                let childBagColor = new Color(childBagColorString)
                select new ChildBagRule(childBagColor, amount);

            return new BagRule(bagColor, subBagMatches.ToImmutableArray());
        }
    }

    public class BagRule
    {
        public BagRule(Color bagColor, IImmutableList<ChildBagRule> allowedChildBags)
        {
            BagColor = bagColor;
            AllowedChildBags = allowedChildBags;
        }

        public Color BagColor { get; }

        public IImmutableList<ChildBagRule> AllowedChildBags { get; }
    }

    public class ChildBagRule
    {
        public ChildBagRule(Color bagColor, int amount)
        {
            BagColor = bagColor;
            Amount = amount;
        }

        public Color BagColor { get; }

        public int Amount { get; }
    }

    public struct Color
        : IEquatable<Color>
    {
        public Color(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public bool Equals(Color other)
            => Name == other.Name;

        public override bool Equals(object? obj)
            => obj is Color other && Equals(other);

        public override int GetHashCode()
            => Name.GetHashCode();

        public static bool operator ==(Color left, Color right)
            => left.Equals(right);

        public static bool operator !=(Color left, Color right)
            => !left.Equals(right);
    }
}
