using Statiq.Common;
using Statiq.Razor;

namespace Blog.Helpers;

/// <summary>
/// Helpers to get access to translation configuration
/// </summary>
public static class TranslationExtensions
{
    public static string Translate(this StatiqRazorPage<IDocument> page, string category, string key)
    {
        var translation = page.Outputs.FromPipeline("Data").FilterSources($"languages/{Constants.Language}.yml").First();

        return translation.GetMetadata(category).GetString(key);
    }
}