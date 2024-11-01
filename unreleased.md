# Unreleased

## Improvements

- Fake reporting wasn't working without registering a `ReportOptions`, fixed
- Fake reporting now allows to provide a base path
- Data source spec was throwing missing `ISession` error, fixed
- Add query not found option to report context mocking
- Rich transient wasn't added to data source recipe, fixed
- Rich transient group names are now plural, making it similar to rich entity
- Domain assemblies feature now accepts one assembly when it accepts one base
  namespace, to avoid confusion
- File provider wasn't working without providing a base namespace in domain
  assemblies, fixed
