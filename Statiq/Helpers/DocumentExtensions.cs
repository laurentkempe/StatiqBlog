using Blog.Statiq.Models;
using Statiq.Common;

namespace Blog.Statiq.Helpers;

public static class DocumentExtensions
{
    private static BlogPost AsBlogPost(this IDocument? document) => new(document);

    public static SideBar GetSidebar(this IExecutionContext executionContext)
        => TypeHelper.Convert<SideBar>(executionContext.GetMetadata(SideBar.Key));

    public static Author GetAuthor(this IExecutionContext executionContext)
        => new Author(executionContext);
    
    public static Deployment GetDeployment(this IExecutionContext executionContext)
        => TypeHelper.Convert<Deployment>(executionContext);
    
    public static IEnumerable<BlogPost> AsBlogPosts(this DocumentList<IDocument> blogs)
        => blogs.Select(blog => blog.AsBlogPost());
}