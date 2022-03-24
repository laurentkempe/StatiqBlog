using Microsoft.AspNetCore.Html;
using NetlifySharp;
using Statiq.Common;
using HtmlHelper = Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelper;

namespace Blog.Statiq.Models;

public class Author
{
    public Author(IMetadata metadata, Localization? localization)
    {
        Email = metadata.GetString("author:email");
        Picture = metadata.GetString("author:picture");
        Twitter = metadata.GetString("author:twitter");
        Location = metadata.GetString("author:location");
        Name = metadata.GetString("author2");
        
        Bio = new HtmlString(localization?.GetMarkdownRendered("author.bio") ?? "Cannot read bio");
        Job = new HtmlString(localization?.GetMarkdownRendered("author.job") ?? "Cannot read job");
    }
    
    public string Name { get; }
    public string Email { get; }
    public string Location { get; }
    public string Picture { get; }
    public string Twitter { get; }
    public HtmlString Bio { get; }
    public HtmlString Job { get; }
}