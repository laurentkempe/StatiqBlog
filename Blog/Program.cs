using Blog.ShortCodes;
using Microsoft.Extensions.Configuration;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

    await Bootstrapper
        .Factory
        .CreateWeb(args)
        .BuildConfiguration(builder => builder.AddYamlFile("_config.yml", optional: false, reloadOnChange: true))
        .BuildConfiguration(builder => builder.AddYamlFile("theme\\languages\\en.yml", optional: false, reloadOnChange: true))
        .AddShortcode(typeof(PlyrShortcode))
        .AddShortcode(typeof(ImageShortcode))
        .RunAsync();