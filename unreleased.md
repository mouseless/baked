# Unreleased

## Features

- `Domain` layer now introduces a level system to improve managing convention
  execution orders.
  ```csharp
  configurator.Domain.ConfigureBuilder(builder =>
  {
    builder.ConventionOrderMatrix.Bases.Add("Base");
    ...
    builder.ConventionOrderMatrix.Levels.Add("Level");
    ...
    builder.ConventionOrderMatrix.Extensions.Add("Ext");
    ...

    builder.ConventionOrderMatrix.FallbackBase = convention => ...;
    builder.ConventionOrderMatrix.FallbackLevel = convention => ...;
    builder.ConventionOrderMatrix.FallbackExtension = convention => ...;

    builder.DefaultConventionLevel = "...";
  });

  configurator.Domain.ConfigureConventions(conventions => 
  {
    conventions.SetTypeAttribute(
      when: _ => true,
      attribute: () => new GroupAttribute(),
      order: Order.At.WithBase("Base").WithLevel("Level").WithExtension("Ext")
    );
  });
  ```

## Breaking Changes

- `requireIndex` convention flag is renamed to `beforeBuildingIndex`
- `ConfigureDomainModelBuilder` is is renamed to `ConfigureBuilder`
  ```csharp
  // old usage
  configurator.Domain.ConfigureDomainModelBuilder(builder =>
  {
      ...
  });

  // current usage
  configurator.Domain.ConfigureBuilder(builder =>
  {
      ...
  });
  ```
- `IDomainModelConventionCollection` is now provided as a configuration target
  ```csharp
  // old usage
  configurator.Domain.ConfigureBuilder(builder =>
  {
      builder.Conventions.Add(...);
  });

  // current usage
  configurator.Domain.ConfigureConventions(conventions =>
  {
      conventions.Add(...);
  });
  ```
- `order: {int}` will now be cast to an `Order` which will fallback to default
  convention level having (-5000, 4999) range, previous usages like below will
  result errors;
  ```csharp
  // this will throw error
  conventions.Add(..., order: int.MinValue + 10);

  // use below instead
  conventions.Add(..., order: Order.At.Global.Min);
  ```
- `Inpect` is now moved to `DomainModelBuilderOptions`
  ```csharp
  // old usage
  configurator.Domain.ConfigureInspect(inspect =>
  {
      inspect.Attribute<...>();
  });

  // new usage
  configurator.Domain.ConfigurBuilder(builder =>
  {
      builder.Inspect.Attribute<...>();
  });
  ```
  
## Bugfixes

- Adding locate action by conventions was missing claim requirements, fixed
