using System.Globalization;
using Statiq.Common;

namespace Blog.Statiq.Models;

public class BlogPost : BlogElementBase
{
    public BlogPost(IDocument document) : base(document)
    {
    }

    public bool IsPost =>  Document.Source.FullPath.Contains("input/posts");

    public IEnumerable<string> Categories => Document.GetList<string>("categories") ?? Enumerable.Empty<string>();
    public IEnumerable<string> Tags => Document.GetList<string>("tags") ?? Enumerable.Empty<string>();
    public string FullDateTime => new DateTimeOffset(Document.GetDateTime("date").ToUniversalTime()).ToString("O", CultureInfo.InvariantCulture);
    public string? CoverCaption => Document.GetString("coverCaption");
    public string CoverMeta => Document.GetString("coverMeta") ?? "in";
    public string? CoverImage => Document.GetString("coverImage");
    public string CoverSize => Document.GetString("coverSize") ?? "";
    public string CoverSizeClass => !string.IsNullOrEmpty(CoverSize) ? $"post-header-cover--{CoverSize}" : "";
    public bool Meta => Document.GetBool("meta");
    public string? MetaAlignment => Document.GetString("metaAlignment");
    public string MetaAlignmentClass => !string.IsNullOrEmpty(MetaAlignment) ? $"text-{MetaAlignment}" : "text-left";
}