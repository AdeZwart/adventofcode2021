using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var movement = input.Split(" ");

                switch (movement.First())
                {
                    case "forward":
                        horizontal += int.Parse(movement.Last());
                        break;

                    case "down":
                        depth += int.Parse(movement.Last());
                        break;

                    case "up":
                        depth -= int.Parse(movement.Last());
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
                var movement = input.Split(" ");

                switch (movement.First())
                {
                    case "forward":
                        horizontal += int.Parse(movement.Last());
                        depth += (int.Parse(movement.Last()) * aim);
                        break;

                    case "down":
                        aim += int.Parse(movement.Last());
                        break;

                    case "up":
                        aim -= int.Parse(movement.Last());
                        break;
                }
            }

            return horizontal * depth;
        }
    }
}
