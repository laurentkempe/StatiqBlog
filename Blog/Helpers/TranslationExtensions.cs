using Statiq.Common;
using Statiq.Razor;

namespace Blog.Helpers;

/// <summary>
/// Helpers to get access to translation configuration
/// </summary>
public static class TranslationExtensions
{
    public static string Translate(this StatiqRazorPage<IDocument> page, string translationCategory, string key)
    {
        return GetTranslation(page).GetMetadata(translationCategory).GetString(key);
    }

    public static string TranslateFormatted(this StatiqRazorPage<IDocument> page, string translationCategory, string formattedKey, string value)
    {
        var format = GetTranslation(page).GetMetadata(translationCategory).GetString(formattedKey);
        
        return string.Format(format, value);
    }

    private static IDocument GetTranslation(StatiqRazorPage<IDocument> page)
    {
        return page.Outputs.FromPipeline("Data").FilterSources($"languages/{Constants.Language}.yml").First();
    }
}