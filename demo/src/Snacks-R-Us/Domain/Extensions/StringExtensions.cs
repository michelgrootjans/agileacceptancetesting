namespace Snacks_R_Us.Domain.Etensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool ContainsCharacters(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static long ToLong(this string value)
        {
            return long.Parse(value);
        }
    }
}