# Unreleased

## Improvements

- `RequireClaim` now accepts list of claims and can keep the base claim
- `RequireClaim` and `RequireNoClaim` can now be added to class
- `RequireClaim` and `RequireNoClaim` renamed to .NET standards `Authorize` and
  `AllowAnonymous` respectively
- `Authorization` now expects authenticated user when no base claim is given
- `Authorization` now allows multiple base claims
