using Baked.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked.Ux.ObjectWithListIsDataTable;

public static class ObjectWithListIsDataTableExtensions
{
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