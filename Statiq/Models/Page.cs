using Statiq.Common;

namespace Blog.Statiq.Models;

public class Page
{
    private readonly IDocument Document;
    public Page(IDocument document)
    {
        document.ThrowIfNull(nameof(document));

        Document = document;
    }

    public string CoverCaption => Document.GetString("coverCaption") ?? "";
    public string CoverMeta => Document.GetString("coverMeta") ?? "in";
    public string CoverImage => Document.GetString("coverImage") ?? "";

    public string MainClass
    {
        get
        {
            var classes = new List<string>();

            if (!CoverImage.IsNullOrEmpty())
            {
                classes.Add("hasCover");
            }

            classes.Add(CoverMeta.Equals("out", StringComparison.OrdinalIgnoreCase) ? "hasCoverMetaOut" : "hasCoverMetaIn");

            if (!CoverCaption.IsNullOrEmpty())
            {
                classes.Add("hasCoverCaption");
            }

            return string.Join(' ', classes);
        }        
    }
}