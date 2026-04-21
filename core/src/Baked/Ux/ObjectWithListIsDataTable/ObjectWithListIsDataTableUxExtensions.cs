using Baked.Domain.Model;
using Baked.Ux;
using Baked.Ux.ObjectWithListIsDataTable;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class ObjectWithListIsDataTableUxExtensions
{
    extension(UxConfigurator _)
    {
        public ObjectWithListIsDataTableUxFeature ObjectWithListIsDataTable() =>
            new();
    }

    extension(TypeModel type)
    {
        public bool TryGetListProperty([NotNullWhen(true)] out PropertyModel? result)
        {
            if (!type.TryGetMembers(out var returnMembers)) { result = null; return false; }
            if (!returnMembers.TryGet<ObjectWithListAttribute>(out var objectWithList)) { result = null; return false; }

            result = returnMembers.Properties[objectWithList.ListPropertyName];

            return true;
        }

        public PropertyModel GetListProperty()
        {
            if (!type.TryGetListProperty(out var result))
            {
                Diagnostics.ReportError(
                    DiagnosticsCode.PropertyWithAttribute,
                    $"{type.Name} is expected to have members and at least one property with `{nameof(ObjectWithListAttribute)}`"
                );
            }

            return result;
        }
    }
}