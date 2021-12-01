using Tidy.AdventOfCode;

namespace AzW.AdventOfCode.Year2021
{
    class Day1 : Day.NewLineSplitParsed<int>
    {        
        public override object ExecutePart1()
        {            
            var largerMeasurementsCount = 0;
            int? previousMeasure = null;

            foreach (var i in Enumerable.Range(0, Input.Length))
            {
                var measure = Input[i];

                if (i == 0)
                {
                    previousMeasure = measure;
                    continue;
                }

                if (measure > previousMeasure)
                {
                    largerMeasurementsCount++;
                }

                previousMeasure = measure;
            }

            return largerMeasurementsCount;
        }

        public override object ExecutePart2()
        {
            var largerMeasurementsCount = 0;
            int? previousMeasure = null;

            foreach (var i in Enumerable.Range(0, Input.Length))
            {
                var measure = Input.Skip(i).Take(3).Sum();

                if (i == 0)
                {
                    previousMeasure = measure;
                    continue;
                }

                if (measure > previousMeasure)
                {
                    largerMeasurementsCount++;
                }

                previousMeasure = measure;
            }

            return largerMeasurementsCount;
        }
    }
}