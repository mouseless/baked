using Baked.Database;
using Baked.Database.InMemory;

namespace Baked;

public static class InMemoryDatabaseExtensions
{
    public static InMemoryDatabaseFeature InMemory(this DatabaseConfigurator _) =>
        new();
}