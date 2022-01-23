using Blog.Statiq.Helpers;
using Statiq.Common;

namespace Blog.Statiq.Models;

public class Sidebar
{
    private readonly IDocument? _document;

    public Sidebar(IDocument? document)
    {
        _document = document;

        var d = _document.GetMetadata("sidebar");
        var raw = _document.GetRaw("sidebar:menu");
        SideBarButtons = _document.Get<IList<SideBarButton>>("sidebar:menu");
    }

    public Sidebar(IExecutionContext context)
    {
        SideBarButtons = context.Get<IList<SideBarButton>>("sidebar:menu");
    }

    public string? AuthorPicture => _document.GetString("author:picture");
    public string? ReadMoreAboutAuthor => _document?.GetLocalized("global:read_more_about_author");
    
    public IEnumerable<SideBarButton> SideBarButtons { get; }  
}

public class SideBarButton
{
    public SideBarButton(string title, string url, string icon)
    {
        Title = title;
        Url = url;
        Icon = icon;
    }

    public string Title { get; }
    public string Url { get; }
    public string Icon { get; }
}

/*
    var authorPicture = Metadata[""].ToString();
    var read_ore_about_author = ((IMetadata)Metadata["global"])["read_more_about_author"].ToString();
    var author_picture = ((IMetadata)Metadata["global"])["author_picture"].ToString();
    var author_bio = ((IMetadata)Metadata["author"])["bio"].ToString();
*/
