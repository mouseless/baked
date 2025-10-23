# Unreleased

## Improvements

- folder reorganization
- `Baked.Architecture` and `Baked.Recipe.Service` merged into `Baked`
- `Baked.Core` and `Baked.Service` merged into `Baked.Abstractions`
- Renamed `Service` recipe to `Monolith`
- `ServiceNfr` and `ServiceSpec` renamed to `MonolithNfr` and `MonolithSpec`
- UI components are moved from `Baked.Theme.Default` to `Baked.Ui` namespace
- Convention extensions are standardized
  - `whenType:`, `whenProperty:`, `whenMethod:`, `whenParameter:` are all
    renamed as `when:` to match other convention extensions
  - `apply:` parameter in metadata configuration extensions are renamed as
    `attribute:` to match `Set/Add`
  - First parameter of `when:` in metadata configuration extensions is changed
    to context instead of attribute for consistency with other extensions
  - All metadata convention and configurations are renamed as attribute, e.g.,
    `SetTypeMetadata` -> `SetTypeAttribute`
  - `whenComponent:` is renamed as `where:` for better readability
- `String` ui component is renamed to `Text`
- Style improvement:  `PageTitle`, `bg-color`
- Style improvement:  `PageTitle`, `bg-color`
- Nested types were causing compilation error in generated controllers when
  used as parameter or return type, fixed
- Logo is now hidden during generate
