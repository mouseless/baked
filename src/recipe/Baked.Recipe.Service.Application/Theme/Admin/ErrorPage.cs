﻿using Baked.Ui;

namespace Baked.Theme.Admin;

public record ErrorPage : INamedComponentSchema
{
    public Dictionary<int, object> ErrorInfos { get; init; } = [];
    public List<IComponentDescriptor> SafeLinks { get; init; } = [];

    public string Name => "errorPage";
}