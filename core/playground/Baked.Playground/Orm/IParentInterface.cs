namespace Baked.Playground.Orm;

public interface IParentInterface
{
    Entity? CalculatedReferenceOverInterface { get; }

    bool IsContextNull();
}