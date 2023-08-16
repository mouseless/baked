# Unreleased

## Improvements

- `ITransaction` now accepts `Action` and `Action<TEntity>` where you can give
  an entity to be updated in a new transaction.
- `MemberIsId` being overridden for auto mapping in data access, fixed.
