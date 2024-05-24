# Unreleased

## Improvements

- `RequireClaim` and `RequireNoClaim` renamed to .NET standards `RequireUser`
  and `AllowAnonymous` respectively
- `RequireUser` now accepts list of claims and can keep the base claim
- `RequireUser` and `AllowAnonymous` can now be added to class
- `Authorization` now expects authenticated user when no base claim is given
- `Authorization` now allows multiple base claims
