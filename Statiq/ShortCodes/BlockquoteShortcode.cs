using System.Text;
using System.Text.RegularExpressions;
using Statiq.Common;
using Statiq.Markdown;

namespace Blog.Statiq.ShortCodes;

/// <summary>
/// Blockquote Shortcode
/// <example>
/// <br/>&lt;?# Blockquote [author[, source]] [link] [source_link_title] ?&gt;
/// <br/>Quote string
/// <br/>&lt;?#/ Blockquote ?&gt;
/// </example> 
/// </summary>
internal class BlockquoteShortcode : SyncShortcode
{
    private static readonly Regex RegexFullCiteWithTitle = new(@"(\S.*)\s+(https?:\/\/\S+)\s+(.+)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

    private static readonly Regex RegexFullCite = new(@"(\S.*)\s+(https?:\/\/\S+)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

    private static readonly Regex RegexAuthorTitle = new(@"([^,]+),\s*([^,]+)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

    public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document,
        IExecutionContext context)
    {
        var str = string.Join(' ', args.Select(a => a.Value));
        if (string.IsNullOrWhiteSpace(str)) return "";

        string author;
        var source = "";
        var title = "";
        var match = RegexFullCiteWithTitle.Match(str);
        if (match.Success)
        {
            author = match.Groups[1].Value;
            source = match.Groups[2].Value;
            title = match.Groups[3].Value;
        }
        else if ((match = RegexFullCite.Match(str)).Success)
        {
            author = match.Groups[1].Value;
            source = match.Groups[2].Value;
        }
        else if ((match = RegexAuthorTitle.Match(str)).Success)
        {
            author = match.Groups[1].Value;
            title = match.Groups[2].Value;
        }
        else
        {
            author = str;
        }

        var footer = new StringBuilder();
        if (author != "") footer.AppendFormat("<strong>{0}</strong>", author);

        if (source != "")
        {
            var link = source.Replace("https://", "").Replace("http://", "").Replace("/", "");
            footer.Append(
                $"<cite><a target=\"_blank\" rel=\"noopener\" href=\"{source}\">{(title != "" ? title : link)}</a></cite>");
        }
        else if (title != "")
        {
            footer.Append($"<cite>{title}</cite>");
        }

        using var writer = new StringWriter();
        MarkdownHelper.RenderMarkdown(context, document, content, writer);

        var result = new StringBuilder();
        result.Append($"<blockquote>{writer}");
        if (footer.Length > 0) result.Append($"<footer>{footer}</footer>");
        result.Append("</blockquote>");

        return result.ToString();
    }
}