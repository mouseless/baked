# Unreleased

## Improvements

- Change `Response.StatusCode` property type to `HttpStatusCode`
- `VerifySent` now have `allowErrorResponse` parameter as optional
- `MockMe.ThClient<T>` can now set response with only `statusCode` parameter

## Bugfixes

- `VerifySent` was throwing key not found exception when header key was not
  present, fixed