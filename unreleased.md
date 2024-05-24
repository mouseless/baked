# Unreleased

## Improvements

- `ObjectAsJson` coding style feature now supports rendering `object` parameter
  as `FromBody` without generating a request class
- Only concrete classes are now included in `EntityExtensionViaComposition` and
 `EntitySubclassViaComposition` coding style features
- `RequireClaim` and `RequireNoClaim` renamed to `RequireUser` and
  `AllowAnonymous` respectively
- `RequireUser` now accepts list of claims and can keep the base claim
- `RequireUser` and `AllowAnonymous` can now be added to class
- `Authorization` now expects authenticated user when no base claim is given
- `Authorization` now allows multiple base claims
