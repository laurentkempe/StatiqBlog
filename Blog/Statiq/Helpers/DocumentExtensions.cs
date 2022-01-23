using Blog.Statiq.Models;
using Statiq.Common;

namespace Blog.Statiq.Helpers;

public static class DocumentExtensions
{
    private static BlogPost AsBlogPost(this IDocument? document) => new(document);

    public static Sidebar AsSidebar(this IExecutionContext executionContext) => new(executionContext);
    public static Deployment AsDeployment(this IExecutionContext context) => TypeHelper.Convert<Deployment>(context);
    public static IEnumerable<BlogPost> AsBlogPosts(this DocumentList<IDocument> blogs)
        => blogs.Select(blog => blog.AsBlogPost());
}