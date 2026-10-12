# Unreleased

## Features

- `Business` namespace now provides `Validate` object to perform business
  validations
- `FlagsEnumCodingStyle` is now available that supports enums with `[Flags]`
- `StatusEnumCodingStyle` is now available that provides a status enum system
  using codes that are similar to HTTP status codes
- `IExportOptions` in `Baked.Binding` lets an attribute set the name it is
  exported with, e.g., `IdProperty` is exported as `@id`
- All component, data, action, trigger and constraint schemas now have a
  parameterless constructor, so a convention can add any of them by its type,
  e.g., `conventions.AddMethodComponent<DataPanel>()`
- Values are no longer set while adding, they are set by edit conventions, so
  adding and configuring a schema are separate concerns
- Default theme provides default conventions for the schemas it adds at
  `Order.At.Min`, so an added schema comes configured without any extra
  convention
  - To change a default, add an edit convention at default order, e.g.,
    `conventions.EditMethodSchema<RemoteData>(schema: rd => ...)`
- A schema is turned into a component descriptor with `ToDescriptor()`, which
  also accepts `data` and `action`
- New display components are added; `Tag` and `Avatar`
- Button with dialog mode of `SimpleForm` is migrated to `Button`
  - You can now simplify conventions by configuring methods as buttons with
    dialogs instead of simple form in dialog mode

## Breaking Changes

- Features are now defined with `Define()` of their abstraction configurator,
  feature classes and `XConfigurator` classes are removed
  ```csharp
  // was
  public class WelcomeGreetingFeature : IFeature<GreetingConfigurator>
  {
      public void Configure(LayerConfigurator configurator) { ... }
  }

  // now
  public static IFeature<GreetingFeature.Configurator> Welcome(this GreetingFeature.Configurator x) =>
      x.Define(configurator => { ... });
  ```
  - Every abstraction now provides `Disabled()`, `Feature.Empty<T>()` is
    removed
- Attributes used in conventions no longer have the `Attribute` suffix, e.g.,
  `LabelAttribute` -> `Label`. These also changed beyond the suffix;
  | old                        | new             |
  | -------------------------- | --------------- |
  | `ControllerModelAttribute` | `ApiController` |
  | `ActionModelAttribute`     | `ApiAction`     |
  | `ParameterModelAttribute`  | `ApiParameter`  |
  | `ActionAttribute`          | `UiAction`      |
  | `DataAttribute`            | `UiData`        |
  | `DescriptionAttribute`     | `UiDescription` |
  | `RouteAttribute`           | `UiRoute`       |
  | `ApiInputAttribute`        | `Bindable`      |
  | `IdAttribute`              | `IdProperty`    |
  | `RichTransientAttribute`   | `Resource`      |
  | `ValueTypeAttribute`       | `Primitive`     |
  - `Bindable` moved from `Baked.RestApi.Model` to `Baked.Binding`
- Members named after a renamed attribute follow it;
  | old                          | new                          |
  | ---------------------------- | ---------------------------- |
  | `GetControllerModel()`       | `GetApiController()`         |
  | `TryGetControllerModel()`    | `TryGetApiController()`      |
  | `GetActionModel()`           | `GetAction()`                |
  | `IsApiInput`                 | `IsBindable`                 |
  | `AllParametersAreApiInput()` | `AllParametersAreBindable()` |
  | `TryGetLocatableAttribute()` | `TryGetLocatable()`          |
  | `ValueTypeUserType<T>`       | `PrimitiveUserType<T>`       |
- Coding styles are renamed to express how they detect types, `via` is used when
  the mechanism needs naming and `based` when it reads as a qualifier;
  | old                  | new                                |
  | -------------------- | ---------------------------------- |
  | `AddRemoveChild`     | `AddRemoveChildAsSubResource`      |
  | `Client`             | `SuffixBasedClient`                |
  | `CommandPattern`     | `CommandViaMethodName`             |
  | `Id`                 | `TypeBasedId`                      |
  | `Initializable`      | `InitializableViaMethodName`       |
  | `Label`              | `NameBasedLabel`                   |
  | `Locatable`          | `LocateViaId`                      |
  | `LocatableExtension` | `ExtensionViaLocatableInitializer` |
  | `Query`              | `QueryViaPluralName`               |
  | `RichTransient`      | `ResourceViaIdInitializer`         |
  | `ScopedBySuffix`     | `ScopedViaSuffix`                  |
  | `Unique`             | `UniqueViaSingleBy`                |
  | `ValueType`          | `PrimitiveViaParsable`             |
- Lifetime features are renamed after the lifetime they provide;
  | old         | new           |
  | ----------- | ------------- |
  | `Singleton` | `Application` |
  | `Scoped`    | `Scope`       |
  | `Transient` | `Instance`    |
- Conventions that configure an existing attribute, component or schema are
  renamed as edits
  ```csharp
  // was
  conventions.AddTypeAttributeConfiguration<Entity>(...);
  conventions.AddMethodComponentConfiguration<Button>(...);
  conventions.AddParameterSchemaConfiguration<Select>(...);

  // now
  conventions.EditTypeAttribute<Entity>(...);
  conventions.EditMethodComponent<Button>(...);
  conventions.EditParameterSchema<Select>(...);
  ```
