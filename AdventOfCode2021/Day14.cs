using System.Diagnostics;
using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day14 : Day<dynamic>
    {
        private readonly string SampleInput = "NNCB\n\nCH -> B\nHH -> N\nCB -> H\nNH -> C\nHB -> C\nHC -> B\nHN -> C\nNN -> C\nBH -> H\nNC -> B\nNB -> B\nBN -> B\nBB -> N\nBC -> B\nCC -> N\nCN -> C";

        public override dynamic ParseInput(string rawInput)
        {
            // Sample input
            //rawInput = SampleInput;

            var input = rawInput.Split('\n');

            var template = input.First();

            var rules = input.Skip(1)
                             .Where(i => i != string.Empty)
                             .ToDictionary(i => i.Split(" -> ").First(),
                                           i => i.Split(" -> ").Last());

            return new { polymerTemplate = template, insertionRules = rules };
        }

        public override object ExecutePart1()
        {
            string polymer = Input.polymerTemplate;
            Dictionary<string, string> rules = Input.insertionRules;

            Console.WriteLine($"Template: {polymer}");

            for (int step = 1; step <= 10; step++)
            {
                for (int c = 0; ; c++)
                {
                    try
                    {
                        var pair = $"{polymer[c]}{polymer[c + 1]}";
                        var input = rules.Where(kvp => kvp.Key == pair).Select(kvp => kvp.Value).FirstOrDefault();

                        if (!string.IsNullOrWhiteSpace(input))
                        {
                            polymer = polymer.Insert(c + 1, input);
                            c++;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // End of string reached
                        break;
                    }
                }
                Console.WriteLine($"After step {step}: {polymer}");
            }

            var charMap = polymer.Distinct()
                                 .ToDictionary(c => c, c => polymer.Count(s => s == c))
                                 .OrderByDescending(kvp => kvp.Value);

            var mostCommon = charMap.First().Value;
            var leastCommon = charMap.Last().Value;

            return mostCommon - leastCommon;
        }

        public override object ExecutePart2()
        {
            string polymer = Input.polymerTemplate;
            Dictionary<string, string> rules = Input.insertionRules;

            Console.WriteLine($"Template: {polymer}");

            var pairs = new Dictionary<string, long>();

            foreach (var i in Enumerable.Range(0, polymer.Length - 1))
            {
                var pair = polymer.Substring(i, 2);
                if (pairs.ContainsKey(pair))
                {
                    pairs[pair] = pairs[pair] + 1;
                }
                else
                {
                    pairs.Add(pair, 1);
                }
            }

            foreach (var step in Enumerable.Range(0, 40))
            {
                var newPairs = new Dictionary<string, long>();

                foreach (var pair in pairs)
                {
                    if (rules.ContainsKey(pair.Key))
                    {
                        var element = rules.First(r => r.Key == pair.Key).Value;
                        var result = pair.Key.Insert(1, element);

                        foreach (var i in Enumerable.Range(0, result.Length - 1))
                        {
                            var subPair = result.Substring(i, 2);

                            if (newPairs.ContainsKey(subPair))
                            {
                                newPairs[subPair] = newPairs[subPair] + pair.Value;
                            }
                            else
                            {
                                newPairs.Add(subPair, pair.Value);
                            }
                        }
                    }
                }

                pairs = new Dictionary<string, long>(newPairs);
            }

            var molucules = GetMolecules(pairs);

            var max = (molucules.OrderByDescending(c => c.Value).First().Value / 2) + 1;
            var min = (molucules.OrderByDescending(c => c.Value).Last().Value / 2) + 1;

            return max - min;
        }

        private object Part2Try1()
        {
            string polymer = Input.polymerTemplate;
            Dictionary<string, string> rules = Input.insertionRules;

            Console.WriteLine($"Template: {polymer}");

            var stopwatch = new Stopwatch();

            for (int step = 1; step <= 15; step++)
            {
                stopwatch.Restart();
                for (int c = 0; ; c++)
                {
                    try
                    {
                        var pair = $"{polymer[c]}{polymer[c + 1]}";
                        var input = rules.Where(kvp => kvp.Key == pair).Select(kvp => kvp.Value).FirstOrDefault();

                        if (!string.IsNullOrWhiteSpace(input))
                        {
                            polymer = polymer.Insert(c + 1, input);
                            c++;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        // End of string reached
                        break;
                    }
                }
                //Console.WriteLine($"After step {step}: {polymer}");
                Console.WriteLine($"Step {step}; {stopwatch.ElapsedMilliseconds}ms");
                stopwatch.Stop();
            }

            var charMap = polymer.Distinct()
                                 .ToDictionary(c => c, c => polymer.Count(s => s == c))
                                 .OrderByDescending(kvp => kvp.Value);

            var mostCommon = charMap.First().Value;
            var leastCommon = charMap.Last().Value;

            return mostCommon - leastCommon;
        }

        private object Part2Try2()
        {
            string polymer = Input.polymerTemplate;
            Dictionary<string, string> rules = Input.insertionRules;

            Console.WriteLine($"Template: {polymer}");

            var stopwatch = new Stopwatch();

            for (int step = 1; step <= 40; step++)
            {
                var inserts = new List<KeyValuePair<int, string>>();
                stopwatch.Restart();
                foreach (var rule in rules)
                {
                    var matches = polymer.GetAllIndexes(rule.Key);
                    if (!matches.Any())
                    {
                        continue;
                    }

                    foreach (var match in matches)
                    {
                        inserts.Add(new KeyValuePair<int, string>(match, rule.Value));
                    }
                }

                polymer = UpdatePolymer(polymer, inserts);

                //Console.WriteLine($"Step {step}");
                Console.WriteLine($"Step {step}; {stopwatch.ElapsedMilliseconds}ms");
                stopwatch.Stop();
            }

            var charMap = polymer.Distinct()
                                 .ToDictionary(c => c, c => polymer.Count(s => s == c))
                                 .OrderByDescending(kvp => kvp.Value);

            var mostCommon = charMap.First().Value;
            var leastCommon = charMap.Last().Value;

            return mostCommon - leastCommon;
            //return base.ExecutePart2();
        }

        private object Part2Try3()
        {
            string polymer = Input.polymerTemplate;
            Dictionary<string, string> rules = Input.insertionRules;

            Console.WriteLine($"Template: {polymer}");

            var pairs = new Dictionary<string, long>();

            foreach (var i in Enumerable.Range(0, polymer.Length - 1))
            {
                pairs.Add(polymer.Substring(i, 2), 1);
            }

            foreach (var step in Enumerable.Range(0, 40))
            {
                var newPairs = new Dictionary<string, long>();

                foreach (var pair in pairs)
                {
                    if (rules.ContainsKey(pair.Key))
                    {
                        var element = rules.First(r => r.Key == pair.Key).Value;
                        var result = pair.Key.Insert(1, element);

                        foreach (var i in Enumerable.Range(0, result.Length - 1))
                        {
                            var subPair = result.Substring(i, 2);

                            if (newPairs.ContainsKey(subPair))
                            {
                                newPairs[subPair] = newPairs[subPair] + pair.Value;
                            }
                            else
                            {
                                newPairs.Add(subPair, pair.Value);
                            }
                        }
                    }
                }

                pairs = new Dictionary<string, long>(newPairs);
            }

            var molucules = GetMolecules(pairs);

            var max = (molucules.OrderByDescending(c => c.Value).First().Value / 2) + 1;
            var min = (molucules.OrderByDescending(c => c.Value).Last().Value / 2) + 1;

            return max - min;
        }

        private Dictionary<string, long> GetMolecules(Dictionary<string, long> pairs)
        {
            var molucules = new Dictionary<string, long>();
            foreach (var p in pairs)
            {
                foreach (var c in p.Key)
                {
                    var character = c.ToString();
                    if (molucules.ContainsKey(character))
                    {
                        molucules[character] = molucules[character] + p.Value;
                    }
                    else
                    {
                        molucules.Add(character, p.Value);
                    }
                }
            }
            return molucules;
        }

        private string UpdatePolymer(string polymer, List<KeyValuePair<int, string>> insertValues)
        {
            var increment = 0;

            foreach (var val in insertValues.OrderBy(kvp => kvp.Key))
            {
                polymer = polymer.Insert(val.Key + 1 + increment, val.Value);
            }

            return polymer;
        }
    }
}
