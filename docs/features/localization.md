# Localization

This feature allows you to perform localization.

Add this feature implementations using `AddLocalization()` extension;

```csharp
app.Features.AddLocalization(...);
```

## Resources

Keep the localize keys in the `.restext` and `.resx` files under the `Resources`
folder in the root of your application project, using the name you specified in
the configuration file.

```
App/
├─ Resources/
│  ├─ {NameOfYourGivenInConfig}.restext
│  ├─ {NameOfYourGivenInConfig}.en.restext
└─ └─ {NameOfYourGivenInConfig}.tr.restext
```

> [!NOTE]
>
> If you want to use a resource file with the `.restext` extension, don't forget
> to register it as an embedded resource so that dotnet can read it as a
> resource file during the build.
>
> ```xml
> <ItemGroup>
>   <EmbeddedResource Include="Resources\*.restext" />
> </ItemGroup>
> ```

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
