# Mock Overrider

This feature provides an overrider mechanism for overriding default components 
registered in `IServiceColleciton`.

Add this feature using `AddMockOverrrider()` extension;

```csharp
app.Features.AddMockOverrider(...);
```

## First Interface

This feature implementation provides a `IMockOverrider`implementation which
overrides components with mocks added to `MockDescriptor` collection 
according to their type's first interface.

```csharp
public class MockOverrider{

    ...

    public void Override(object mocked)
    {
        _overrides.TryAdd(mocked.GetType().GetInterfaces().First(), mocked);
    }
}
```

Than all components are added to service collection during `AddServices` phase of 
testing layer.

```csharp
foreach (var mock in _configuration.Mocks)
{
    if (mock.Singleton)
    {
        services.AddSingleton(mock.Type, sp => _configuration.MockFactory.Create(sp, mock));
    }
    else
    {
        services.AddTransient(mock.Type, sp => _configuration.MockFactory.Create(sp, mock));
    }
}
```

