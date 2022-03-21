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
        .AddShortcode(typeof(RevealShortcode))
        .AddShortcode(typeof(AlertShortcode))
        .AddShortcode("githubCard", typeof(GitHubCardShortCode))
        .AddProcess(ProcessTiming.AfterExecution, _ => new ProcessLauncher("cmd", @"/C move feed.atom atom.xml")
        {
            WorkingDirectory = "output"
        })
        .RunAsync();