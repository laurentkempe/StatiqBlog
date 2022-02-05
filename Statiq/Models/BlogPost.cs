﻿using Blog.Statiq.Helpers;
using Statiq.Common;

namespace Blog.Statiq.Models;

public class BlogPost
{
    private readonly IDocument _document;
    public BlogPost(IDocument document)
    {
        document.ThrowIfNull(nameof(document));

        _document = document;
    }

    public bool IsPost => Permalink != null;
    public string Title => _document.GetString(Keys.Title) ?? _document.GetLocalized("post.no_title");
    public string? Permalink => _document.GetString("permalink");
    public string? Excerpt => _document.GetString(Keys.Excerpt);
    public string Date => _document.GetDateTime("date").ToString(_document?.GetDateFormat());
    public string? CoverCaption => _document.GetString("coverCaption");
    public string CoverMeta => _document.GetString("coverMeta") ?? "in";
    public string? CoverImage => _document.GetString("coverImage");
    public string CoverSize => _document.GetString("coverSize") ?? "";
    public string CoverSizeClass => !string.IsNullOrEmpty(CoverSize) ? $"post-header-cover--{CoverSize}" : "";
    public string? ThumbnailImage => _document.GetString("thumbnailImage");
    public string? MetaAlignment => _document.GetString("metaAlignment");
    public string MetaAlignmentClass => !string.IsNullOrEmpty(MetaAlignment) ? $"text-{MetaAlignment}" : "text-left";
}