# Unreleased

## Improvements

- Change `Response.StatusCode` property type to `HttpStatusCode`
- `VerifySent` now have `allowErrorResponse` parameter as optional
- Change `MockMe.TheClient` parameter `noResponse` name to `emptyResponse`

## Bug Fixes

- `VerifySent` was throwing key not found exception when header key was not
  present, fixed