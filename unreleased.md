# Unreleased

## Improvements

- `ITransaction` now accepts `Action` and `Action<TEntity>` where you can give
  an entity to be updated in a new transaction.
- `ApplicationContext` `KeyNotFoundException` is now more informative.
- Same `Layer` and `Feature` can no longer be added multiple times.
- Mocked services which are singleton are now reset during unit test teardown
- `IConfiguration`mock is now added and can be configured with helpers
  provided from `ServiceSpec`
- `ISystem`can now be configured with helpers provided from `ServiceSpec`


