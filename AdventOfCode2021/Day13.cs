using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day13 : Day<(IEnumerable<dynamic>, IEnumerable<string>)>
    {
        private readonly string SampleInput = "6,10\n0,14\n9,10\n0,3\n10,4\n4,11\n6,0\n6,12\n4,1\n0,13\n10,12\n3,4\n3,0\n8,4\n1,10\n2,14\n8,10\n9,0\n\nfold along y=7\nfold along x=5";

        public override object ExecutePart1()
        {
            var coordinates = Input.Item1;

            var xLength = int.Parse(Input.Item2.First(f => f.Contains("x")).Split("=").Last()) * 2;
            var yLength = int.Parse(Input.Item2.First(f => f.Contains("y")).Split("=").Last()) * 2;

            var manual = new List<string>();
            foreach (var y in Enumerable.Range(0, yLength + 1))
            {
                var manualLine = string.Empty;
                foreach (var x in Enumerable.Range(0, xLength + 1))
                {
                    var c = coordinates.Any(c => c.x == x && c.y == y) ? "#" : ".";
                    manualLine = $"{manualLine}{c}";
                }
                manual.Add(manualLine);
            }

            manual = Fold(manual, Input.Item2.First());

            long dotCount = 0;
            foreach (var ln in manual)
            {
                dotCount += ln.Where(c => c.Equals('#')).Count();
            }

            return dotCount;
        }

        public override object ExecutePart2()
        {
            var coordinates = Input.Item1;
            var folds = Input.Item2;

            var xLength = int.Parse(folds.First(f => f.Contains("x")).Split("=").Last()) * 2;
            var yLength = int.Parse(folds.First(f => f.Contains("y")).Split("=").Last()) * 2;

            var manual = new List<string>();
            foreach (var y in Enumerable.Range(0, yLength + 1))
            {
                var manualLine = string.Empty;
                foreach (var x in Enumerable.Range(0, xLength + 1))
                {
                    var c = coordinates.Any(c => c.x == x && c.y == y) ? "#" : ".";
                    manualLine = $"{manualLine}{c}";
                }
                manual.Add(manualLine);
            }

            foreach(var fold in folds)
            {
                manual = Fold(manual, fold);
            }

            foreach(var line in manual)
            {
                Console.WriteLine(line);
            }

            return base.ExecutePart2();
        }

        public override (IEnumerable<dynamic>, IEnumerable<string>) ParseInput(string rawInput)
        {
            //rawInput = SampleInput;

            var splittedInput = rawInput.Split('\n');
            var indexOf = Array.IndexOf(splittedInput, "");

            var coordinates = new List<dynamic>();
            foreach (var i in Enumerable.Range(0, indexOf))
            {
                var c = splittedInput[i].Split(',');
                coordinates.Add(new { x = int.Parse(c.First()), y = int.Parse(c.Last()) });
            }

            var foldInstructions = splittedInput.Skip(indexOf + 1).Select(i => i.Split(" ").Last());

            return (coordinates, foldInstructions);
        }

        private List<string> Fold(List<string> manual, string foldInstruction)
        {
            var foldedManual = new List<string>();

            var instruction = foldInstruction.Split("=");
            var fold = int.Parse(instruction.Last());

            // Fold on X or Y?
            if (instruction.First().Equals("x"))
            {
                foreach(var ln in manual)
                {
                    var leftHalf = ln.Take(fold).ToArray();
                    var rightHalf = ln.Skip(fold + 1).Take(ln.Length - (fold - 1)).Reverse().ToArray();

                    var newLine = string.Empty;
                    foreach(var i in Enumerable.Range(0, leftHalf.Length))
                    {
                        var c = leftHalf[i].Equals('#') || rightHalf[i].Equals('#') ? "#" : ".";
                        newLine = $"{newLine}{c}";
                    }
                    foldedManual.Add(newLine);
                }
            }
            else
            {
                var topHalf = manual.Take(fold).ToArray();
                var bottomHalf = manual.Skip(fold + 1).Take(manual.Count - (fold - 1)).Reverse().ToArray();

                foreach (var i in Enumerable.Range(0, topHalf.Count()))
                {
                    var newLine = string.Empty;
                    foreach (var x in Enumerable.Range(0, topHalf.First().Length))
                    {
                        var c = topHalf[i][x].Equals('#') || bottomHalf[i][x].Equals('#') ? "#" : ".";                        
                        newLine = $"{newLine}{c}";
                    }
                    foldedManual.Add(newLine);
                }                
            }

            return foldedManual;
        }
    }
}
