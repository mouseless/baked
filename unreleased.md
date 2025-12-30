# Unreleased

## Bugfixes

- Nested types in generic classes were causing build error, fixed
- Adding configured plugins was causing error when resolver was not defined,
  fixed by defaulting resolver to `MetaUrl`