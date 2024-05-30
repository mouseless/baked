# Unreleased

## Improvements

- `RequestLoggingFeature` now uses target domain method as category
- `4xx` is now logged in warning level leaving only `5xx` in error level
- Single line is now configurable from outside of `RequestLoggingFeature`
- Log now uses timestamp and trace id in log message
