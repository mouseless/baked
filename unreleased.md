# Unreleased

## Features

- Dynamic routing is now supported and can be used when;
  - navigating through pages
  - fetching data from backend
  
## Improvements

- `Parameters` now accept parameter class attribute for each parameter

## Breaking Changes

- `[...baked].vue` page is now not used, ``*.page.json` file paths are used as 
  route patterns and rendered directly with `Page.vue`
- `useRoute` composable now accepts property name as parameter to access 
  `params`, `query`
  - `useQuery` composable is now deprecated and will be removed in 
  further releases
```csharp
{
  // Obsolete
  data = Computed(Composables.UseQuery)

  // Use `UseRoute` with args
  data = Computed(Composables.UseRoute, options: o => o.Args.Add("params"))
}
```
