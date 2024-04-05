using Do.Database;
using Do.Database.InMemory;

namespace Do;

public static class InMemoryDatabaseExtensions
{
    public static InMemoryDatabaseFeature InMemory(this DatabaseConfigurator _) => new();
}