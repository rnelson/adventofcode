namespace advent.Util.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s;

            return s.Length == 1 ? s.ToUpper() : $"{char.ToUpper(s[0])}{s.Substring(1)}";
        }
    }
}
