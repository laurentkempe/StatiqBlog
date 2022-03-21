using Statiq.Common;
using Statiq.Markdown;

namespace Blog.Statiq.ShortCodes;

internal class AlertShortcode : SyncShortcode
{
    private const string AlertType = nameof(AlertType);
    
    public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document,
        IExecutionContext context)
    {
        var props = args.ToDictionary(AlertType);
        var alertType = props.Get<string?>(AlertType) ?? "info";

        using var writer = new StringWriter();
        MarkdownHelper.RenderMarkdown(context, document, content, writer);
            
        return @$"<div class=""alert {alertType}"">{writer}</div>";
    }
}