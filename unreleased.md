# Unreleased

## Improvements

- __DO__ is renamed as __Baked (Objects)__
  - Root namespace is `Baked`
  - `Forge` is now `Bake`
  - `Bluprints` are now `Recipes`
- `Authentication.Disabled` was removed
- NHibernate logs are now redirected to logger instead of direct console logging
- Default levels are added to enable request/response and sql logging for
  development, only error for production

## Bugfixes

- NHibernate proxies were causing serialization error, fixed
