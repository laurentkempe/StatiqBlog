using Statiq.Common;

namespace Blog;

public class Global
{
    public static IMetadata ContextSetting(IExecutionContext executionContext)
    {
        return (IMetadata)executionContext.Settings["global"];
    }
}