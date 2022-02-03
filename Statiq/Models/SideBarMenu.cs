using System.ComponentModel;
using System.Globalization;
using Statiq.Common;

namespace Blog.Statiq.Models;

[TypeConverter(typeof(SideBarMenuTypeConverter))]
public record SideBarMenu(string Name, IEnumerable<SideBarButton> SideBarButtons);

public class SideBarMenuTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) 
        => sourceType.IsAssignableTo(typeof(KeyValuePair<string, object>));

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    { 
        if (value is not KeyValuePair<string, object> menu) return default;

        return new SideBarMenu(menu.Key, TypeHelper.Convert<IList<SideBarButton>>(menu.Value));
    }
}
