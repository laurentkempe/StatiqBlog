using Blog.Statiq.Helpers;
using Statiq.Common;

namespace Blog.Statiq.Models;

public class BlogElementBase
{
    protected readonly IDocument Document;

    protected BlogElementBase(IDocument document)
    {
        document.ThrowIfNull(nameof(document));

        Document = document;
    }

    public string Title => Document.GetString(Keys.Title) ?? Document.GetLocalized("post.no_title");
    public string? Permalink => Document.GetString("permalink");
    public string? Excerpt => Document.GetString(Keys.Excerpt);
    public string Date => Document.GetDateTime("date").ToString(Document.GetDateFormat());
    public DateTime DateTime => Document.GetDateTime("date");
    public string? ThumbnailImage => Document.GetString("thumbnailImage");
}