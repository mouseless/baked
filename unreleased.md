# Unreleased

## Improvements

- Changed Id automaping logic. Now `Guid` properties named as `Id` are being
 mapped as Id.
- Entity properties of type `object` were not being mapped as `MEDIUMTEXT`,
 fixed.
