# Service

Service blueprint is the default backend blueprint provided by DO which
includes necessary layers and feature implementations for any backend
application.

> [!TIP]
>
> This blueprint is under development and will be detailed as it becomes more
> mature.

To create an application from this blueprint, use `Service()` extension of
`Forge` class directly in `Program.cs`.

```csharp
Forge.New
    .Service(
        business: c => c.MyBusiness(),
        database: c => c.Sqlite()
    )
    .Run();
```

Layers in this blueprint are;

| Layers               | Run | Test |
| -------------------- | --- | ---- |
| Configuration        | +   | +    |
| Data Access          | +   | +    |
| Dependency Injection | +   | +    |
| Domain               | +   | +    |
| Http Client          | +   |      |
| Http Server          | +   |      |
| Monitoring           | +   | +    |
| Rest Api             | +   | +    |
| Testing              |     | +    |

Features with default options are;

| Features           | Run           | Test            | Required |
| ------------------ | ------------- | --------------- | -------- |
| Authentication     | FixedToken    | FixedToken      |          |
| Business           | Default       | Default         | Yes      |
| Caching            | Scoped Memory | Scoped Memory   |          |
| Communication      | Http          | Mock            |          |
| Core               | Dotnet        | Mock            |          |
| Database           | Sqlite        | InMemory        | Yes      |
| Documentation      | Default       |                 |          |
| Exception Handling | Default       |                 |          |
| Greeting           | Hello World   |                 |          |
| Logging            | Request       |                 |          |
| Mocking Overrider  |               | First Interface |          |
| Orm                | Default       | Default         |          |

Phase execution order;

```mermaid
flowchart TD
    AS[AddServices]
    B[Build]
    BC[BuildConfiguration]
    BD[BuildDomain]
    CB[CreateBuilder]
    R[Run]
    CB -->|ConfigurationManager| BC
    CB -->|ConfigurationManager| BD
    BD -->|DomainModel| AS
    CB -->|WebApplicationBuilder| B
    AS -->|IServiceCollection| B
    B -->|WebApplication|R
```
