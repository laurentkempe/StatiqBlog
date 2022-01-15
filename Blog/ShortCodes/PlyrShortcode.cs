using Statiq.Common;

namespace Blog.ShortCodes;

internal class PlyrShortcode : SyncShortcode
{
    public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document,
        IExecutionContext context)
    {
        var video = "XtASb2tmo5c";
        var start = 0;

        if (args.Length >= 1 && args[0].Key != null && args[0].Key.ToLower() == "video") video = args[0].Value;

        if (args.Length == 2 && args[1].Key != null && args[1].Key.ToLower() == "start")
            start = int.Parse(args[1].Value);

        //     var markup = @$"
// <div class=""lk-player"">
//     <iframe width=""200"" height=""113""
//         src=""https://www.youtube.com/embed/{video}?origin=https://plyr.io&amp;iv_load_policy=3&amp;modestbranding=2&amp;playsinline=1&amp;showinfo=0&amp;rel=0&amp;enablejsapi=1""
//         allowfullscreen
//         allowtransparency
//         allow=""autoplay"">
//     </iframe>
// </div>";    

        var markup =
            @$"<div class=""container""><div class=""lk-player plyr__poster"" width=""200"" height=""113"" data-plyr-provider=""youtube"" data-plyr-config='{{ ""youtube"": {{ ""start"": {start}}} }}' data-plyr-embed-id=""{video}""></div></div>";

        return markup;
    }
}