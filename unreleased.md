# Unreleased

## Features

- `Labeler` component is introduced to standardize labeling of input components
  - `ILabeler` schema interface is introduced to allow conventions to change 
    label configuration at once
  - `Select`, `SelectButton`, `InputText`, `InputNumber` now implements
    `ILabeler`
  - `.LabelIfta(...)`, `.LabelFloatOn(...)`, `.LabelFloatIn(...)`,
    `LabelFloatOver(...)` and `.LabelNone()` extensions are available for any
    component schema that implements `ILabeler`

# Breaking Changes
## Features

- `QueryMethodCodingStyle` feature is now added which marks methods of a query
  as `QueryMethod` along with `sort`, `skip` and `take` parameters
- New UX features are introduced in `Monolith` recipe
  - `QuerActionAsDataContainerUxFeature` to configure descriptor properties of
    query methods 
- `DataContainer` component is added to render enumerable datas with basic input
  support
  - `PageSize` component is added which is an override of `Select`
  - `Paginator` component is added for paging

## Breaking Changes

- `CodeGeneration` namespace is changed to `Builtime`, along with its layer name
  and extensions
  - `configurator.CodeGeneration` is now `configurator.Buildtime`
  - `CodeGenerationLayer` is now `BuildtimeLayer`
  - Anything under `Baked.CodeGeneration` is now under `Baked.Buildtime`
- `CodeGeneration.GenerateCode` phase is now renamed as `Buildtime.Generate`
- `DescriptorBuilderAttribute` and `ComponentDescriptorBuilderAttribute` are
  renamed as `GeneratorAttribute` and `ComponentGeneratorAttribute` respectively
  - `Build` method is renamed as `Generate`
- `GetSchema`, `GetSchemas`, `GetRequiredSchema`, `GetComponent` and
  `GetRequiredComponent` extension methods are renamed as `GenerateSchema`,
  `GenerateSchemas`, `GenerateRequiredSchema`, `GenerateComponent` and
  `GenerateRequiredComponent` respectively
- `FormPage` schema is completely redesigned, migrate your existing
  configurations to match the new one
- `GroupAttribute` now supports context based names through `this[string
  context]` indexer property
  - Use extension properties to set/get a group name for your custom context,
    see
    ```csharp
    extension(GroupAttribute group)
    {
        public string MyCustomName { get => group["MyCustom"]; set => group["MyCustom"] = value; }
    }
    ```
- `TabNameAttribute` is now removed, instead get `GroupAttribute` and use its
  `TabName` extension property
- `Select`, `InputText`, `InputNumber` component schemas does not require
  `label:` in their component builders and constructors any more
- `ServerPaginator` is renamed to `Paginator` and converted to a component
- `ServerPaginator` is removed from `DataTable` schema
- `Take` is removed from `DataTable` schema  
- `Sort` is removed from `DataTable` schema

## Improvements

- Inspection mechanism
  - Add trace wasn't showing up when initial value is null, fixed
  - JSON serialization is restricted to only anonymous types to avoid
    unnecessarily long (and for some attributes failing) serializations
- `GiveMe.AnEnum<T>` is now available, it returns the first element of given
  enum
- `FormPage` now restricts tab navigation with only its inputs via `v-focustrap`
- `Input` now supports numeric datas

## Bugfixes

- Component action having `undefined` model value when `Input` was configured 
  query bound, fixed

