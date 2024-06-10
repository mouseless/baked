# Recipes

Baked consists of many features and layers as separate packages. You are free to
add necessary feature and layer packages to your project and bake an application
that fits your needs. However, there are also architectural recipes that include
a set of layers and features with a default configuration to allow you to reuse
what is commonly needed for an application.

> [!TIP]
>
> To override a built-in configuration see
> [Overriding A Configuration](../architecture/application.md#overriding-a-configuration)

Each recipe comes with its own package that references to all the packages it
uses. This way your application will only need to depend on one recipe package.
