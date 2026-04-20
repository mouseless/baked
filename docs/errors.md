# Errors

On this page you can find the list of erros defined in baked.

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

## `method-required`

This error is raised when a page is trying to be generated out of a method.
You're probably misspelled the method name or set a method name from another
type.

To fix, make sure method name in `nameof(..)` belongs to the type in `MyType`
generic argument: `g => g.Method<MyType, MyPage>(nameof(MyType.MyMethod))`.

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
