using System.ComponentModel;
using System.Globalization;
using Statiq.Common;

namespace Blog.Statiq.Models;

[TypeConverter(typeof(SideBarTypeConverter))]
public record SideBar(IList<SideBarMenu> SideBarMenus)
{
    public static string Key => "sidebar";
}

public class SideBarTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) 
        => sourceType.IsAssignableTo(typeof(IMetadata));

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    { 
        if (value is not IMetadata menus) return default;

        return new SideBar(TypeHelper.Convert<IList<SideBarMenu>>(menus));        
    }
}
