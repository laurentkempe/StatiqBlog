using System.Globalization;
using Statiq.Common;
using Statiq.Razor;

namespace Blog.Statiq.Helpers;

/// <summary>
///     Helpers to get access to translation configuration
/// </summary>
public static class TranslationExtensions
{
    public static string GetLocalized(this StatiqRazorPage<IDocument> page, string key)
    {
        var keys = key.Split(".", StringSplitOptions.RemoveEmptyEntries);

        var translation = GetTranslation(page);

        return keys.Length switch
        {
            1 => translation.GetString(keys[0]),
            2 => translation.GetMetadata(keys[0]).GetString(keys[1]),
            3 => translation.GetMetadata(keys[0]).GetMetadata(keys[1]).GetString(keys[2]),
            _ => "!!! Error: Translation key is not in the correct format"
        };
    }

    public static string GetLocalized(this StatiqRazorPage<IDocument> page, string key, string value) => string.Format(GetLocalized(page, key), value);
    public static string GetLocalized(this IDocument? document, string key) => document.GetString(key);
    public static string GetDateFormat(this IDocument document) => document.GetString("date_format");
    public static string GetLocalizedMonth(this StatiqRazorPage<IDocument> page, int month)
    {
        var language = page.Document.GetString("language");
        CultureInfo cultureInfo;
        try
        {
            cultureInfo = CultureInfo.GetCultureInfo(language);
        }
        catch (CultureNotFoundException)
        {
            cultureInfo = CultureInfo.CurrentCulture;
        }

        return cultureInfo.DateTimeFormat.GetMonthName(month);
    }
    
    private static IDocument GetTranslation(StatiqRazorPage<IDocument> page) => page.Outputs.FromPipeline("Data").FilterSources($"languages/{Constants.Language}.yml").First();
}