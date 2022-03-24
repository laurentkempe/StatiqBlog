using System.Globalization;
using Statiq.Common;
using Statiq.Markdown;

namespace Blog.Statiq;

public class Localization
{
    private readonly Func<IPipelineOutputs> _getOutputs;
    private readonly Func<IDocument> _getDocument;

    public Localization(Func<IPipelineOutputs> getOutputs, Func<IDocument> getDocument)
    {
        _getOutputs = getOutputs;
        _getDocument = getDocument;
    }

    public string? Get(string key)
    {
        var keys = key.Split(".", StringSplitOptions.RemoveEmptyEntries);

        var metadata = GetLocalizationMetadata();
        
        return keys.Length switch
        {
            1 => metadata.GetString(keys[0]),
            2 => metadata.GetMetadata(keys[0]).GetString(keys[1]),
            3 => metadata.GetMetadata(keys[0]).GetMetadata(keys[1]).GetString(keys[2]),
            _ => "!!! Error: Translation key is not in the correct format"
        };
    }

    public string GetMarkdownRendered(string key)
    {
        using var writer = new StringWriter();
        MarkdownHelper.RenderMarkdown(null, _getDocument(), Get(key), writer);
            
        return writer.ToString();
    }

    public string GetLocalizedMonth(int month)
    {
        var language = _getDocument().GetString("language");
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

    public string GetFormatted(string key, string? value)
    {
        var localizedFormat = Get(key);

        if (localizedFormat != null) return string.Format(localizedFormat, value);

        localizedFormat.ThrowIfNull(nameof(localizedFormat));

        return "";
    }

    private IMetadata GetLocalizationMetadata()
    {
        return _getOutputs().FromPipeline("Data").FilterSources($"languages/{Constants.Language}.yml")[0];
    }
}