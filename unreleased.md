# Unreleased

## Improvements

- NFR tests now run faster. `Nfr` and `ServiceNfr` is redesigned to built only
  once for the complete test run.
  - `Nfr` environment is added to environment extensions for NFR specific
    configuration under `Program.cs`
  - `ServiceNfr` now runs only for `Nfr` environment
  - Forge configurations of `ServiceNfr` is removed completely, it uses
    automatically generated `Program` class
    - To enable test projects to see internal `Program` class, add
      `InternalsVisibleTo` to your application
  - Test projects don't need to have `GenerateProgramFile` set to `false` any
    more
- `WelcomePage` greeting feature is now removed

## Bugfixes

- Fixes a bug where casters fails because of service providers during nfr tests
