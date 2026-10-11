namespace Baked.Theme;

public interface IComponentContextBasedGenerator<T>
{
    T Generate(ComponentContext context);
}