using Blog.ShortCodes;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

await Bootstrapper
    .Factory
    .CreateWeb(args)
    .AddShortcode(typeof(PlyrShortcode))
    .AddShortcode(typeof(ImageShortcode))
    .RunAsync();