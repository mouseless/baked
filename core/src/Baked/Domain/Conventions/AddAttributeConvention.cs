using Baked.Domain.Configuration;
using Baked.Domain.Inspection;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class AddAttributeConvention<TModelContext>(
    Action<TModelContext, Action<ICustomAttributesModel, Attribute>> _apply,
    Func<TModelContext, bool> _when,
    bool beforeBuildingIndexes = true
) : IDomainModelConvention<TModelContext>
    where TModelContext : DomainModelContext
{
    readonly Trace _trace = Trace.Here();

    bool IDomainModelConvention.BeforeBuildingIndexes => beforeBuildingIndexes;

    public void Apply(TModelContext context)
    {
        context.Trace = _trace;

        if (!_when(context)) { return; }

        _apply(context, (model, attribute) =>
            _trace.CaptureAttribute(context, () =>
            {
                Add(model, attribute);

                return attribute;
            })
        );
    }

    static void Add(ICustomAttributesModel model, Attribute attribute)
    {
        attribute.ThrowIfNotTarget(model);

        ((IMutableAttributeCollection)model.CustomAttributes).Add(attribute);
    }
}