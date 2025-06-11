# Localization

This feature allows you to perform localization.

Add this feature implementations using `AddLocalization()` extension;

```csharp
app.Features.AddLocalization(...);
```

## Resources

Keep the localize keys in the `.restext` and `.resx` files under the `Locales`
folder in the root of your application project.

```
App/
├─ Locales/
│  ├─ locale.restext
│  ├─ locale.en.restext
└─ └─ locale.tr.restext
```

## Exceptions

We provide localization support for exceptions, allowing users to view exception
messages in their preferred languages.

```csharp
public class CustomException
    : HandledException(
        message: "Custom_exception_message_key__PARAM",
        extraData: new() { ["param"] = "param_value" }
    );
```
