using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    public class Day03 : Day.NewLineSplitParsed<string>
    {
        public override object ExecutePart1()
        {
            var binaryGammaRate = string.Empty;
            var binaryEpsilonRate = string.Empty;

            foreach (var i in Enumerable.Range(0, Input.First().Length))
            {
                var rslt = new List<char>();

                Input.ToList().ForEach(inp => rslt.Add(inp.Skip(i).First()));

                var onCount = rslt.Where(x => x.Equals('1')).Count();
                var offCount = rslt.Where(x => x.Equals('0')).Count();

                if (onCount > offCount)
                {
                    binaryGammaRate = $"{binaryGammaRate}1";
                    binaryEpsilonRate = $"{binaryEpsilonRate}0";
                }
                else
                {
                    binaryGammaRate = $"{binaryGammaRate}0";
                    binaryEpsilonRate = $"{binaryEpsilonRate}1";
                }

            }

            return Convert.ToInt32(binaryGammaRate, 2) * Convert.ToInt32(binaryEpsilonRate, 2);
        }

        public override object ExecutePart2()
        {
            var oxygenGeneratorRating = Input.ToList();
            foreach (var i in Enumerable.Range(0, oxygenGeneratorRating.First().Length))
            {
                var rslt = new List<char>();

                oxygenGeneratorRating.ForEach(inp => rslt.Add(inp.Skip(i).First()));

                oxygenGeneratorRating = rslt.Where(x => x.Equals('1')).Count() >= rslt.Where(x => x.Equals('0')).Count()
                    ? oxygenGeneratorRating.Where(x => x.Skip(i).First().Equals('1')).ToList()
                    : oxygenGeneratorRating.Where(x => x.Skip(i).First().Equals('0')).ToList();

                if (oxygenGeneratorRating.Count == 1)
                {
                    break;
                }
            }

            var co2ScrubbingRating = Input.ToList();
            foreach (var i in Enumerable.Range(0, co2ScrubbingRating.First().Length))
            {
                var rslt = new List<char>();

                co2ScrubbingRating.ForEach(inp => rslt.Add(inp.Skip(i).First()));

                co2ScrubbingRating = rslt.Where(x => x.Equals('0')).Count() > rslt.Where(x => x.Equals('1')).Count()
                    ? co2ScrubbingRating.Where(x => x.Skip(i).First().Equals('1')).ToList()
                    : co2ScrubbingRating.Where(x => x.Skip(i).First().Equals('0')).ToList();

                if (co2ScrubbingRating.Count == 1)
                {
                    break;
                }
            }

            return Convert.ToInt32(oxygenGeneratorRating.First(), 2) * Convert.ToInt32(co2ScrubbingRating.First(), 2);
        }
    }
}
