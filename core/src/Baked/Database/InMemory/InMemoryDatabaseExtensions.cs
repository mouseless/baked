using Baked.Database;
using Baked.Database.InMemory;

namespace Baked;

public static class InMemoryDatabaseExtensions
{
    extension(DatabaseConfigurator _)
    {
        public InMemoryDatabaseFeature InMemory() =>
            new();
    }
}