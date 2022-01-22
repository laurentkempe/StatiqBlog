using Statiq.Common;
using Statiq.Razor;

namespace Blog.Helpers;

/// <summary>
/// Helpers to get access to translation configuration
/// </summary>
public static class TranslationExtensions
{
    public static string Translate(this StatiqRazorPage<IDocument> page, string key)
    {
        var keys = key.Split(".", StringSplitOptions.RemoveEmptyEntries);
        
        var translation = GetTranslation(page);

        return keys.Length switch
        {
            1 => translation.GetString(keys[0]),
            2 => translation.GetMetadata(keys[0]).GetString(keys[1]),
            _ => "!!! Error: Translation key is not in the correct format"
        };
    }

    public static string Translate(this StatiqRazorPage<IDocument> page, string key, string value)
    {
        return string.Format(Translate(page, key), value);
    }

    private static IDocument GetTranslation(StatiqRazorPage<IDocument> page)
    {
        return page.Outputs.FromPipeline("Data").FilterSources($"languages/{Constants.Language}.yml").First();
    }
}