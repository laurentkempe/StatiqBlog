using Blog.Statiq.Helpers;
using Statiq.Common;

namespace Blog.Statiq.Models;

public class Sidebar
{
    private readonly IDocument? _document;

    public Sidebar(IDocument? document)
    {
        _document = document;
    }

    public string? AuthorPicture => _document.GetString("author:picture");
    public string? ReadMoreAboutAuthor => _document?.GetLocalized("global:read_more_about_author");
}

/*
    var authorPicture = Metadata[""].ToString();
    var read_ore_about_author = ((IMetadata)Metadata["global"])["read_more_about_author"].ToString();
    var author_picture = ((IMetadata)Metadata["global"])["author_picture"].ToString();
    var author_bio = ((IMetadata)Metadata["author"])["bio"].ToString();
*/
