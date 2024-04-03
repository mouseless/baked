using Do.Domain.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Do.Domain.Model;

public class TypeModelMetadata : TypeModelInheritance, ICustomAttributesModel
{
    public AttributeCollection CustomAttributes { get; private set; } = default!;

    public bool Has<T>() where T : Attribute =>
        CustomAttributes.Contains<T>();

    public T GetSingle<T>() where T : Attribute =>
        Get<T>().Single();

    public IEnumerable<T> Get<T>() where T : Attribute =>
        CustomAttributes.Get<T>();

    public bool TryGetSingle<T>([NotNullWhen(true)] out T? result) where T : Attribute
    {
        if (!TryGet<T>(out var attributes))
        {
            result = null;

            return false;
        }

        result = attributes.SingleOrDefault();

        return result is not null;
    }

    public bool TryGet<T>([NotNullWhen(true)] out IEnumerable<T>? result) where T : Attribute =>
        CustomAttributes.TryGet<T>(out result);

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
