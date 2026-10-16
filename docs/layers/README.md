# Layers

Layers set up the technologies an application runs on, such as ASP.NET Core for
HTTP, NHibernate for data access and Nuxt for UI. A layer does not decide how it
is configured, features configure layers through the targets they provide.

::columns

#add

### Adding Layers

Recipes add layers for you, or you can add them to an application directly;

```csharp
app.Layers.AddDomain();
app.Layers.AddHttpServer();
app.Layers.AddDataAccess();
```

#configure

### Configuring Layers

A feature configures a layer using its configuration targets;

```csharp
configurator.Runtime.ConfigureServiceCollection(services =>
{
    services.AddSingleton<IMyService, MyService>();
});
```

::

To learn how layers work and how to create one, see
[Layer](../architecture/layer.md).
