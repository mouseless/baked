# Unreleased

## Improvements

- `SecurityRequirementOperationFilter` was resetting the whole security
  definition list instead of adding, fixed
- `UseAttribute` didn't allow multiple usage, fixed
- `AddHeaderOperationFilter` was specific to adding headers, generalized and
  renamed as `AddParameterOperationFilter`
- `HasMetadata` helper added to make it easy to check if an `HttpContext` has
  given attribute in requests action
