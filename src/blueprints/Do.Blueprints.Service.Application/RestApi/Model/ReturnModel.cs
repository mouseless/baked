﻿using Do.Domain.Model;

namespace Do.RestApi.Model;

public record ReturnModel(TypeModel TypeModel)
{
    public string Type { get; set; } = TypeModel.CSharpFriendlyFullName;
    public bool IsAsync { get; set; } = TypeModel.IsAssignableTo<Task>();
    public bool IsVoid { get; set; } = TypeModel.Is(typeof(void)) || TypeModel.Is<Task>();
}