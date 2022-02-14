using Statiq.Razor;

namespace Blog.Statiq;

public abstract class MyStatiqRazorPage<TModel> : StatiqRazorPage<TModel>
{
    protected MyStatiqRazorPage()
    {
        Localization = new Localization(() => Outputs, () => Document);
    }

    protected Localization Localization { get; }
}