using Statiq.Common;

namespace Blog.Statiq.Helpers;

/// <summary>
///     Helpers to get access to translation configuration
/// </summary>
public static class LocalizationExtensions
{
    public static string GetLocalized(this IDocument? document, string key) => document.GetString(key);
    public static string GetLocalized(this IExecutionContext executionContext, string key) => executionContext.ToDocument().GetString(key);
    public static string GetDateFormat(this IDocument document) => document.GetString("date_format");
}