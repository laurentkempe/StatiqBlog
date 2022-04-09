using Statiq.Common;

namespace Blog.Statiq.ShortCodes;

public class ImageShortcode : SyncShortcode
{
    /**
     * Image tag
     * 
     * Syntax:
     * {% image [classes] group:group1 /path/to/image [/path/to/thumbnail]
     * [width of thumbnail] [height of thumbnail] [title text] %}
     * E.g:
     * {% image fig-50 right fancybox group:travel image2.png http://example.com/image125.png
     * 100% 160px "A beautiful sunrise" %}
     */
    public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document,
        IExecutionContext context)
    {
        // <?# image center clear group=travel https://farm2.staticflickr.com/1971/31306281378_02b055ccfe_z.jpg /?>
        
        var classes = new List<string> { "figure" };
        var group = "";
        var pathToImage = "";
        var alt = "";
        
        var state = 0;
       
        foreach (var (key, value) in args)
        {
            if (state == 1)
            {
                pathToImage = value;
                state = 2;
            }

            if (state == 0 && key.IsNullOrEmpty())
            {
                classes.Add(value);
            }

            if (state == 0 && !key.IsNullOrEmpty() && key.Equals("group", StringComparison.OrdinalIgnoreCase))
            {
                state = 1;
                group = value;
            }

            if (state == 2 && !key.IsNullOrEmpty() && key.Equals("alt", StringComparison.OrdinalIgnoreCase))
            {
                alt = value;
            }
        }
        
        /*
            <div class="figure center" style="width:;"><img class="fig-img" src="https://farm2.staticflickr.com/1971/31306281378_02b055ccfe_z.jpg" alt=""></div>
         */

        // Build HTML structure
        var html = @$"<div class='{string.Join(' ', classes)}'>";
        html +=  @$"<img class=""fig-img"" src=""{pathToImage}"" alt=""{alt}"" />";

        // Build HTML structure
        // var html = @$"<div class='{figureClass} {string.Join(' ', classes)}'"' +
        //         (reIndexOf(classes, rFigClass) === -1 ? ' style="width:' + thumbnailWidth + ';"' : '') + '>';
        // html += fancybox || image;
        //
        // // Add caption
        // // if (!title.IsNullOrEmpty() && classes.IndexOf(noCaptionClass) == -1)
        // // {
        // //     html += @$"<span class='{captionClass}'>";
        // //     html += title;
        // //     html += "</span>";
        // // }
        //
        html += "</div>";
        // add `clear` div
        // html += clear;
               
        return html;
    }

    // private string GetImage()
    // {
    //     var image = @$"<img class=""fig-img"" src=""' + (thumbnail || original) + '"" ";

        /*
    // Get title of image
    var title = args.join(' ');
    // Build the image HTML structure
    var image = '<img class="fig-img" src="' + (thumbnail || original) + '" ';
    // add image size
    if (thumbnailWidth || thumbnailHeight) {
      image += 'style="';
      // add width
      if (thumbnailWidth) {
        image += 'width:' + thumbnailWidth + ';';
      }
      // add height
      if (thumbnailHeight) {
        image += 'height:' + thumbnailHeight + ';';
      }
      image += '"';
    }
    image += 'alt="' + title + '">';         */
    // }

    // public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context)
    // {
    //     var group = "default";
    //     var original = "";
    //     var thumbnail = "";
    //     var thumbnailWidth = "";
    //     var thumbnailHeight = "";
    //     var title = "";
    //     var clearClass = "clear";
    //     var clear = "";
    //     var fancyboxClass = "fancybox";
    //     var fancybox = "";
    //     var figureClass = "figure";
    //     var noCaptionClass = "nocaption";
    //     var captionClass = "caption";
    //     
    //     var classes = new List<string>();
    //
    //     var i = 0;
    //     while (args.Any() && args[i].Key.IsNullOrEmpty())
    //     {
    //         classes.Add(args[i++].Value);
    //     }
    //
    //     if (i < args.Length && args[i].Key.Equals("group", StringComparison.OrdinalIgnoreCase))
    //     {
    //         group = args[i++].Value;
    //     }
    //
    //     if (i < args.Length)
    //     {
    //         original = args[i++].Value;
    //     }
    //
    //     if (i < args.Length && args[i].Key.IsNullOrEmpty())
    //     {
    //         thumbnail = args[i++].Value;    
    //     }
    //     
    //     if (i < args.Length && args[i].Key.IsNullOrEmpty())
    //     {
    //         thumbnailWidth = args[i++].Value;    
    //     }
    //
    //     if (i < args.Length && args[i].Key.IsNullOrEmpty())
    //     {
    //         thumbnailHeight = args[i++].Value;    
    //     }
    //
    //     if (i < args.Length && args[i].Key.IsNullOrEmpty())
    //     {
    //         title = args[i++].Value;    
    //     }
    //     
    //     var image = @$"<img class=""fig-im"" src=""{(thumbnail.IsNullOrEmpty() ? thumbnail : original)}"" ";
    //
    //     if (!(thumbnailWidth.IsNullOrEmpty() || thumbnailHeight.IsNullOrEmpty()))
    //     {
    //         image += @"style=""";
    //         if (!thumbnailWidth.IsNullOrEmpty())
    //         {
    //             image += $"width:{thumbnailWidth};";
    //         }
    //         if (!thumbnailHeight.IsNullOrEmpty()) {
    //             image += $"height:{thumbnailHeight};";
    //         }
    //         image += '"';
    //     }
    //     image += $@"alt=""{title}"">";
    //
    //     // Build div to retrieve normal flow of document
    //     if (classes.IndexOf(clearClass) >= 0)
    //     {
    //         clear = @"<div style=""clear:both;""></div>";
    //         // remove `clear` class of `classes` to not be attached on the main div
    //         classes.Remove(clearClass);
    //     }
    //     
    //     // Add Fancybox structure around image
    //     if (classes.IndexOf(fancyboxClass) >= 0) {
    //         fancybox +=
    //             $@"<a class='{fancyboxClass}' href='{original}' title='{title}' data-caption='{title}' data-fancybox='{group}'>";
    //         fancybox += image;
    //         fancybox += "</a>";
    //         // remove `fancyfox` class of `classes` to not be attached on the main div
    //         classes.Remove(fancyboxClass);
    //     }
    //     
    //     // Build HTML structure
    //     var html = @$"<div class='{figureClass} {string.Join(' ', classes)}'"' +
    //             (reIndexOf(classes, rFigClass) === -1 ? ' style="width:' + thumbnailWidth + ';"' : '') + '>';
    //     html += fancybox || image;
    //     
    //     // Add caption
    //     if (!title.IsNullOrEmpty() && classes.IndexOf(noCaptionClass) == -1)
    //     {
    //         html += @$"<span class='{captionClass}'>";
    //         html += title;
    //         html += "</span>";
    //     }
    //     
    //     html += "</div>";
    //     // add `clear` div
    //     html += clear;
    //            
    //     return html;
    // }
}