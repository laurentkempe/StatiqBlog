using System.Globalization;
using System.Text;
using Blog.Statiq.Helpers;
using Statiq.Common;

namespace Blog.Statiq.Models;

public class Page
{
    private readonly IDocument _document;
    public Page(IDocument document)
    {
        document.ThrowIfNull(nameof(document));

        _document = document;
    }

    public string CoverCaption => _document.GetString("coverCaption") ?? "";
    public string CoverMeta => _document.GetString("coverMeta") ?? "in";
    public string CoverImage => _document.GetString("coverImage") ?? "";

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