# Errors

On this page you can find the list of erros defined in baked.

## `invalid-state`

This error occurs when a convention filters out a condition but still encounters
that condition. Make sure your `when:` condition in the convention matches the
requirement in apply section (`attribute:`, `component:` or `schema:`) of the
convention.

## `method-required`

This error is raised when a page is trying to be generated out of a method.
You're probably misspelled the method name or set a method name from another
type.

To fix, make sure method name in `nameof(..)` belongs to the type in `MyType`
generic argument: `g => g.Method<MyType, MyPage>(nameof(MyType.MyMethod))`.

## `method-with-attribute`

It indicates that the type is required to have a method with the given
attribute. This requirement usually comes from a UX or coding style feature to
find the first matching method in a type.

To fix this, either add the given attribute to an existing method in the type or
add a new method that will match existing coding style so that it will be marked
automatically with the given attribute. For example, if the required attribute
is `InitializerAttribute`, it will be added to a method that is named as `With`.
Other option is to add it to an existing method using a domain override.

## `missing-item`

This error occurs when an element is assumed to be in a list but not found. Make
sure the name (or the filter you've given) has a corresponding in the list.

## `missing-required-schema`

This error occurs when a schema is required to build a component in page but it
is not configured through a convention.

To fix, you need to add a convention that covers the indicated path in the error.

## `missing-required-component`

This error occurs when a component is required but not configured through a
convention.

To fix, you need to add a convention that adds a component for the domain member
at the indicated path.

## `missing-required-component-of-type`

This error occurs when a component of given type is required but not configured
through a convention.

To fix this, you need to add a convention that adds a component of the given
type for the domain member at the indicated path.

> [!NOTE]
>
> This error raises even if there is some component for the domain member at the
> indicated path. This is because a SPECIFIC type of component, not ANY type, is
> required for that domain member.

## `parameter-with-attribute`

It indicates that the type is required to have a parameter with the given
attribute. This requirement usually comes from a UX or coding style feature to
find the first matching parameter in a method.

To fix this, either add the given attribute to an existing method in the method
or add a new method that will match existing coding style so that it will be
marked automatically with the given attribute.

## `property-with-attribute`

It indicates that the type is required to have a property with the given
attribute. This requirement usually comes from a UX or coding style feature to
find the first matching property in a type.

To fix this, either add the given attribute to an existing property in the type
or add a new property that will match existing coding style so that it will be
marked automatically with the given attribute. For example, if the required
attribute is `LabelAttribute`, it will be added to a property that is named as
`Name`. Other option is to add it to an existing property using a domain
override.

## `requires-controller`

It means that the indicated type is required to be a controller.

To fix this, look through your conventions where you require a controller over a
domain type using `type.GetControllerModel()`.

In Baked, `conventions.AddEntityRemoteData<MyEntity>()` helper assumes given
entity type has a controller model. Make sure you didn't call that helper over a
non-entity.

## `requires-element-type`

This error occurs when a convention mistakenly tries to get element type out of
a type that is not an array or an enumerable.

## `requires-initializer-action`

Indicated type requires an initializer method that is an API action.

An initializer is a method that serves as a builder method for a domain object,
e.g., a `With` method. For an initializer to become an API action it needs to be
`public` and all parameters should be in type that is marked with
`ApiInputAttribute`, e.g., primitives and your own domain types.

To fix this, go check if the type indicated in the error message has a public
initializer method (`public MyType With(...)`). If not, add one or make the
existing a public.

If the type is not expected to have a public initializer, than you need to
remove whatever convention is requiring it.

## `requires-locate-action`

Indicated type requires a `Locate` action for this operation. You may add a
locate action using `AddLocateAction`. This is required when you add remote data
using `AddEntityRemoteData`.

## `requires-members`

The indicated type should have been build at `BuildLevels.Members` level in
`DomainLayer`.

## `requires-metadata`

The indicated type should have been build at `BuildLevels.Metadata` level in
`DomainLayer`.

## `type-with-attribute`

It indicates that the type is required to have the given attribute.

To fix this, either add the given attribute to the type or remove the convention
that causes this requirement.
