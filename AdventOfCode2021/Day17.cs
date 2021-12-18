using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day17 : Day<(IEnumerable<int>, IEnumerable<int>)>
    {
        private readonly string SampleInput = "target area: x=20..30, y=-10..-5";
        private int highestY = 0;

        public override object ExecutePart1()
        {
            var xVelocities = GetViableXVelosity(Input.Item1);

            for (int y = 0; y <= Math.Abs(Input.Item2.Min()); y++)
            {
                foreach (var x in xVelocities)
                {
                    var launchResult = LaunchProbe(x, y);
                }
            }

            Console.WriteLine($"\nHighest position reached: {highestY}\n");

            return highestY;
            //return base.ExecutePart1();
        }

        public override object ExecutePart2()
        {
            var xVelocities = GetViableXVelosity(Input.Item1);

            var hits = new List<string>();
            for (int y = 0; y <= Math.Abs(Input.Item2.Min()); y++)
            {

                foreach (var x in xVelocities)
                {
                    var launchResult = LaunchProbe(x, y);
                    switch (launchResult)
                    {
                        case LaunchResult.miss:
                        case LaunchResult.pass:
                            break;
                        case LaunchResult.hit:
                            hits.Add($"{x},{y}");
                            break;
                    }
                }
            }

            for (int y = -1; y >= Input.Item2.Min(); y--)
            {
                foreach (var x in xVelocities)
                {
                    var launchResult = LaunchProbe(x, y);
                    switch (launchResult)
                    {
                        case LaunchResult.miss:
                            break;
                        case LaunchResult.pass:
                            break;
                        case LaunchResult.hit:
                            hits.Add($"{x},{y}");
                            break;
                    }
                }
            }

            return hits.Count();
            // base.ExecutePart2();
        }

        public override (IEnumerable<int>, IEnumerable<int>) ParseInput(string rawInput)
        {
            //rawInput = SampleInput;

            var coordinates = rawInput.Split(':').Last().Trim().Split(',');

            var xCoordinates = coordinates.First().Split('=').Last().Split("..").Select(c => int.Parse(c));
            var xSize = xCoordinates.Max() - xCoordinates.Min();

            var yCoordinates = coordinates.Last().Split('=').Last().Split("..").Select(c => int.Parse(c));
            var ySize = yCoordinates.Max() - yCoordinates.Min();

            return (Enumerable.Range(xCoordinates.Min(), xSize + 1), Enumerable.Range(yCoordinates.Min(), ySize + 1));
        }

        private List<int> GetViableXVelosity(IEnumerable<int> xRange)
        {
            var velocities = new List<int>();

            for (var i = 0; i <= xRange.Max(); i++)
            {
                var startVelocity = i;
                var total = startVelocity;

                while (startVelocity > 0)
                {
                    if (xRange.Contains(total))
                    {
                        velocities.Add(i);
                        break;
                    }

                    if (total > xRange.Max())
                    {
                        break;
                    }

                    startVelocity--;
                    total += startVelocity;
                }
            }

            return velocities;
        }

        private LaunchResult LaunchProbe(int xVelocity, int yVelocity)
        {
            Console.WriteLine($"LAUNCH: xVelocity={xVelocity}, yVelocity={yVelocity}");

            var xProbeLocation = 0;
            var yProbeLocation = 0;

            var highestYforLaunch = -1;

            for (var step = 1; ; step++)
            {
                xProbeLocation += xVelocity;
                yProbeLocation += yVelocity;

                if (yProbeLocation > highestYforLaunch)
                {
                    highestYforLaunch = yProbeLocation;
                }

                //Console.WriteLine($"Step {step}, Probe location: x={xProbeLocation},y={yProbeLocation}");

                if (Input.Item1.Contains(xProbeLocation) && Input.Item2.Contains(yProbeLocation))
                {
                    Console.WriteLine("HIT");
                    Console.WriteLine($"Launch peeked at {highestYforLaunch}");

                    // HIT the target
                    if (highestYforLaunch > highestY)
                    {
                        highestY = highestYforLaunch;
                        Console.WriteLine(' ');
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"HIGHEST LAUNCH TO DATE!");
                        Console.ResetColor();
                        Console.Write('\n');
                    }

                    return LaunchResult.hit;
                }

                if (xProbeLocation == 0 && yProbeLocation < Input.Item2.Min())
                {
                    Console.WriteLine("Launched to short\n\n");
                    // We missed the target short
                    return LaunchResult.miss;
                }

                if (xProbeLocation < Input.Item1.Min() && yProbeLocation < Input.Item2.Min())
                {
                    Console.WriteLine("Launched to short\n\n");
                    // We missed the target short
                    return LaunchResult.miss;
                }

                if (xProbeLocation > Input.Item1.Max() || yProbeLocation < Input.Item2.Min())
                {
                    Console.WriteLine($"Launch passed the target");
                    // We passed the target
                    return LaunchResult.pass;
                }

                // Decrease velocity
                if (xVelocity > 0) { xVelocity--; };
                yVelocity--;
            }
        }

        enum LaunchResult
        {
            miss,
            hit,
            pass
        }
    }
}
