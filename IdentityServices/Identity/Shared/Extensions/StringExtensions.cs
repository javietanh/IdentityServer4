using System.Text.RegularExpressions;

namespace IdentityServices.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input) => string.IsNullOrEmpty(input)
                  ? input
                  : Regex.Match(input, @"^_+") + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}
