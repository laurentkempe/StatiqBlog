using Statiq.Common;

namespace Blog.Statiq.Models;

public class Author
{
    public Author(IExecutionContext executionContext)
    {
        Email = executionContext.GetString("author:email");
        Picture = executionContext.GetString("author:picture");
        Twitter = executionContext.GetString("author:twitter");
        Location = executionContext.GetString("author:location");
    }
    
    public Author(IDocument document)
    {
        Email = document.GetString("author:email");
        Picture = document.GetString("author:picture");
        Twitter = document.GetString("author:twitter");
        Location = document.GetString("author:location");
    }
    
    public string Email { get; }
    public string Location { get; }
    public string Picture { get; }
    public string Twitter { get; }
}