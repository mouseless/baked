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

## Actions are Grouped as Tabs

Groups controller actions into tabs on a `ReportPage`.

```csharp
c => c.ActionsAreGroupedAsTabs()
```

- Methods with `TabAttribute` are collected
- Each unique tab name creates a new `ReportPage.Tab`
- Actions inside that tab are added as `ReportPage.Tab.Content`
- Tab titles are automatically localized and formatted (e.g., `SampleTab` →
  `Sample Tab`) when there are more than one tabs

## Actions as Data Panels

Renders controller actions as `DataPanel` components inside a `ReportPage`.

```csharp
c => c.ActionsAsDataPanels()
```

- Methods with `ActionModelAttribute` become `DataPanel` components
- Each action is shown inside the tab content where it belongs
- The panel title is taken from the method name
- Action parameters are added to the panel schema automatically

## Designated String Properties are Label

```csharp
c => c.DesignatedStringPropertiesAreLabel(propertyNames: [...])
```

Marks selected string properties as labels to have a better display in
`DataTable` columns.

- Properties with matching names are given `LabelAttribute`
- Label columns in a `DataTable` are frozen and have minimum width
- The first label column is used as the table’s data key if no key is set

> [!NOTE]
>
> Default value of `propertyNames` is `["Display", "Label", "Name", "Title"]`.

## Enum Parameter is Select

```csharp
c => c.EnumParameterIsSelect(maxMemberCountForSelectButton: ...)
```

Renders enum parameters as `Select` by default.

- By default, enum parameters are shown as a `Select` dropdown
- When the number of enum members is less than or equal to the given limit, it
  switches to a `SelectButton`
- Optional enum parameters allow clearing the selection
- Required enum parameters default to the first enum member

> [!NOTE]
>
> Default value of `maxMemberCountForSelectButton` is `3`.

## Initializer Parameters are in Page Title

```csharp
c => c.InitializerParametersAreInPageTitle()
```

Adds initializer parameters of a report class to the page title area of a
`ReportPage`.

- Adds initializer parameters as query parameters of the page
- Works for types marked with `TransientAttribute`

## List is Data Table

```csharp
c => c.ListDataTable()
```

Shows list results of controller actions as a `DataTable` inside a `DataPanel`.

- Methods with `ActionModelAttribute` that return a list are rendered as
  `DataTable`
- The `DataTable` is placed in the action’s panel content
- Properties of the list element type are added as table columns automatically

## Numeric Values are Formatted

```csharp
c => c.NumericValuesAreFormatted()
```

Right-aligns numeric columns and uses suitable components for each numeric type.

- `int` properties render with `Number`
- `decimal` properties render with `Money`
- `double` properties render with `Rate`
- `DataTable` columns for `int`, `double`, `decimal` are right-aligned

## Object with List is Data Table

```csharp
c => c.ObjectWithListIsDataTable()
```

Shows list data from an object result as a `DataTable` inside a `DataPanel`.

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

```csharp
c => c.PanelParametersAreStateful()
```

Keeps parameter selections inside a `DataPanel` when the panel reloads.
Parameters rendered with `Select` and `SelectButton` are marked as stateful.

> [!WARNING]
>
> This only prevents losing user selection when changing query parameters of a
> report or navigating to different pages __without__ refreshing. When a refresh
> occurs, client app is reloaded and all panel states are reset to their
> default.

## Type with Only `GET` is Report Page

```csharp
c => c.TypeWithOnlyGetIsReportPage()
```

Creates a `ReportPage` for controller types that only have `GET` actions.

- Applies to types marked with `ControllerModelAttribute`
- If all actions are `GET` methods, the type is rendered as a `ReportPage`
- Each `GET` action is added as a tab content in the page
