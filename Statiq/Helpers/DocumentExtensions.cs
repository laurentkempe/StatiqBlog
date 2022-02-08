using Blog.Statiq.Models;
using Statiq.Common;

namespace Blog.Statiq.Helpers;

public static class DocumentExtensions
{
    public static BlogPost AsBlogPost(this IDocument document)
        => new(document);
    public static Page AsPage(this IDocument document)
        => new(document);
    public static Author GetAuthor(this IDocument document)
        => new(document);
    public static IEnumerable<BlogPost> AsBlogPosts(this DocumentList<IDocument> documents)
        => documents.Select(blog => blog.AsBlogPost());
}