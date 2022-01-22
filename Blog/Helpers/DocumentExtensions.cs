using Statiq.Common;

namespace Blog.Helpers;

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

public static class DocumentExtensions
{
    private static BlogPost AsBlogPost(this IDocument? document)
    {
        return new BlogPost(document);
    }

    public static IEnumerable<BlogPost> AsBlogPost(this DocumentList<IDocument> blogs)
    {
        return blogs.Select(blog => blog.AsBlogPost());
    }
}