using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day15 : Day<int[,]>
    {
        // Sample input
        private readonly string SampleInput = "1163751742\n1381373672\n2136511328\n3694931569\n7463417111\n1319128137\n1359912421\n3125421639\n1293138521\n2311944581";

        public override int[,] ParseInput(string rawInput)
        {
            // Use Sample input
            rawInput = SampleInput;

            var input = rawInput.Split('\n');

            var output = new int[input.Length, input.First().Length];

            foreach (var i in Enumerable.Range(0, input.Length))
            {
                var line = input[i].ToCharArray();
                foreach (var j in Enumerable.Range(0, line.Length))
                {
                    output[i, j] = int.Parse(line[j].ToString());
                }
            }

            return output;
        }

        public override object ExecutePart1()
        {
            DrawGrid();



            return base.ExecutePart1();
        }

        private void DrawGrid()
        {
            for (int y = 0; y < 10; y++)
            {
                var ln = string.Empty;
                for (int x = 0; x < 10; x++)
                {
                    ln += $"{Input[y, x]}".PadLeft(3, ' ');
                }
                Console.WriteLine(ln);
            }
        }        
    }
}
