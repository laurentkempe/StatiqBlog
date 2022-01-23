using Blog.Statiq.Models;
using Blog.Statiq.ShortCodes;
using Microsoft.Extensions.Configuration;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;

void RegisterTypeConverters()
{
    TypeHelper.RegisterTypeConverter<IExecutionContext>(typeof(DeploymentTypeConverter));
}

await Bootstrapper
        .Factory
        .CreateWeb(args)
        .BuildConfiguration(builder =>
        {
            builder.AddYamlFile("_config.yml", optional: false, reloadOnChange: true);
            builder.AddYamlFile("theme\\input\\languages\\en.yml", optional: false, reloadOnChange: true);

            RegisterTypeConverters();
        })
        .AddShortcode(typeof(PlyrShortcode))
        .AddShortcode(typeof(ImageShortcode))
        .RunAsync();