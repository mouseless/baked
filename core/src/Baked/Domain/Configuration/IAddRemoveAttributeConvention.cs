using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public interface IAddRemoveAttributeConvention : IDomainModelConvention
{
    bool AttributeRequiresIndex { get; }

    public static void ThrowIfNotTarget(ICustomAttributesModel model, Attribute attribute)
    {
        if (IsRuntimeAttribute(attribute.GetType()))
        {
            return;
        }

        var usages = (AttributeUsageAttribute?)Attribute.GetCustomAttribute(attribute.GetType(), typeof(AttributeUsageAttribute));
        var validOn = usages?.ValidOn ?? AttributeTargets.All;

        if (!validOn.HasFlag(model.Target))
        {
            throw new InvalidOperationException($"'{attribute.GetType().Name}' having target '{validOn}' does not have '{model.Target}' flag");
        }
    }

    static bool IsRuntimeAttribute(Type type) =>
        type.Assembly.GetName().Name == "System.Private.CoreLib" && $"{type.Namespace}".StartsWith("System.Runtime.CompilerServices");
}