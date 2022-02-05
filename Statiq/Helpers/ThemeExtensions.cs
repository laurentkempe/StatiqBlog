using Blog.Statiq.Models;
using Statiq.Common;
using Statiq.Razor;

namespace Blog.Statiq.Helpers;

public static class ThemeExtensions
{
   public static Theme Theme(this StatiqRazorPage<IDocument> page) => new(page.Document);
}

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