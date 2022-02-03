using Blog.Statiq.Helpers;
using Statiq.Common;

namespace Blog.Statiq.Models;

public class BlogPost
{
    private readonly IDocument? _document;
    public BlogPost(IDocument? document)
    {
        _document = document;
    }

    public string? Title => _document.GetString(Keys.Title);
    public string? Permalink => _document.GetString("permalink");
    public string? Excerpt => _document.GetString(Keys.Excerpt);
    public string? Date => _document.GetDateTime("date").ToString(_document?.GetDateFormat());
    public string? ThumbnailImage => _document.GetString("thumbnailImage");
}