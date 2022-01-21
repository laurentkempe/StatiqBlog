namespace Blog.Helpers;

public static class StringExtensions
{
    public static string WithFirstLetterUppercase(this string input)
    {
        var firstLetterUpperCase = input.AsSpan()[0].ToString().ToUpperInvariant();
        var remainingSpan = input.AsSpan()[1..];
       
        return $"{firstLetterUpperCase}{remainingSpan}";
    }
}