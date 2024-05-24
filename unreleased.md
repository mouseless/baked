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
- Transients having only one initializer with not api input paramteres are now 
  not marked as api service
- `Create` is now added as alias to add child methods