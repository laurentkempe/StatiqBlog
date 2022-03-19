using Blog.Statiq.Models;
using Statiq.Common;
using Statiq.Razor;

namespace Blog.Statiq.Helpers;

public static class ThemeExtensions
{
   public static Theme Theme(this StatiqRazorPage<IDocument> page) => new(page);
}