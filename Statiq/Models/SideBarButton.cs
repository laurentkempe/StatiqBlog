using System.ComponentModel;
using System.Globalization;
using Blog.Statiq.Helpers;
using Statiq.Common;

namespace Blog.Statiq.Models;

[TypeConverter(typeof(SideBarButtonTypeConverter))]
public record SideBarButton(string Name, string Title, string Url, string Icon);

public class SideBarButtonTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) 
        => sourceType.IsAssignableTo(typeof(KeyValuePair<string, object>));

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    { 
        if (value is not KeyValuePair<string, object> menuButton) return default;

        if (menuButton.Value is not IMetadata metadata) return default;

        // var localizedTitle = metadata.ToDocument().GetLocalized(metadata.GetString("title"));

        return new SideBarButton(menuButton.Key, metadata.GetString("title"), metadata.GetString("url"), metadata.GetString("icon"));
    }
}