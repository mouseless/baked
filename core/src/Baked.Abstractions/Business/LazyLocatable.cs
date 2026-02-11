namespace Baked.Business;

public record LazyLocatable<T>(T Value, Func<Task> Initialize);