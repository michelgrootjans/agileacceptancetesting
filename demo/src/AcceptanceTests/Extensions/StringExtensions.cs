using System.Text;

namespace Snacks_R_Us.AcceptanceTests.Extensions
{
    public static class StringExtensions
    {
        public static string ToTitle(this string actual)
        {
            var firstLetterUpperCase = actual[0].ToString().ToUpper();
            return actual.ToLower().Remove(0, 1).Insert(0, firstLetterUpperCase);
        }

        public static string ToSentence(this string actual)
        {
            var result = new StringBuilder();
            foreach (var character in actual)
            {
                if (char.IsUpper(character))
                {
                    result.Append(" ");
                }
                result.Append(character);
            }
            return result.ToString().Trim();
        }
    }
}