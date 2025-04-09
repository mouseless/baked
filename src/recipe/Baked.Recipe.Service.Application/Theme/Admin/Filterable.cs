using Baked.Ui;

namespace Baked.Theme.Admin;

public record Filterable(string Title, IComponentDescriptor Component)
    : IComponentSchema;