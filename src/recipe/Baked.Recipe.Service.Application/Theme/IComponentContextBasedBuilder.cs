namespace Baked.Theme;

public interface IComponentContextBasedBuilder<T>
{
    T Build(ComponentContext context);
}