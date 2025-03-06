﻿namespace Baked.Ui;

public record ComputedData(string Composable)
    : IData
{
    public string Type => "Computed";
    public string Composable { get; set; } = Composable;
}