using System.ComponentModel;
using System.Globalization;
using Statiq.Common;

namespace Blog.Statiq.Models;

[TypeConverter(typeof(SideBarMenuTypeConverter))]
public record SideBarMenu(string Name, int Order, IEnumerable<SideBarButton> SideBarButtons);

public class SideBarMenuTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) 
        => sourceType.IsAssignableTo(typeof(KeyValuePair<string, object>));

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    { 
        if (value is not KeyValuePair<string, object> { Value: IMetadata metadata } menu) return default;

        var barButtons = TypeHelper.Convert<IList<SideBarButton>>(menu.Value);
        //todo why one barButton in barButtons is null?!? because converter cannot convert order:1 and home: 
        var sideBarButtons = barButtons.Where(button => button is not null).OrderBy(sbb => sbb.Order);
        
        return new SideBarMenu(menu.Key, metadata.GetInt("order"), sideBarButtons);
    }
}
