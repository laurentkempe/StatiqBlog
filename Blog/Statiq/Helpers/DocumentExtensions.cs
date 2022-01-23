using Blog.Statiq.Models;
using Statiq.Common;

namespace Blog.Statiq.Helpers;

public static class DocumentExtensions
{
    private static BlogPost AsBlogPost(this IDocument? document) => new(document);

    public static Sidebar AsSidebar(this IDocument? document) => new(document);

    public static IEnumerable<BlogPost> AsBlogPosts(this DocumentList<IDocument> blogs)
        => blogs.Select(blog => blog.AsBlogPost());
}