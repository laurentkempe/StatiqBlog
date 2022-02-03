using Statiq.Common;

namespace Blog;

public class Global
{
    public const int SidebarBehavior = 2; //TODO See how we can get that from configuration
    
    public static IMetadata ContextSetting(IExecutionContext executionContext)
    {
        return (IMetadata)executionContext.Settings["global"];
    }
}