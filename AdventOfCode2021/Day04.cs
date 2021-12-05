using System.Text.RegularExpressions;
using Tidy.AdventOfCode;

namespace AzW.AdventOfCode2021.Year2021
{
    class Day04 : Day<dynamic>
    {
        public override object ExecutePart1()
        {
            foreach (string number in Input.drawnNumbers)
            {
                foreach (List<string[]> board in Input.boards)
                {
                    foreach (var i in Enumerable.Range(0, board.Count))
                    {
                        string pattern = String.Format(@"\b{0}\b", number);

                        board[i] = board[i].Select(l => Regex.Replace(l, pattern, string.Empty)).ToArray();

                        if (board[i].All(l => l.Equals(string.Empty)))
                        {
                            // BINGO!
                            var score = calculateScore(board, number);
                            return score;
                        }
                    }

                    // Check for horizontal BINGO
                    if (hasBingo(board))
                    {
                        var score = calculateScore(board, number);
                        return score;
                    }
                }
            }

            return base.ExecutePart1();
        }

        public override object ExecutePart2()
        {
            var boards = new List<Board>();

            foreach (List<string[]> board in Input.boards)
            {
                boards.Add(new Board(board));
            }

            foreach (string number in Input.drawnNumbers)
            {
                foreach (var board in boards.Where(b => b.HasBingo == false))
                {
                    board.StrikeNumber(number);

                    if (!boards.Any(b => b.HasBingo == false))
                    {
                        var orderedBoards = boards.OrderByDescending(b => b.MovesToBingo);
                        var finalScore =  orderedBoards.First().CalculateScore(number);
                        
                        return finalScore;
                    }
                }
            }

            return base.ExecutePart2();
        }

        private object calculateScore(List<string[]> board, string number)
        {
            var total = 0;

            foreach (var line in board)
            {
                var lineTotal = line.Where(l => l != String.Empty).Select(l => int.Parse(l)).Sum();
                total = total + lineTotal;
            }

            return total * int.Parse(number);
        }

        private bool hasBingo(List<string[]> board)
        {
            foreach (var i in Enumerable.Range(0, board.First().Length - 1))
            {
                var horizontalBingo = new List<string>();

                board.ForEach(l => horizontalBingo.Add(l[i]));

                if (horizontalBingo.All(x => x.Equals(string.Empty)))
                {
                    return true;
                }
            }
            return false;
        }

        public override dynamic ParseInput(string rawInput)
        {
            // Split the input on newLines
            var splittedRawInput = rawInput.Split('\n');

            // Get the drawn numbers 
            var drawnNumbers = splittedRawInput.First().Split(',').ToList();

            // Get the boards
            var boardsInput = splittedRawInput.Skip(2).ToArray();

            var boards = new List<List<string[]>>();
            var board = new List<string[]>();

            foreach (var i in Enumerable.Range(0, boardsInput.Length))
            {
                if (boardsInput[i].Equals(string.Empty))
                {
                    boards.Add(board);
                    board = new List<string[]>();
                    continue;
                }

                var sanatizedInput = Regex.Replace(boardsInput[i].Trim(), @"\s+", " ");

                board.Add(sanatizedInput.Split(" "));
            }
            boards.Add(board);

            return new { drawnNumbers = drawnNumbers, boards = boards };
        }
    }

    public class Board
    {
        public List<string[]> Lines { get; set; }
        public bool HasBingo { get; private set; }
        public int MovesToBingo { get; private set; } = 0;

        public Board(List<string[]> lines)
        {
            Lines = lines;
        }

        public void StrikeNumber(string number)
        {
            string pattern = String.Format(@"\b{0}\b", number);

            foreach (var i in Enumerable.Range(0, Lines.Count))
            {
                Lines[i] = Lines[i].Select(l => Regex.Replace(l, pattern, string.Empty)).ToArray();

                if (Lines[i].All(l => l.Equals(string.Empty)))
                {
                    // Horizontal BINGO!
                    this.HasBingo = true;
                }

                if (this.HasVerticalBingo())
                {
                    this.HasBingo = true;
                }
            }

            MovesToBingo++;
        }

        public object CalculateScore(string number)
        {
            var total = 0;

            foreach (var line in this.Lines)
            {
                var lineTotal = line.Where(l => l != String.Empty).Select(l => int.Parse(l)).Sum();
                total = total + lineTotal;
            }

            return total * int.Parse(number);
        }

        private bool HasVerticalBingo()
        {
            foreach (var i in Enumerable.Range(0, this.Lines.First().Length))
            {
                var horizontalBingo = new List<string>();

                this.Lines.ForEach(l => horizontalBingo.Add(l[i]));

                if (horizontalBingo.All(x => x.Equals(string.Empty)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
