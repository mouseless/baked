---
pages:
    - service
---

# Blueprints

DO consists of many features and layers as separate packages. You are free to
add necessary feature and layer packages to your project and forge an
application that fits your needs. However, there are also architectural
blueprints that include a set of layers and features with a default
configuration to allow you to reuse what is commonly needed for an application.

> :information_source: 
>
> You can override feature configurations. For more information you can refer
> to
> [Overriding A Configuration](../architecture/application.md#overriding-a-configuration)

Each blueprint comes with its own package that references to all the packages
it uses. This way your application will only need to depend on one blueprint
package.

## Forge Using a Blueprint

A blueprint comes with an extension to `Forge` class so that you can reuse it
directly in `Program.cs`. For example, for `Service` blueprint following call
will be sufficient to forge a service application;

```csharp
Forge.New
    .Service(
        business: c => c.MyBusiness(),
        database: c => c.Sqlite()
    )
    .Run();
```
