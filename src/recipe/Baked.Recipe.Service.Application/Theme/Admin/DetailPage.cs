﻿using Baked.Ui;
using Humanizer;

namespace Baked.Theme.Admin;

public record DetailPage : IComponentSchema
{
    public IComponentDescriptor? Header { get; set; }
    public List<Property> Props { get; init; } = [];

    public record Property(string Key)
    {
        public string Key { get; } = Key;
        public string Title { get; set; } = Key.Humanize();
        public IComponentDescriptor Component { get; set; } = Components.String();
    }
}