namespace Baked.Theme;

public interface IComponentContextFilter
{
    bool AppliesTo(ComponentContext context);
}