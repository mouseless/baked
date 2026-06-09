using Baked.Domain.Configuration;
using Baked.Domain.Inspection;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class SetAttributeConvention<TModelContext>(
    Action<TModelContext, Action<ICustomAttributesModel, Attribute>> _apply,
    Func<TModelContext, bool> _when,
    Order _order,
    bool beforeBuildingIndexes = true
) : IDomainModelConvention<TModelContext>
    where TModelContext : DomainModelContext
{
    readonly Trace _trace = Trace.Here();
    readonly string _orderInfo = (beforeBuildingIndexes ? "+" : "-") + $"{_order}";

    bool IDomainModelConvention.BeforeBuildingIndexes => beforeBuildingIndexes;

    public void Apply(TModelContext context)
    {
        context.Trace = _trace;

        if (!_when(context)) { return; }

        _apply(context, (model, attribute) =>
            _trace.CaptureAttribute(context, () =>
            {
                Set(model, attribute);

                return attribute;
            }, orderInfo: _orderInfo)
        );
    }

    static void Set(ICustomAttributesModel model, Attribute attribute)
    {
        attribute.ThrowIfNotTarget(model);

        ((IMutableAttributeCollection)model.CustomAttributes).Set(attribute);
    }
}