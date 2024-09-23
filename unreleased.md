# Unreleased

## Bugfixes

- Generated `IManyToOneFecher`services was getting compiler error when a 
  non-public member was accessed, fixed

## Improvements

- Generated assembly names are now set from `Name` property of
  `GeneratedAssemblyDescriptor` with `Baked.g.` prefix
