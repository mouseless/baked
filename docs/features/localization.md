# Localization

This feature registers `.NET` localization services and middleware, and 
generates `.json` resource files for to be used for UI app localization

Add this feature implementations using `AddLocalization()` extension;

```csharp
app.Features.AddLocalization(...);
```

## ASP.NET Core

This feature adds ASP.NET Core's localization service. Add the service using
`c.AspNetCore(...)` extension method.

```csharp
c => c.AspNetCore(
    language: new("en"),
    otherLanguages: [new("tr")]
)
```

### Resources

Keep the localize keys in the `.restext` and `.resx` files under the `Locales`
folder in the root of your application project.

```
App/
├─ Locales/
│  ├─ locale.restext
│  ├─ locale.en.restext
└─ └─ locale.tr.restext
```
