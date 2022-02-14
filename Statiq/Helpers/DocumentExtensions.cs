using Blog.Statiq.Models;
using Statiq.Common;

namespace Blog.Statiq.Helpers;

public static class DocumentExtensions
{
    public static BlogPost AsBlogPost(this IDocument document)
        => new(document);
    public static Presentation AsPresentation(this IDocument document)
        => new(document);
    public static Page AsPage(this IDocument document)
        => new(document);
    public static Author GetAuthor(this IDocument document)
        => new(document);
    public static IEnumerable<BlogElementBase> AsBlogElements(this IDocument document)
        => document.GetChildren().Select(doc => doc.AsBlogPost());
    public static IEnumerable<BlogPost> AsBlogPosts(this DocumentList<IDocument> documents)
        => documents.Select(document => document.AsBlogPost());
    public static IEnumerable<Presentation> AsPresentations(this DocumentList<IDocument> documents)
        => documents.Select(document => document.AsPresentation());
}