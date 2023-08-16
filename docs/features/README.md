---
pages:
    - business
    - core
    - database
    - documentation
    - exception-handling
    - greeting
    - logging
    - mock-overrider
    - orm
---

# Features

Here you can find every feature that has been implemented in DO.

## Override Feature Configuration

To override the configurations of base features, we suggest you to add a
feature called `ConfigurationOverrider` and make overrides in the `IFeature`
implementation. To learn more about features, you can refer to
[Feature](../architecture/feature.md).

> :warning:
>
> If the configurations in other features you add conflict with the
> configurations in the base feature, the base features may not work as
> expected.
