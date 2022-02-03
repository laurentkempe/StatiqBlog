namespace Blog.Statiq.Helpers;

public static class StringExtensions
{
    public static string WithFirstLetterUppercase(this string input)
    {
        //todo rework this crap
        var firstLetterUpperCase = input.AsSpan()[0].ToString().ToUpperInvariant();
        var remainingSpan = input.AsSpan()[1..];
       
        return $"{firstLetterUpperCase}{remainingSpan}";
    }
}