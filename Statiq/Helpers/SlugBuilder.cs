using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Unicode;
using Meziantou.Framework;

namespace Blog.Statiq.Helpers;

public static class SlugBuilder
{
    private static readonly SlugOptions SlugOptions = CreateOptions();

    private static SlugOptions CreateOptions()
    {
        var options = new SlugOptions
        {
            CasingTransformation = CasingTransformation.ToLowerCase,
            Culture = CultureInfo.InvariantCulture,
            MaximumLength = 80,
            Separator = "-",
            CanEndWithSeparator = false,
            AllowedRanges =
            {
                UnicodeRange.Create('0', '9'),
                UnicodeRange.Create('a', 'z'),
                UnicodeRange.Create('A', 'Z'),
            },
        };

        return options;
    }

    [return: NotNullIfNotNull("text")]
    public static string? Create(string type, string? text)
    {
        return Create(type, text, SlugOptions);
    }

    [return: NotNullIfNotNull("text")]
    public static string? Create(string type, string? text, int maxLength)
    {
        var options = CreateOptions();
        options.MaximumLength = maxLength;
        return Create(type, text, options);
    }

    [return: NotNullIfNotNull("text")]
    private static string? Create(string type, string? text, SlugOptions slugOptions)
    {
        if (text == null)
            return null;

        var newText = text
            .Replace("ASP.NET", $"asp{slugOptions.Separator}net", StringComparison.OrdinalIgnoreCase)
            .Replace(".net", "dotnet", StringComparison.OrdinalIgnoreCase)
            .Replace("C#", "csharp", StringComparison.OrdinalIgnoreCase)
            .Replace("F#", "fsharp", StringComparison.OrdinalIgnoreCase);

        text = newText;

        var slug = Slug.Create(text, slugOptions);

        // Would collision with the name of a directory
        if (slug.All(char.IsDigit))
        {
            return type + "-" + slug;
        }

        // Would collision with index.html
        if (string.Equals(slug, "index", StringComparison.OrdinalIgnoreCase))
        {
            return type + "-" + slug;
        }

        return slug;
    }
}