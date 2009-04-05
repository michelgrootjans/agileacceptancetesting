namespace Snacks_R_Us.Domain.Etensions
{
    public static class StringExtensions
    {
        public static long ToLong(this string value)
        {
            return long.Parse(value);
        }
    }
}