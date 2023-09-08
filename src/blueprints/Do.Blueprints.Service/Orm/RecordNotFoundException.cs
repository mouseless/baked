﻿namespace Do.Orm;

public class RecordNotFoundException : Exception
{
    public static RecordNotFoundException For<T>(Guid id) => new(typeof(T), id);
    public static RecordNotFoundException For<T>(string field, object value) => new(typeof(T), field, value);

    public RecordNotFoundException(Type entityType, Guid id)
        : this(entityType, "Id", $"{id}") { }

    public RecordNotFoundException(Type entityType, string field, object value)
        : base($"{entityType.Name} with {field}: '{value}' does not exist") { }
}