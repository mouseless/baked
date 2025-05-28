using Baked.Ui;

namespace Baked.Theme.Admin;

public record Footer(IComponentDescriptor Profile, IComponentDescriptor? LanguageSwitcher)
    : IComponentSchema;