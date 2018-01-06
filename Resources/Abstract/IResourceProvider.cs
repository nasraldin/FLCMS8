namespace Resources.Abstract
{
    public interface IResourceProvider
    {
        object GetResource(string name, string culture);
    }
}