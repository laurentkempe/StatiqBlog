using Blog.Statiq.Helpers;
using Statiq.Common;

namespace Blog.Statiq.Models;

public class Theme
{
    private readonly IDocument _document;

    public Theme(IDocument document)
    {
        _document = document;
    }

    public int SidebarBehavior => _document.GetInt("sidebar_behavior");
    public bool ClearReading => _document.GetBool("clear_reading");

    public bool ThumbnailImage => _document.GetBool("thumbnail_image");

    public string ThumbnailImagePosition => _document.GetString("thumbnail_image_position");

    public string GravatarEmail => _document.GetString("gravatar_email");

    public Author Author => _document.GetAuthor();
}