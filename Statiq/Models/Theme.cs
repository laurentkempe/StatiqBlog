using Blog.Statiq.Helpers;
using Statiq.Common;

namespace Blog.Statiq.Models;

public class Theme
{
    private readonly IDocument Document;

    public Theme(IDocument document)
    {
        Document = document;
    }

    public int SidebarBehavior => Document.GetInt("sidebar_behavior");
    public bool ClearReading => Document.GetBool("clear_reading");

    public bool ThumbnailImage => Document.GetBool("thumbnail_image");

    public string ThumbnailImagePosition => Document.GetString("thumbnail_image_position");

    public string GravatarEmail => Document.GetString("gravatar_email");

    public Author Author => Document.GetAuthor();
}