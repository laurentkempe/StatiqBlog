using System.ComponentModel;
using System.Globalization;
using Statiq.Common;

namespace Blog.Statiq.Models;

[TypeConverter(typeof(DeploymentTypeConverter))]
public record Deployment(string Type, string Repository, string Branch);

public class DeploymentTypeConverter : TypeConverter
{
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) => destinationType == typeof(Deployment);

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType != typeof(Deployment)) return default;

        if (value is not IExecutionContext executionContext) return default;
        
        var deployMetadata = executionContext.GetMetadata("deploy");

        if (deployMetadata is null || !deployMetadata.ContainsKey("type") || !deployMetadata.ContainsKey("repo") || !deployMetadata.ContainsKey("branch")) return default;
        
        return new Deployment(deployMetadata.GetString("type"), deployMetadata.GetString("repo"), deployMetadata.GetString("branch"));
    }
}