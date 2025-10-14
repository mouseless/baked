using Baked.Domain.Model;
using Baked.Ux;
using Baked.Ux.ObjectWithListIsDataTable;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class ObjectWithListIsDataTableUxExtensions
{
    public static ObjectWithListIsDataTableUxFeature ObjectWithListIsDataTable(this UxConfigurator _) =>
        new();

    public static bool TryGetListProperty(this TypeModel type, [NotNullWhen(true)] out PropertyModel? result)
    {
        if (!type.TryGetMembers(out var returnMembers)) { result = null; return false; }
        if (!returnMembers.TryGet<ObjectWithListAttribute>(out var objectWithList)) { result = null; return false; }

        result = returnMembers.Properties[objectWithList.ListPropertyName];

        return true;
    }

    public static PropertyModel GetListProperty(this TypeModel type)
    {
        if (!type.TryGetListProperty(out var result)) { throw new($"{type.Name} is expected to have members and at least one property with `{nameof(ObjectWithListAttribute)}`"); }

        return result;
    }
}