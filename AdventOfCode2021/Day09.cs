using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day09 : Day<int[,]>
    {
        public override object ExecutePart1()
        {
            var risk = new List<int>();

            for (var lineIndex = 0; lineIndex < Input.GetLength(0); lineIndex++)
            {
                for (var locationIndex = 0; locationIndex < Input.GetLength(1); locationIndex++)
                {
                    var location = Input[lineIndex, locationIndex];

                    if (IsLowPoint(location, lineIndex, locationIndex))
                    {
                        risk.Add(location + 1);
                    }
                }
            }

            return risk.Sum();
        }

        public override object ExecutePart2()
        {
            var basins = new List<long>();

            for (var lineIndex = 0; lineIndex < Input.GetLength(0); lineIndex++)
            {
                for (var locationIndex = 0; locationIndex < Input.GetLength(1); locationIndex++)
                {
                    var location = Input[lineIndex, locationIndex];

                    if (IsLowPoint(location, lineIndex, locationIndex))
                    {
                        var basin = new HashSet<(int, int)>();

                        basin.Add((lineIndex, locationIndex));

                        GetBasin(basin, lineIndex, locationIndex);

                        basins.Add(basin.Count);
                    }
                }
            }

            var largestBasins = basins.OrderByDescending(b => b).Take(3);

            return largestBasins.Skip(0).First() * largestBasins.Skip(1).First() * largestBasins.Skip(2).First();
        }

        public override int[,] ParseInput(string rawInput)
        {
            // Sample input
            //rawInput = "2199943210\n3987894921\n9856789892\n8767896789\n9899965678";

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

        private bool IsLowPoint(int location, int y, int x)
        {
            // check up
            try
            {
                if (Input[y - 1, x] <= location)
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException) { }

            // check right
            try
            {
                if (Input[y, x + 1] <= location)
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException) { }

            // check down
            try
            {
                if (Input[y + 1, x] <= location)
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException) { }

            // check left
            try
            {
                if (Input[y, x - 1] <= location)
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException) { }

            // All adjacent locations where higher
            return true;
        }

        private HashSet<(int, int)> GetBasin(HashSet<(int, int)> basin, int y, int x)
        {
            // Check up
            var upY = y - 1;
            if (GetNext(upY, x) < 9 && !basin.Contains((upY, x)))
            {
                basin.Add((upY, x));
                GetBasin(basin, upY, x);
            }

            // Check right
            var rightX = x + 1;
            if (GetNext(y, rightX) < 9 && !basin.Contains((y, rightX)))
            {
                basin.Add((y, rightX));
                GetBasin(basin, y, rightX);
            }

            // Check down
            var downY = y + 1;
            if (GetNext(downY, x) < 9 && !basin.Contains((downY, x)))
            {
                basin.Add((downY, x));
                GetBasin(basin, downY, x);
            }

            // Check left
            var leftX = x - 1;
            if (GetNext(y, leftX) < 9 && !basin.Contains((y, leftX)))
            {
                basin.Add((y, leftX));
                GetBasin(basin, y, leftX);
            }

            return basin;
        }

        private int GetNext(int y, int x)
        {
            try
            {
                return Input[y, x];
            }
            catch (IndexOutOfRangeException)
            {
                return 9;
            }
        }
    }

}
