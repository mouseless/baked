using Baked.Domain.Configuration;
using System.Reflection;

namespace Baked.Domain.Model;

public class TypeModelMetadata : TypeModelInheritance, ICustomAttributesModel
{
    public AttributeCollection CustomAttributes { get; private set; } = default!;

    AttributeCollection ICustomAttributesModel.CustomAttributes => CustomAttributes;

    public new class Factory : TypeModelInheritance.Factory
    {
        protected override TypeModelMetadata Create() => new();

        protected override void Fill(TypeModel result, Type type, DomainModelBuilder builder)
        {
            base.Fill(result, type, builder);

            if (result is not TypeModelMetadata metadata) { return; }

            metadata.CustomAttributes = new(type.GetCustomAttributes());
        }
    }
}