using Blog.Statiq.Helpers;
using Statiq.Common;

namespace Blog.Statiq.Models;

public class Presentation
{
    private readonly IDocument _document;
    public Presentation(IDocument document)
    {
        document.ThrowIfNull(nameof(document));

        _document = document;
    }

    public string Title => _document.GetString(Keys.Title) ?? _document.GetLocalized("post.no_title");
    public string? Permalink => _document.GetString("permalink");
    public string? Excerpt => _document.GetString(Keys.Excerpt);
    public string Date => _document.GetDateTime("date").ToString(_document.GetDateFormat());
    public DateTime DateTime => _document.GetDateTime("date");
    public string Video => _document.GetString("video");
    public string VideoStart => _document.GetString("start") ?? "0";
}