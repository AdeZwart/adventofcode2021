using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day11 : Day<int[,]>
    {
        private long FlashCount = 0;

        public override object ExecutePart1()
        {
            //Console.WriteLine(" Before any steps:");
            //DrawGrid();
            //Console.WriteLine(' ');

            for (var step = 1; step <= 100; step++)
            {
                IncrementOctopusses()
;
                foreach (var y in Enumerable.Range(0, Input.GetLength(0)))
                {
                    foreach (var x in Enumerable.Range(0, Input.GetLength(1)))
                    {
                        //Input[y, x]++;
                        if (Input[y, x] == 10)
                        {
                            FlashCount++;
                            Flash(y, x);
                        }
                    }
                }

                ResetFlashedOctopusses();

                //Console.WriteLine($"After step {step}:");
                //DrawGrid();
                //Console.WriteLine(' ');
            }

            return FlashCount;
        }

        public override object ExecutePart2()
        {
            var totalOctopusCount = Input.GetLength(0) * Input.GetLength(1);

            //Console.WriteLine(" Before any steps:");
            //DrawGrid();
            //Console.WriteLine(' ');

            for (var step = 1; ; step++)
            {
                IncrementOctopusses()
;
                foreach (var y in Enumerable.Range(0, Input.GetLength(0)))
                {
                    foreach (var x in Enumerable.Range(0, Input.GetLength(1)))
                    {
                        //Input[y, x]++;
                        if (Input[y, x] == 10)
                        {
                            FlashCount++;
                            Flash(y, x);
                        }
                    }
                }

                var flashCount = CountFlashedOctopusses();
                if (flashCount == totalOctopusCount)
                {
                    return step;
                }

                ResetFlashedOctopusses();

                //Console.WriteLine($"After step {step}:");
                //DrawGrid();
                //Console.WriteLine(' ');
            }
        }

        public override int[,] ParseInput(string rawInput)
        {
            // Sample Input
            rawInput = "5483143223\n2745854711\n5264556173\n6141336146\n6357385478\n4167524645\n2176841721\n6882881134\n846848554\n5283751526";

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

        private void Flash(int y, int x)
        {
            var positions = new List<dynamic>()
            {
                // NorthWest
                new {y=y-1, x=x-1},
                // North
                new {y=y-1, x},
                // NorthEast
                new {y=y-1, x=x+1},
                // West
                new { y, x=x-1},
                // East
                new { y, x=x+1},
                // SouthWest
                new {y=y+1, x=x-1},
                // South
                new {y=y+1, x},
                // SouthEast
                new {y=y+1, x=x+1}
            };

            //Console.WriteLine($"FlashCount:{FlashCount}");
            //Console.WriteLine($"FLASH! Y={y},X={x}");
            //DrawGridWithFlash(positions, ConsoleColor.White);
            // increment
            foreach (var p in positions)
            {
                try
                {
                    // Don't increas if the octopus still has to flash
                    if (Input[p.y, p.x] != 10)
                    {
                        Input[p.y, p.x]++;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    // We've reached the edge of the grid
                }
            }

            //DrawGridWithFlash(positions, ConsoleColor.Red);
            Input[y, x]++;

            foreach (var p in positions)
            {
                try
                {
                    if (Input[p.y, p.x] == 10)
                    {
                        FlashCount++;
                        Flash(p.y, p.x);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    // We've reached the edge of the grid
                }
            }
        }

        private void IncrementOctopusses()
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Input[y, x]++;
                }
            }
        }

        private long CountFlashedOctopusses()
        {
            long count = 0;

            for (int y = 0; y < 10; y++)
            {
                var ln = string.Empty;
                for (int x = 0; x < 10; x++)
                {

                    if (Input[y, x] > 9)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void ResetFlashedOctopusses()
        {
            for (int y = 0; y < 10; y++)
            {
                var ln = string.Empty;
                for (int x = 0; x < 10; x++)
                {
                    Input[y, x] = Input[y, x] > 9 ? 0 : Input[y, x];
                }
            }
        }

        private void DrawGridWithFlash(List<dynamic> positions, ConsoleColor color)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (positions.Contains(new { y, x }))
                    {
                        Console.BackgroundColor = color;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write($"{Input[y, x]}".PadLeft(3, ' '));
                    Console.ResetColor();
                }
                Console.Write('\n');
            }
            Console.Write('\n');
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
