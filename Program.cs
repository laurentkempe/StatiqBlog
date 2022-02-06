using Blog.Statiq.ShortCodes;
using Microsoft.Extensions.Configuration;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

await Bootstrapper
        .Factory
        .CreateWeb(args)
        .BuildConfiguration(builder =>
        {
            builder.AddYamlFile("_config.yml", optional: false, reloadOnChange: true);
            builder.AddYamlFile("theme\\sidebar.yml", optional: false, reloadOnChange: true);
            builder.AddYamlFile("theme\\input\\languages\\en.yml", optional: false, reloadOnChange: true);
        })
        .AddShortcode(typeof(PlyrShortcode))
        .AddShortcode(typeof(ImageShortcode))
        .AddShortcode("githubCard", typeof(GitHubCardShortCode))
        .RunAsync();