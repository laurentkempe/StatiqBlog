using System.Text;
using Statiq.Common;

namespace Blog.Statiq.ShortCodes;

public class GitHubCardShortCode : SyncShortcode
{
    public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context)
    {
        var dict = args.ToDictionary(pair => pair.Key, pair => pair.Value);
        var user = dict["user"];
        var repo = dict["repo"];
        var align = dict["align"];

        var stringBuilder = new StringBuilder();

        stringBuilder.Append($@"<div style=""text-align: {align}"">");
        stringBuilder.Append($@"<iframe id=""ghcard-{user}-1"" frameborder=""0"" scrolling=""0"" allowtransparency=""true"" src=""//lab.lepture.com/github-cards/cards/default.html?user={user}&amp;identity=ghcard-{user}-1&amp;repo={repo}"" width=""400"" height=""273""></iframe>");
        stringBuilder.Append("</div>");
        
        return stringBuilder.ToString();
    }
}