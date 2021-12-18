using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day18 : Day.NewLineSplitParsed<string>
    {
        public override object ExecutePart1()
        {
            // Reduce a snailfish number
            // If any pair is nested inside four pairs, the leftmost such pair explodes.
            // If any regular number is 10 or greater, the leftmost such regular number splits.
            
            return base.ExecutePart1();
        }

        private string ReduceSnailfishNumber(string snailfishNumber)
        {
            return snailfishNumber;
        }
    }
}
