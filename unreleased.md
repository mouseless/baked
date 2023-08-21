# Unreleased

## Improvements

- `ITransaction` now accepts `Action` and `Action<TEntity>` where you can give
  an entity to be updated in a new transaction.
- `ApplicationContext` `KeyNotFoundException` is now more informative.
- Multiple `Layers` and `Features` can no longer be added to application.
