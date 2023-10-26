# Net 7

## Rate limit an HTTP handler in .NET

Gelen http isteklerini sınırlandırabiliriyoruz.

## Coding Style

### csharp_style_prefer_null_check_over_type_check

```csharp
// Violates IDE0150.
if (numbers is not IEnumerable<int>) ...

// Fixed code.
if (numbers is null) ...
```

### csharp_style_prefer_extended_property_pattern

```csharp
public record Point(int X, int Y);
public record Segment(Point Start, Point End);

// Violates IDE0170.
static bool IsEndOnXAxis(Segment segment) =>
    segment is { Start: { Y: 0 } } or { End: { Y: 0 } };

// Fixed code.
static bool IsEndOnXAxis(Segment segment) =>
    segment is { Start.Y: 0 } or { End.Y: 0 };
```

### csharp_style_prefer_tuple_swap

```csharp
List<int> numbers = new List<int>() { 5, 6, 4 };

// Violates IDE0180.
int temp = numbers[0];
numbers[0] = numbers[1];
numbers[1] = temp;

// Fixed code.
(numbers[1], numbers[0]) = (numbers[0], numbers[1]);
```

## .NET regular expression source generators

```csharp
private static readonly Regex s_abcOrDefGeneratedRegex =
new(pattern: "abc|def",
    options: RegexOptions.Compiled | RegexOptions.IgnoreCase);

private static void EvaluateText(string text)
{
    if (s_abcOrDefGeneratedRegex.IsMatch(text))
    {
        // Take action with matching text
    }
}
// You can now rewrite the previous code as follows:

[GeneratedRegex("abc|def", RegexOptions.IgnoreCase, "en-US")]
private static partial Regex AbcOrDefGeneratedRegex();

private static void EvaluateText(string text)
{
    if (AbcOrDefGeneratedRegex().IsMatch(text))
    {
        // Take action with matching text
    }
}
```

## Generic math

```csharp
static T Add<T>(T left, T right)
    where T : INumber<T>
{
    return left + right;
}
```

## Containerize a .NET app with dotnet publish

Container images are now a supported output type of the .NET SDK, and you can
create containerized versions of your applications using dotnet publish.

## dotnet workload command

## [Source generation for platform invokes](https://learn.microsoft.com/en-us/dotnet/standard/native-interop/pinvoke-source-generation)

## [Customize a Json Contract](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/custom-contracts)

## open telemetry ile proje metriclerini takip etmeyi sağlıyor

## Breaking Changes

- Returning a collectible `Assembly` in the
 `AssemblyLoadContext.Load(AssemblyName)` override or the
 `AssemblyLoadContext.Resolving` event of a non-collectible
 `AssemblyLoadContext` throws a `System.IO.FileLoadException`
 with a `NotSupportedException` as the inner exception.
- A FormatException is thrown if the precision is greater than 999,999,999.
  This change was implemented in the parsing logic that affects all numeric
  types.
- ContentRootPath for apps launched by Windows Shell
  Host.CreateDefaultBuilder no longer defaults the ContentRootPath property to
  the current directory if it's the System special folder on Windows. Instead,
  the base directory of the application is used.
- Environment variable prefixes

  ```csharp
  //// old
  const string myValue = "value1";
  Environment.SetEnvironmentVariable("MY_PREFIX__ConfigKey", myValue);

  IConfiguration config = new ConfigurationBuilder()
      .AddEnvironmentVariables(prefix: "MY_PREFIX:")
      .Build();

  //// new
  const string myValue = "value1";
  Environment.SetEnvironmentVariable("MY_PREFIX__ConfigKey", myValue);

  IConfiguration config = new ConfigurationBuilder()
      .AddEnvironmentVariables(prefix: "MY_PREFIX__")
      .Build();
  ```

- Solution-level --output option no longer valid for build-related commands
  The dotnet CLI will error if the --output/-o option is used with a solution
  file. Starting in the 7.0.201 SDK, a warning will be emitted instead, and in
  the case of dotnet pack no warning or error will be generated.
- SDK no longer calls ResolvePackageDependencies
  Package information is added from `PreprocessPackageDependenciesDesignTime`
  into the design-time build cache. If you depended on `PackageDependencies`
  and `PackageDefinitions` in your build, you'll see build errors such as `No
  dependencies found`.
