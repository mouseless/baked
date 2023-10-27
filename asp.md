# ASP.NET Core 7.0

## Rate limiting middleware in ASP.NET Core

## Authentication uses single scheme as DefaultScheme

Starting in ASP.NET Core 7.0, if (and only if) a single scheme is registered
in an application, that scheme is treated as the default. No default is set
when multiple schemes are registered. You can disable the new behavior by
setting the `Microsoft.AspNetCore.Authentication.SuppressAutoDefaultScheme`
app context flag.

## Parameter binding with DI in API controllers

```csharp
[Route("[controller]")]
[ApiController]
public class MyController : ControllerBase
{
    public ActionResult GetWithAttribute([FromServices] IDateTime dateTime)
                                                        => Ok(dateTime.Now);

    [Route("noAttribute")]
    public ActionResult Get(IDateTime dateTime) => Ok(dateTime.Now);
}
```

to disable, set

```csharp
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.DisableImplicitFromServicesParameters = true;
});
```

## JSON property names in validation errors

```csharp
builder.Services.AddControllers(options =>
{
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
});

builder.Services.AddControllers(options =>
{
    options.ModelMetadataDetailsProviders.Add(new NewtonsoftJsonValidationMetadataProvider());
}).AddNewtonsoftJson();
```

## Typed results for minimal APIs

```csharp
[TestClass()]
public class WeatherApiTests
{
    [TestMethod()]
    public void MapWeatherApiTest()
    {
        var result = WeatherApi.GetAllWeathers();
        Assert.IsInstanceOfType(result, typeof(Ok<WeatherForecast[]>));
    }
}
```

## Improved unit testability for minimal route handlers

```csharp
[Fact]
public async Task GetTodoReturnsTodoFromDatabase()
{
    // Arrange
    await using var context = new MockDb().CreateDbContext();

    context.Todos.Add(new Todo
    {
        Id = 1,
        Title = "Test title",
        Description = "Test description",
        IsDone = false
    });

    await context.SaveChangesAsync();

    // Act
    var result = await TodoEndpointsV1.GetTodo(1, context);

    //Assert
    Assert.IsType<Results<Ok<Todo>, NotFound>>(result);

    var okResult = (Ok<Todo>)result.Result;

    Assert.NotNull(okResult.Value);
    Assert.Equal(1, okResult.Value.Id);
}
```

## New HttpResult interfaces

- `IContentTypeHttpResult`
- `IFileHttpResult`
- `INestedHttpResult`
- `IStatusCodeHttpResult`
- `IValueHttpResult`
- `IValueHttpResult<TValue>`

## OpenAPI improvements for minimal APIs

```csharp
app.MapPost("/todo2/{id}", async (int id, Todo todo, TodoDb db) =>
{
    todo.Id = id;
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
})
.WithOpenApi(generatedOperation =>
{
    var parameter = generatedOperation.Parameters[0];
    parameter.Description = "The ID associated with the created Todo";
    return generatedOperation;
});
```

## extension methods `WithDescription` and `WithSummary` or use attributes `[EndpointDescription]` and `[EndpointSummary]`

## File uploads using `IFormFile` and `IFormFileCollection`

```csharp
app.MapPost("/upload", async (IFormFile file) =>
{
    var tempFile = Path.GetTempFileName();
    app.Logger.LogInformation(tempFile);
    using var stream = File.OpenWrite(tempFile);
    await file.CopyToAsync(stream);
});

app.MapPost("/upload_many", async (IFormFileCollection myFiles) =>
{
    foreach (var file in myFiles)
    {
        var tempFile = Path.GetTempFileName();
        app.Logger.LogInformation(tempFile);
        using var stream = File.OpenWrite(tempFile);
        await file.CopyToAsync(stream);
    }
});
```

## `[AsParameters]` attribute enables parameter binding for argument lists

```csharp
[System.AttributeUsage(System.AttributeTargets.Parameter, AllowMultiple=false, Inherited=false)]
public sealed class AsParametersAttribute : Attribute
```

## `IProblemDetailsService` interface, which supports creating Problem Details for `HTTP APIs`

## Route groups

```csharp
public static RouteGroupBuilder MapTodosApi(this RouteGroupBuilder group)
{
    group.MapGet("/", GetAllTodos);
    group.MapGet("/{id}", GetTodo);
    group.MapPost("/", CreateTodo);
    group.MapPut("/{id}", UpdateTodo);
    group.MapDelete("/{id}", DeleteTodo);

    return group;
}

//////

var all = app.MapGroup("").WithOpenApi();
var org = all.MapGroup("{org}");
var user = org.MapGroup("{user}");
user.MapGet("", (string org, string user) => $"{org}/{user}");
```

## HTTP/3 improvements

- HTTP/3 fully supported
- Kestrel's support for HTTP/3.
- UseHttps(ListenOptions, X509Certificate2) with HTTP/3.
- support for HTTP/3 on HTTP.sys and IIS.

## Shadow copying in IIS

## Sqlite

Look here for more detail
[Breaking Changes Sqlite](https://github.com/dotnet/SqlClient/blob/main/release-notes/4.0/4.0.0.md#breaking-changes)

## Middleware no longer defers to endpoint with null request delegate

Bi bakılmalı
