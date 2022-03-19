using Statiq.Common;

namespace Blog.Statiq.Models;

public class Presentation : BlogElementBase
{
    public Presentation(IDocument document) : base(document)
    {
    }

    public string Video => Document.GetString("video");
    public string VideoStart => Document.GetString("start") ?? "0";
    public override string? Permalink => $"/presentations/{base.Permalink}";
    public string? Slides => Document.GetString("slides");
}