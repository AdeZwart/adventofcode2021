using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    public class Day02 : Day.NewLineSplitParsed<string>
    {
        public override object ExecutePart1()
        {
            var horizontal = 0;
            var depth = 0;

            foreach(var input in Input)
            {
                var movement = input.Split(" ").First();
                var unit = int.Parse(input.Split(" ").Last());
                
                switch (movement)
                {
                    case "forward":
                        horizontal += unit;
                        break;

                    case "down":
                        depth += unit;
                        break;

                    case "up":
                        depth -= unit;
                        break; 
                }
            }

            return horizontal * depth;
        }

        public override object ExecutePart2()
        {
            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            foreach (var input in Input)
            {
                var movement = input.Split(" ").First();
                var unit = int.Parse(input.Split(" ").Last());

                switch (movement)
                {
                    case "forward":
                        horizontal += unit;
                        depth += (unit * aim);
                        break;

                    case "down":
                        aim += unit;
                        break;

                    case "up":
                        aim -= unit;
                        break;
                }
            }

            return horizontal * depth;
        }
    }
}
