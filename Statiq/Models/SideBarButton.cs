using System.ComponentModel;
using System.Globalization;
using Statiq.Common;

namespace Blog.Statiq.Models;

[TypeConverter(typeof(SideBarButtonTypeConverter))]
public record SideBarButton(string Name, int Order, string Title, string Url, string Icon);

public class SideBarButtonTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType.IsAssignableTo(typeof(KeyValuePair<string, object>));
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not KeyValuePair<string, object> { Value: IMetadata metadata } menuButton) return default;

        //todo Check how to get the localized value
        // var localizedTitle = metadata.ToDocument().GetLocalized(metadata.GetString("title"));

        return new SideBarButton(menuButton.Key, metadata.GetInt("order"), metadata.GetString("title"), metadata.GetString("url"), metadata.GetString("icon"));
    }
}