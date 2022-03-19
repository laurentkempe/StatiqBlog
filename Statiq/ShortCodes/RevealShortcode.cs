using Statiq.Common;

namespace Blog.Statiq.ShortCodes;

internal class RevealShortcode : SyncShortcode
{
    private const string Src = nameof(Src);
    private const string Width = nameof(Width);
    private const string Height = nameof(Height);
    
    public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document,
        IExecutionContext context)
    {
        var props = args.ToDictionary(Src, Width, Height);
        var src = props.GetString(Src);
        var width = props.Get<string?>(Width) ?? "800";
        var height = props.Get<string?>(Height) ?? "600";

        return @$"<div class=""slides"" style=""position: relative; padding-bottom: 56.25%; padding-top: 35px; height: 0; overflow: hidden;""><iframe src=""{src}"" width=""{width}"" height=""{height}"" frameborder=""0"" allowfullscreen="""" style=""position: absolute;top:0;left: 0;width: 100%;height: 100%;""></iframe></div>";
    }
}

/*

<div class="slides" style="position: relative; padding-bottom: 56.25%; padding-top: 35px; height: 0; overflow: hidden;">
  <iframe src="https://laurentkempe.com/presentations/dotNET%20build%20automation%20with%20NUKE/#/" width="800" height="600" frameborder="0" allowfullscreen="" style="
    position: absolute;
    top:0;
    left: 0;
    width: 100%;
    height: 100%;
  "></iframe>
</div>

*/