using System.Text.RegularExpressions;

namespace AzW.AdventOfCode2021.Year2021
{
    public static class StringExtensions
    {
        public static string Sort(this string input)
        {
            return string.Concat(input.OrderBy(c => c));
        }

        public static IEnumerable<int> GetAllIndexes(this string source, string matchString)
        {
            matchString = Regex.Escape(matchString);
            foreach (Match match in Regex.Matches(source, matchString))
            {
                yield return match.Index;
            }
        }
    }
}
