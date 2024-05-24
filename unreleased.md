# Unreleased

## Improvements

- `ObjectAsJson` coding style feature now supports rendering `object` parameter
  as `FromBody` without generating a request class
- Only concrete classes are now included in `EntityExtensionViaComposition` and
 `EntitySubclassViaComposition` coding style features
- Transients with non api input parameters are now not rendered as api service