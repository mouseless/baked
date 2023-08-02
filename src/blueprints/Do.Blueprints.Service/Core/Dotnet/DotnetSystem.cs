using Do.Architecture;

namespace Do.Blueprints.Service.Core.Dotnet;

public class DotnetSystem : ISystem
{
    public DateTime Now => DateTime.Now;
}