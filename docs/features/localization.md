# Localization

This feature registers `.NET` localization services and middleware, and
generates `.json` resource files for to be used for UI app localization

Add this feature implementations using `AddLocalization()` extension;

```csharp
app.Features.AddLocalization(...);
```

## .NET

This feature adds .NET's standard localization service. Add the service using
`c.Dotnet(...)` extension method.

```csharp
c => c.Dotnet(
    language: new("en"),
    otherLanguages: [new("tr")]
)
```

### Resources

Keep the localize keys in the `.restext` and `.json` files under the `Locales`
and `locales` folder in the root of your backend (app) and frontend projects
respectively.

```
App/
├─ Locales/
│  ├─ locale.en.restext
└─ └─ locale.tr.restext
```

```
UI/
├─ .baked/
│  ├─ locale.en.json
│  └─ locale.tr.json
├─ locales/
│  ├─ locale.en.json
└─ └─ locale.tr.json
```