- All schemas now have a parameterless constructor, their constructor parameters
  became settable properties, except that constraint and trigger properties
  became `required`
  > [!NOTE]
  >
  > Page `Path` is set from the route when it is left empty
- `DomainComponents`, `DomainDatas`, `DomainActions` and `Components` builder
  classes are removed, their defaults are now handled by the default theme. A
  schema is built with its own constructor and turned into a descriptor with
  `ToDescriptor()`
  ```csharp
  // was
  using B = Baked.Ui.Components;

  B.Filter(
      options: f => f.Placeholder = l("Filter"),
      action: Publish.Event("filter-changed")
  );
  B.PageTitle(options: pt => pt.LocalizeTitle = true);

  // now
  new Filter { Placeholder = l("Filter") }
      .ToDescriptor(action: Execute.Publish.Event("filter-changed"));
  new PageTitle { LocalizeTitle = true }.ToDescriptor();
  ```
  - `options` parameters are gone, `using static` and `using B =` are no longer
    needed
  - `Label` component is renamed as `Labeler`
- `Datas`, `Actions` and `Constraints` builder classes are renamed as `Fetch`,
  `Execute` and `Evaluate`
  ```csharp
  // was
  using static Baked.Ui.Datas;
  using static Baked.Ui.Actions;
  using static Baked.Ui.Constraints;

  conventions.AddMethodComponentConfiguration<Button>(component: c =>
  {
      c.Data = Remote("some/endpoint");
      c.Action = Publish.Event("saved");
      c.ShowWhen("state", constraint: Is("editing"));
  });

  // now
  conventions.EditMethodComponent<Button>(component: c =>
  {
      c.Data = Fetch.Remote("some/endpoint");
      c.Action = Execute.Publish.Event("saved");
      c.ShowWhen("state", constraint: Evaluate.Is("editing"));
  });
  ```
- `ExtensionViaLocatableInitializerCodingStyle` no longer requires extension
  classes to have an implicit operator, any transient with an initializer that
  has one locatable parameter becomes an extension for it
  > [!WARNING]
  >
  > This might make unintended classes an extension and change their API
  > endpoint routes, removing `LocatableExtension` attribute from them, or
  > adding another parameter to the initializer, resolves it
- `QueryMethodCodingStyle` is merged into `QueryViaPluralNameCodingStyle`
- `EntitySubclassCodingStyle` is removed completely
- `ICasts` interface and `Caster.Cast()` extension are removed
- `AllowAnonymous`, `ClientCache` and `NoTransaction` now declare
  `[AttributeUsage]`, so they are no longer included in every export target
- `SimpleForm` no more provides a dialog mode, switch to `Button` to render
  methods as button with a dialog
- Additional renamings are as below
  | old                                         | new                                    |
  | ------------------------------------------- | -------------------------------------- |
  | `Generator<T>.Generator`                    | `Generator<T>.GeneratorDelegate`       |
  | `Generator<T>.Filter`                       | `Generator<T>.FilterDelegate`          |
  | `ContextBasedComponent.Filter`              | `ContextBasedComponent.FilterDelegate` |
  | `EntityInitializerIsPostResourceConvention` | `EntityInitializerIsPostConvention`    |
  | `AuthorizedContent`                         | `Authorize`                            |

## Improvements

- It was impossible to validate records have locatables in their constructor due
  to late initialization, fixed
- `useFormat` now provides `asCamelCase` helper function
- `Authorization` improvements
  - It now supports claim type-value based restriction in API layer
  - `RequireUser` can now contain allowed values
  - It no longer requires all claims to exist in token separately and publishes
    roles under one claim name: `user_roles`
- `Authorize` improvements
  - It now supports claim based content hiding
  - You can now show a fallback component when user is not authorized
- `SideMenu` items can now be hidden depending on authentication and claims
- C# friendly name calculation is now fully compliant with C# language standards
- `Enum` defaults wasn't getting camelized in `Input`, fixed
- `Bake.vue` now waits until data is loaded to show a component when there is a
  show reaction in the descriptor
  - It was showing the components that reacts to `showWhen: is null` causing to
    render unintended components while page is loading
- Minor improvements in `components.css`

## Library Upgrades

| NuGet Package                             | Old Version | New Version |
| ---                                       | ---         | ---         |
| Microsoft.Testing.Extensions.CodeCoverage | 18.11.2     | 18.12.0     |
| Microsoft.Testing.Extensions.TrxReport    | 2.4.1       | 2.5.1       |
| NUnit3TestAdapter                         | 6.3.0       | 6.3.1       |
| Swashbuckle.AspNetCore                    | 10.2.3      | 10.3.0      |
| Swashbuckle.AspNetCore.Annotations        | 10.2.3      | 10.3.0      |

| npm Package | Old Version | New Version |
| ---         | ---         | ---         |
| playwright  | 1.63.0      | 1.64.0      |
