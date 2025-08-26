# Theme

This feature implementations configures `DomainModel` metadata attributes and
extracts component descriptors for ui theme pages and components of an
application.

## Admin

To add `Admin` theme feature;

```csharp
c => c.Admin(
    indexPage: ...,
    pages: [...],
    errorPageOptions: ep => ...,
    sideMenuOptions: sm => ...,
    hedaerOptions: h => ...
)
```
