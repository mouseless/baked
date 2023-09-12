# Unreleased

## Improvements

- Reset mocked singleton services were not setup after reset, fixed
- Added `HandledException` and `HandledExceptionHandler` for providing a 
  better distinction in results for managed and unmanaged exceptions
- `AddHandler<T>()` extension for exception handling feature is now removed, 
  all `IExceptionHandler` implementations should be registered as singleton 
  services from features
- `RecordNotFoundException` now returns 404 status code