# Unreleased

## Improvements

- Folder reorganization
- `Baked.Architecture` and `Baked.Recipe.Service` merged into `Baked`
- `Baked.Core` and `Baked.Service` merged into `Baked.Abstractions`
- Renamed `Service` recipe to `Monolith`
- `ServiceNfr` and `ServiceSpec` renamed to `MonolithNfr` and `MonolithSpec`
- Style improvement:  `PageTitle`, `bg-color`
- Nested record classes were causing compilation error in generated controllers 
  when used as parameter or return type, fixed