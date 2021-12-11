using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day10 : Day.NewLineSplitParsed<string>
    {
        private readonly char[] OpeningChars = new[] { '(', '<', '[', '{' };
        private readonly char[] ClosingChars = new[] { ')', '>', ']', '}' };

        public override object ExecutePart1()
        {
            // Sample Input 
            //Input = new string[]
            //{
            //    "[({(<(())[]>[[{[]{<()<>>",
            //    "[(()[<>])]({[<{<<[]>>(",
            //    "{([(<{}[<>[]}>{[]{[(<()>",
            //    "(((({<>}<{<{<>}{[]{[]{}",
            //    "[[<[([]))<([[{}[[()]]]",
            //    "[{[{({}]{}}([{[{{{}}([]",
            //    "{<[[]]>}<{[{[{[]{()[[[]",
            //    "[<(<(<(<{}))><([]([]()",
            //    "<{([([[(<>()){}]>(<<{{",
            //    "<{([{{}}[<[[[<>{}]]]>[]]"
            //};

            long totalSyntaxErrorScore = 0;

            foreach (var line in Input)
            {
                var result = ParseLine(line);

                if (result.Length == 1)
                {
                    switch (result)
                    {
                        case ")":
                            totalSyntaxErrorScore += 3;
                            break;

                        case "]":
                            totalSyntaxErrorScore += 57;
                            break;

                        case "}":
                            totalSyntaxErrorScore += 1197;
                            break;

                        case ">":
                            totalSyntaxErrorScore += 25137;
                            break;
                    }
                }
                else
                {
                    // Discard incomplete entries
                }
            }

            return totalSyntaxErrorScore;
        }

        public override object ExecutePart2()
        {
            // Sample Input 
            //Input = new string[]
            //{
            //    "[({(<(())[]>[[{[]{<()<>>",
            //    "[(()[<>])]({[<{<<[]>>(",
            //    "{([(<{}[<>[]}>{[]{[(<()>",
            //    "(((({<>}<{<{<>}{[]{[]{}",
            //    "[[<[([]))<([[{}[[()]]]",
            //    "[{[{({}]{}}([{[{{{}}([]",
            //    "{<[[]]>}<{[{[{[]{()[[[]",
            //    "[<(<(<(<{}))><([]([]()",
            //    "<{([([[(<>()){}]>(<<{{",
            //    "<{([{{}}[<[[[<>{}]]]>[]]"
            //};

            var autoCompletionScores = new List<long>();

            foreach (var line in Input)
            {
                var result = ParseLine(line);

                if (result.Length == 1)
                {
                    // Discard corrupted lines
                }
                else
                {
                    long autoCompleteScore = 0;
                    foreach(var c in result.Reverse())
                    {
                        switch(c)
                        {
                            case '(':
                                autoCompleteScore = (autoCompleteScore * 5) + 1;
                                break;

                            case '[':
                                autoCompleteScore = (autoCompleteScore * 5) + 2;
                                break;

                            case '{':
                                autoCompleteScore = (autoCompleteScore * 5) + 3;
                                break;

                            case '<':
                                autoCompleteScore = (autoCompleteScore * 5) + 4;
                                break;
                        }
                    }
                    autoCompletionScores.Add(autoCompleteScore);
                }
            }

            var scores = autoCompletionScores.OrderBy(x => x).ToArray();

            return scores[scores.Length/2];            
        }

        // 40 (     60 <    91 [    123 {
        // 41 )     62 >    93 ]    125 }
        private string ParseLine(string line)
        {
            if (line.Length == 1)
            {
                return line;
            }

            if (!line.Intersect(ClosingChars).Any())
            {
                return line;
            }

            foreach (var i in Enumerable.Range(0, line.Length))
            {
                try
                {
                    if (OpeningChars.Contains(line[i]) && ClosingChars.Contains(line[i + 1]))
                    {
                        // Found open/close pair!

                        if (line[i] - line[i + 1] == -1 || line[i] - line[i + 1] == -2)
                        {
                            line = line.Remove(i + 1, 1);
                            line = line.Remove(i, 1);

                            break;
                        }
                        else
                        {
                            // Illegal pair detected!
                            return line[i + 1].ToString();
                        }

                    }
                }
                catch (IndexOutOfRangeException)
                {
                    // We've reached the end of the line
                }
                catch (Exception ex)
                {
                    var abc = ex;
                }
            }

            return ParseLine(line);
        }
    }
}
