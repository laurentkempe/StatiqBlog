using Statiq.Common;

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

        Bio = localization?.Get("author.bio") ?? "Cannot read bio";
        Job = localization?.Get("author.job") ?? "Cannot read job";
    }
    
    public string Name { get; }
    public string Email { get; }
    public string Location { get; }
    public string Picture { get; }
    public string Twitter { get; }
    public string? Bio { get; }
    public string? Job { get; }
}