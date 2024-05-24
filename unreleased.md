# Unreleased

## Improvements

- `ObjectAsJson` coding style feature now supports rendering `object` parameter
  as `FromBody` without generating a request class
- Only concrete classes are now included in `EntityExtensionViaComposition` and
 `EntitySubclassViaComposition` coding style features
- Transients having only one initializer with not api input paramteres are now 
  not marked as api service