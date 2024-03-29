using Do.Domain.Configuration;
using System.Reflection;

namespace Do.Domain.Model;

public class TypeModelMetadata : TypeModelInheritance, IMemberModel
{
    public AttributeCollection CustomAttributes { get; private set; } = default!;

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.ContainsKey<T>();

    AttributeCollection IMemberModel.CustomAttributes => CustomAttributes;

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
