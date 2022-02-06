using Blog.Statiq.Models;
using Statiq.Common;

namespace Blog.Statiq.Helpers;

public static class ExecutionContextExtensions
{
    public static SideBar GetSidebar(this IExecutionContext executionContext)
        => TypeHelper.Convert<SideBar>(executionContext.GetMetadata(SideBar.Key));
    public static Author GetAuthor(this IExecutionContext executionContext)
        => new(executionContext);
    public static Deployment GetDeployment(this IExecutionContext executionContext)
        => TypeHelper.Convert<Deployment>(executionContext);
}