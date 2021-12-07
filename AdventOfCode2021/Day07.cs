using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day07 : Day<IEnumerable<int>>
    {
        public override object ExecutePart1()
        {
            Input = Input.OrderBy(x => x);

            var min = Input.Min();
            var max = Input.Max();

            var totalFuelForEachPosition = new List<KeyValuePair<int, int>>();

            foreach(var targetPosition in Enumerable.Range(min, max - min))
            {
                var fuelToPosition = new List<int>();
                foreach(var crabPosition in Input)
                {
                    fuelToPosition.Add(Math.Abs(crabPosition - targetPosition));
                }
                totalFuelForEachPosition.Add(new KeyValuePair<int, int>(targetPosition, fuelToPosition.Sum()));
            }

            totalFuelForEachPosition = totalFuelForEachPosition.OrderBy(f => f.Value).ToList();

            return totalFuelForEachPosition.First().Value;            
        }

        public override object ExecutePart2()
        {
            Input = Input.OrderBy(x => x);

            var min = Input.Min();
            var max = Input.Max();

            var positionMap = new long[max];

            foreach (var crab in Input)
            {
                foreach (var targetPosition in Enumerable.Range(min, max - min))
                {
                    foreach(var move in Enumerable.Range(1, Math.Abs(crab - targetPosition)))
                    {
                        positionMap[targetPosition] += move;
                    }
                }
            }

            positionMap = positionMap.OrderBy(x => x).ToArray();

            return positionMap.First();
        }

        public override IEnumerable<int> ParseInput(string rawInput)
        {
            // Sample input
            //rawInput = "16,1,2,0,4,2,7,1,2,14";

            return rawInput.Split(',').Select(int.Parse);
        }
    }
}
