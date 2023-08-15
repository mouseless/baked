# Unreleased

## Improvements

- `ITransaction` now accepts `Action` and `Action<TEntity>` where you can give
  an entity to be updated in a new transaction.
- When `ApplicationContext` doesn't have the given type, looks for any other
  type that implements or extends given type.
