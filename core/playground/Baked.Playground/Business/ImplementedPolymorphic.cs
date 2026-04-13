namespace Baked.Playground.Business;

public class ImplementedPolymorphic(string name)
    : IPolymorphic
{
    public string Name { get; } = name;
}