using System.Text.RegularExpressions;

namespace GameGraph.Editor
{
    public static class PrettifyNameExtension
    {
        public static string PrettifyName(this string input)
        {
            return Regex.Replace(
                    Regex.Replace(
                        input.FirstLetterToUpperCase(),
                        @"(\P{Ll})(\P{Ll}\p{Ll})",
                        "$1 $2"
                    ),
                    @"(\p{Ll})(\P{Ll})",
                    "$1 $2"
                )
                .Trim()
                .FirstLetterToUpperCase();
        }

        public static string FirstLetterToUpperCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
