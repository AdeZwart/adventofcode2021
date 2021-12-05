using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day05 : Day<List<dynamic>>
    {
        public override object ExecutePart1()
        {
            var boundary = GetBoundary(Input);

            var grid = new int[boundary, boundary];

            foreach (var inp in Input)
            {
                if (inp.Start.X == inp.Stop.X)
                {
                    var x = inp.Start.X;
                    var start = inp.Start.Y < inp.Stop.Y ? inp.Start.Y : inp.Stop.Y;
                    var count = Math.Abs(inp.Start.Y - inp.Stop.Y) + 1;

                    // Draw on Y
                    foreach (var y in Enumerable.Range(start, count))
                    {
                        grid[y, x]++;
                    }
                }
                else if (inp.Start.Y == inp.Stop.Y)
                {
                    var y = inp.Start.Y;
                    var start = inp.Start.X < inp.Stop.X ? inp.Start.X : inp.Stop.X;
                    var count = Math.Abs(inp.Start.X - inp.Stop.X) + 1;

                    // Draw on X
                    foreach (var x in Enumerable.Range(start, count))
                    {
                        grid[y, x]++;
                    }
                }
            }

            var totalIntersectCount = 0;
            foreach (var g in grid)
            {
                if (g > 1)
                {
                    totalIntersectCount++;
                }
            }

            return totalIntersectCount;
        }

        public override object ExecutePart2()
        {
            var boundary = GetBoundary(Input);

            var grid = new int[boundary, boundary];

            foreach (var inp in Input)
            {
                if (inp.Start.X == inp.Stop.X)
                {
                    var x = inp.Start.X;
                    var start = inp.Start.Y < inp.Stop.Y ? inp.Start.Y : inp.Stop.Y;
                    var count = Math.Abs(inp.Start.Y - inp.Stop.Y) + 1;

                    // Draw on Y
                    foreach (var y in Enumerable.Range(start, count))
                    {
                        grid[y, x]++;
                    }
                }
                else if (inp.Start.Y == inp.Stop.Y)
                {
                    var y = inp.Start.Y;
                    var start = inp.Start.X < inp.Stop.X ? inp.Start.X : inp.Stop.X;
                    var count = Math.Abs(inp.Start.X - inp.Stop.X) + 1;

                    // Draw on X
                    foreach (var x in Enumerable.Range(start, count))
                    {
                        grid[y, x]++;
                    }
                }
                else
                {                    
                    int[] xRange = GetRange(inp.Start.X, inp.Stop.X);
                    int[] yRange = GetRange(inp.Start.Y, inp.Stop.Y);
                    foreach(var i in Enumerable.Range(0, xRange.Length))
                    {
                        grid[yRange[i], xRange[i]]++;
                    }
                }
            }

            //DrawGrid(grid, boundary);

            var totalIntersectCount = 0;
            foreach (var g in grid)
            {
                if (g > 1)
                {
                    totalIntersectCount++;
                }
            }

            return totalIntersectCount;
        }

        public override List<dynamic> ParseInput(string rawInput)
        {
            var splittedRawInput = rawInput.Split('\n');

            var coordinates = new List<dynamic>();

            foreach (var input in splittedRawInput)
            {
                var startCoordinates = input.Split("->").First().Trim().Split(',');
                var start = new { X = int.Parse(startCoordinates.First()), Y = int.Parse(startCoordinates.Last()) };

                var endCoordinates = input.Split("->").Last().Trim().Split(',');
                var stop = new { X = int.Parse(endCoordinates.First()), Y = int.Parse(endCoordinates.Last()) };

                coordinates.Add(new { Start = start, Stop = stop });
            }

            return coordinates;
        }

        private int GetBoundary(List<dynamic> input)
        {
            var max = 0;

            foreach (var inputItem in input)
            {
                max = inputItem.Start.X > max ? inputItem.Start.X : max;
                max = inputItem.Start.Y > max ? inputItem.Start.Y : max;
                max = inputItem.Stop.X > max ? inputItem.Stop.X : max;
                max = inputItem.Stop.Y > max ? inputItem.Stop.Y : max;
            }

            return max + 1;
        }

        private int[] GetRange(int start, int stop)
        {
            var range = new List<int>();

            if (start > stop)
            {
                for (var i = start; i >= stop; i--)
                {
                    range.Add(i);
                }
            }
            else
            {
                for (var i = start; i <= stop; i++)
                {
                    range.Add(i);
                }
            }

            return range.ToArray();
        }

        private void DrawGrid(int[,] grid, int boundary)
        {
            for (int i = 0; i < boundary; i++)
            {
                var ln = string.Empty;
                for (int j = 0; j < boundary; j++)
                {
                    ln += $" {grid[i, j]}";
                }
                Console.WriteLine(ln);
            }
        }
    }
}
