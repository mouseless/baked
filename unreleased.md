# Unreleased

## Improvements

- Reset mocked singleton services were not setup after reset, fixed
- Added `HandledException` and `HandledExceptionHandler` for providing a 
  better distincition in results for managed and unmanaged exceptions
- `DefaultExceptionHandler` is renamed to `UnhandledExceptionHandler`
- `AddHandler<T>()` extension for exception handling feature is now removed, 
  all `IExceptionHandler` implementations should be registered as singleton 
  services from features