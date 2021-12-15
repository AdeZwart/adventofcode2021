using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day08 : Day.NewLineSplitParsed<string>
    {
        public override object ExecutePart1()
        {
            var simpleDigitCount = 0;

            foreach (var input in Input)
            {
                var output = input.Split('|').Last();

                foreach (var o in output.Split(" "))
                {
                    switch (o.Length)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 7:
                            simpleDigitCount++;
                            break;
                    }
                }
            }

            return simpleDigitCount;
        }

        public override object ExecutePart2()
        {
            // Sample input
            //Input = new string[]
            //{
            //    "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"
            //};

            //Input = new string[]
            //{
            //    "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
            //    "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc",
            //    "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
            //    "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
            //    "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
            //    "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
            //    "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
            //    "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
            //    "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
            //    "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
            //};

            var outputValues = new List<int>();

            foreach (var input in Input)
            {
                var decodeMap = new string[10];

                var pattern = input.Split('|').First().Trim();
                var output = input.Split('|').Last().Trim();

                var patternParts = pattern.Split(' ');

                decodeMap[1] = patternParts.Where(p => p.Length == 2).First();
                decodeMap[4] = patternParts.Where(p => p.Length == 4).First();
                decodeMap[7] = patternParts.Where(p => p.Length == 3).First();
                decodeMap[8] = patternParts.Where(p => p.Length == 7).First();

                decodeMap[6] = patternParts.Where(p => p.Length == 6 && decodeMap[1].Except(p.ToCharArray()).Any()).First();
                decodeMap[9] = patternParts.Where(p => p.Length == 6 && p != decodeMap[6] && !decodeMap[4].Except(p.ToCharArray()).Any()).First();
                decodeMap[0] = patternParts.Where(p => p.Length == 6 && p != decodeMap[6] && p != decodeMap[9]).First();

                decodeMap[3] = patternParts.Where(p => p.Length == 5 && !decodeMap[1].Except(p.ToCharArray()).Any()).First();
                decodeMap[2] = patternParts.Where(p => p.Length == 5 && p != decodeMap[3] && decodeMap[4].Except(p.ToCharArray()).Count() > 1).First();
                decodeMap[5] = patternParts.Where(p => p.Length == 5 && p != decodeMap[3] && p != decodeMap[2]).First();

                for (var i = 0; i < decodeMap.Length; i++)
                {
                    decodeMap[i] = decodeMap[i].Sort();
                }

                var outputValue = new List<string>();
                foreach (var o in output.Split(" "))
                {
                    var val = Array.IndexOf(decodeMap, o.Sort());

                    outputValue.Add(val.ToString());
                }

                outputValues.Add(int.Parse(string.Join("", outputValue)));
            }

            return outputValues.Sum();
        }
    }
}
