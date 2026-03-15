# Binding

Add this feature implementations using `AddBindings()` extension;

```csharp
app.Features.AddBindings([...]);
```

## Rest

Binds domain types, methods and parameters as API controllers, actions and
parameters respectively.

Controller, action and parameters are represented via corresponding model
attributes, e.g., `ControllerModelAttribute`. Once these attributes are added to
domain model, this feature initializes them with default parameters.

These models are subject to alteration via coding styles and other features for
them to have a control on api rendering in `RestApiLayer`.

```csharp
c => c.Rest()
```

> [!NOTE]
>
> For now, this feature is automatically added to recipes since it is the only
> way for business to be exposed as API endpoints.
