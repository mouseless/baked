# UX

User eXperience (UX) features are sets of component and schema conventions that
add specific behavior to the current theme feature. Add instances of these
features using the `AddUx()` extension;

```csharp
app.Features.AddUx([...]);
```

> [!NOTE]
>
> Notice this is a multiple feature, meaning you may add more than one UX
> features. Since `AddUxes()` doesn't read well, while being still available, we
> kept the singular `AddUx` naming as the default.

## Actions as Buttons

Renders non-`GET` actions as buttons under paths ending with `actions/*`.

```csharp
c => c.ActionsAsButtons()
```

- Parameterized actions are rendered as forms in dialogs
  - When an action has `RouteAttribute`, it is rendered as a `Button` that
    routes to the indicated path
- Submit buttons are rendered using `primary` severity
- Cancel and back buttons are rendered using `text` variant
- Default icons of buttons are added based on their HTTP method
  - `PUT` and `PATCH` use `pi pi-pencil`
  - `DELETE` use `pi pi-trash` with `danger` severity
  - `POST` that starts with add, create or new use `pi pi-plus`

## Actions are Contents

Adds `GET` actions as contents for `SimplePage` and `TabbedPage`. It also groups
contents under configured tabs for `TabbedPage`.

```csharp
c => c.ActionsAreContents()
```

- All `GET` actions of a type, except initializers, are added as contents
  - Without a `Content` configuration for a method at expected path, method will
    be skipped
- For `TabbedPage`, actions are grouped under tabs using their tab name in
  `TabNameAttribute`
  - Tab titles are automatically localized and formatted (e.g., `SampleTab` →
    `Sample Tab`) when there are more than one tabs

## Actions as Data Panels

Renders controller actions as `DataPanel` components inside a `TabbedPage`.

```csharp
c => c.ActionsAsDataPanels()
```

- Methods with `ActionModelAttribute` become `DataPanel` components
- Each action is shown inside the tab content where it belongs
- The panel title is taken from the method name
- Action parameters are added to the panel schema automatically

## Data Table defaults

Configures `DataTable` components with a bunch of default settings.

```csharp
c => c.DataTableDefaults()
```

- Sets row count to 5 and adds paginator
- Adds properties with `DataAttribute` as columns
  - For locatable properties, uses the first label (or id) property as component
    data, e.g., `row.parent.name`
  - Otherwise, sets the property value as component data, e.g., `row.name`
- Hides locatable properties of type as the same as the type of current page,
  e.g., `Child.Parent` column is hidden under any `Parent` page
- Adds export action to header
- Prepares action column to include item actions along with a reload reaction
- Action and dialog buttons use `text` variant using rounded style

## Description Property

Marks properties that ends with `*Description` using `DescriptionAttribute` and
treats properties with `DescriptionAttribute` special attention to allow more
UI space when under a `DataTable` or a `Fieldset`.

- Set `Field.Wide` to `true` to have a full width under a fieldset
- Sets up a dialog button to show the content of description properties in a
  dialog under data tables

## Enum Parameter is Select

Renders enum parameters as `Select` by default.

```csharp
c => c.EnumParameterIsSelect(maxMemberCountForSelectButton: ...)
```

- By default, enum parameters are shown as a `Select` dropdown
- When the number of enum members is less than or equal to the given limit, it
  switches to a `SelectButton`
- Optional enum parameters allow clearing the selection
- Required enum parameters default to the first enum member

> [!NOTE]
>
> Default value of `maxMemberCountForSelectButton` is `3`.

## Initializer Parameters are in Page Title

Adds initializer parameters of a report class to the page title area of a
`TabbedPage`.

```csharp
c => c.InitializerParametersAreInPageTitle()
```

- Adds initializer parameters as query parameters of the page
- Works for types marked with `TransientAttribute`

## Labels are Frozen

Configure label properties (properties with `LabelAttribute`) to have a better
display in `DataTable` columns.

```csharp
c => c.LabelsAreFrozen()
```

- Label columns in a `DataTable` are frozen and have minimum width
- The first label column is used as the table’s data key if no key is set

## List is Data Table

Shows list results of controller actions as a `DataTable` inside a `DataPanel`.

```csharp
c => c.ListDataTable()
```

- Methods with `ActionModelAttribute` that return a list are rendered as
  `DataTable`
- The `DataTable` is placed in the action’s panel content
- Properties of the list element type are added as table columns automatically

## Numeric Values are Formatted

Right-aligns numeric columns and uses suitable components for each numeric type.

```csharp
c => c.NumericValuesAreFormatted()
```

- `int` properties render with `Number`
- `decimal` properties render with `Money`
- `double` properties render with `Rate`
- `DataTable` columns for `int`, `double`, `decimal` are right-aligned

## Object with List is Data Table

Shows list data from an object result as a `DataTable` inside a `DataPanel`.

```csharp
c => c.ObjectWithListIsDataTable()
```

- Methods with `ActionModelAttribute` that return an object containing a visible
  list property are rendered as `DataTable`
- The list property is detected automatically and used as the data source
- Properties of the list element type are added as table columns
- Other properties of the object are shown in the table footer as summary
  columns

> [!TIP]
>
> This feature uses `ObjectWithListAttribute` to decide which list property to
> render in a `DataTable`. You can override an existing attribute to point to
> another `IEnumerable` property. You can also add this attribute to a type so
> that actions returning it are rendered as a `DataTable` as well.

## Panel Parameters are Stateful

Keeps parameter selections inside a `DataPanel` when the panel reloads.
Parameters rendered with `Select` and `SelectButton` are marked as stateful.

```csharp
c => c.PanelParametersAreStateful()
```

> [!WARNING]
>
> This only prevents losing user selection when changing query parameters of a
> report or navigating to different pages __without__ refreshing. When a refresh
> occurs, client app is reloaded and all panel states are reset to their
> default.

## Properties as Fieldset

Allows to add properties as a `Fieldset` component under `SimplePage`.

```csharp
c => c.PropertiesAsFieldset()
```

- A content is created under `.../simple-page/contents/fields` path
- Each data property is configured as one field under a `Fieldset` instance
- Field components get their data from parent context

## Routed Types as Nav Links

Configures `NavLink` component for types that have `RouteAttribute` under data
table columns.

```csharp
c => c.RoutedTypesAsNavLinks()
```

- Converts label properties to a `NavLink` using `RouteAttribute` route params

> [!NOTE]
>
> This will support other paths than data table column in the upcoming releases.
