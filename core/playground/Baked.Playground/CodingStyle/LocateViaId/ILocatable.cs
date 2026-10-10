namespace Baked.Playground.CodingStyle.LocateViaId;

public interface ILocatable
{
    Baked.Business.Id Id { get; }
    string Name { get; }
}