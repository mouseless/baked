using Baked.Database.InMemory;

namespace Baked;

public static class InMemoryDatabaseExtensions
{
    extension(DatabaseFeature.Configurator _)
    {
        public InMemoryDatabaseFeature InMemory() =>
            new();
    }
}