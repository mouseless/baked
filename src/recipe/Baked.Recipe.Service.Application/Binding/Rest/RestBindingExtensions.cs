using Baked.Binding.Rest;
using Baked.Business;
using Baked.Domain.Model;
using Baked.RestApi.Model;

namespace Baked;

public static class RestBindingExtensions
{
    public static RestBindingFeature RestBinding(this BusinessConfigurator _) =>
        new();

    public static bool IsPublicInstanceWithNoSpecialName(this MethodOverloadModel overload) =>
        overload.IsPublic && !overload.IsStatic && !overload.IsSpecialName;

    public static bool AllParametersAreApiInput(this MethodOverloadModel overload) =>
        overload.Parameters.All(IsApiInput);

    public static bool IsApiInput(this Domain.Model.ParameterModel parameter) =>
        parameter.ParameterType.TryGetMetadata(out var metadata) && metadata.Has<ApiInputAttribute>();

    public static bool IsTarget(this RestApi.Model.ParameterModel parameter) =>
        parameter.Id == "target";
}