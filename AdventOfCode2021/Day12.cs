using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day12 : Day.NewLineSplitParsed<string>
    {
        private readonly string[] SampleInput1 = new string[] { "start-A", "start-b", "A-c", "A-b", "b-d", "A-end", "b-end" };

        public override object ExecutePart1()
        {
            Input = SampleInput1;

            var caves = new HashSet<Cave>();

            foreach (var path in Input)
            {
                var nodes = path.Split('-');

                var firstCave = new Cave(nodes.First());
                var secondCave = new Cave(nodes.Last());
                
                if (!caves.Contains(firstCave))
                {
                    caves.Add(firstCave);
                }

                if (!caves.Contains(secondCave))
                {
                    caves.Add((secondCave));
                }

                var azw = caves.Select(c => c == firstCave).First();
            }

            return base.ExecutePart1();
        }

        public override object ExecutePart2()
        {
            return base.ExecutePart2();
        }
    }

    public class Cave
    {
        public Cave(string name)
        {
            Name = name;
            CaveType = this.Name.All(c => char.IsUpper(c)) ? CaveType.large : CaveType.small;
            ConnectedCaves = new List<Cave>();
        }

        public string Name { get; set; }

        public CaveType CaveType { get; set; }

        public List<Cave> ConnectedCaves { get; set; }
    }

    public enum CaveType
    {
        small,
        large
    }
}
