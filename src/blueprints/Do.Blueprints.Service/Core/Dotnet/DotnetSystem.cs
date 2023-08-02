namespace Do.Blueprints.Service.Core.Dotnet;

public class DotnetSystem : ISystem
{
    public DateTime Now => DateTime.Now;
}

public interface ISystem
{
    DateTime Now { get; }
}