using Do.Business;
using Do.Business.Default;
using Do.Domain.Model;
using System.Reflection;

namespace Do;

public static class DefaultBusinessExtensions
{
    public static DefaultBusinessFeature Default(this BusinessConfigurator _, List<Assembly> assemblies) =>
        new(assemblies);

    internal static bool IsIgnored(this TypeModel type) =>
        !type.IsBusinessType ||
        !type.IsPublic ||
        type.IsInterface ||
        type.Namespace?.StartsWith("System") == true ||
        (type.IsSealed && type.IsAbstract) || // if type is static
        type.IsAbstract ||
        type.IsValueType ||
        type.IsGenericMethodParameter ||
        type.IsGenericTypeParameter ||
        type.IsAssignableTo<MulticastDelegate>() ||
        type.IsAssignableTo<Exception>() ||
        type.IsAssignableTo<Attribute>() ||
        (type.ContainsGenericParameters && !type.GenericTypeArguments.Any()) ||
        type.HasAttribute<DataClassAttribute>()
    ;
}
