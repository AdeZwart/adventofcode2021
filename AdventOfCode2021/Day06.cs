using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day06 : Day<IEnumerable<int>>
    {
        public const int NEW_FISH_CYCLE = 6;

        public override object ExecutePart1()
        {
            // Sample input
            // int iterationDays = 18;
            // Input = new int[] { 3, 4, 3, 1, 2 };

            int iterationDays = 80;

            var fishes = new List<LanternFish>();

            foreach (var f in Input)
            {
                fishes.Add(new LanternFish(f));
            }

            for (var i = 0; i < iterationDays; i++)
            {
                var newFishes = new List<LanternFish>();

                fishes.ForEach(f => f.DaysTillNewFish--);

                foreach (var fish in fishes.Where(f => f.DaysTillNewFish < 0))
                {
                    fish.DaysTillNewFish = NEW_FISH_CYCLE;
                    newFishes.Add(new LanternFish());
                }

                fishes.AddRange(newFishes);

                // Console.WriteLine($"{i} Day(s): {string.Join(", ", fishes.Select(f => f.ToString()))}");
            }

            var fishCount = fishes.Count();

            return fishCount;
        }

        public override object ExecutePart2()
        {
            // Sample input
            // int iterationDays = 256;
            // Input = new int[] { 3, 4, 3, 1, 2 };

            int iterationDays = 256;

            var ageMap = new long[9];

            // Set initial values
            foreach (var f in Input)
            {
                ageMap[f]++;
            }

            // Debug print
            Console.WriteLine("==========");
            foreach (var j in Enumerable.Range(0, ageMap.Length))
            {
                Console.WriteLine($"Age {j} count = {ageMap[j]}");
            }
            Console.WriteLine("==========");

            for (var i = 0; i < iterationDays; i++)
            {
                var newFish = ageMap[0];

                foreach (var j in Enumerable.Range(0, ageMap.Length))
                {
                    ageMap[j] = (j == ageMap.Length - 1) ? newFish : ageMap[j] = ageMap[j + 1];
                    
                    if (j == NEW_FISH_CYCLE)
                    {
                        ageMap[j] += newFish;
                    }
                    
                    Console.WriteLine($"Age {j} count = {ageMap[j]}");
                }

                Console.WriteLine("==========");
            }

            return ageMap.Sum();      
        }

        public override IEnumerable<int> ParseInput(string rawInput)
        {
            return rawInput.Split(',').Select(i => int.Parse(i));
        }
    }

    public class LanternFish
    {
        private const int INITIAL_NEW_FISH_CYCLE = 8;

        public int DaysTillNewFish { get; set; }

        public LanternFish()
        {
            DaysTillNewFish = INITIAL_NEW_FISH_CYCLE;
        }

        public LanternFish(int newFishCycle)
        {
            DaysTillNewFish = newFishCycle;
        }

        public override string ToString()
        {
            return this.DaysTillNewFish.ToString();
        }
    }
}
