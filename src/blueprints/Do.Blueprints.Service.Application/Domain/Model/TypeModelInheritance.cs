using Do.Domain.Configuration;

namespace Do.Domain.Model;

public class TypeModelInheritance : TypeModelGenerics
{
    protected TypeModelInheritance() { }

    public TypeModelReference? BaseTypeReference { get; private set; } = default!;
    public TypeModel? BaseType => BaseTypeReference?.Model;
    public ModelCollection<TypeModelReference> Interfaces { get; private set; } = default!;

    public new class Factory : TypeModelGenerics.Factory
    {
        protected override TypeModelInheritance Create() =>
            new();

        protected override void Fill(TypeModel result, Type type, DomainModelBuilder builder)
        {
            base.Fill(result, type, builder);

            if (result is not TypeModelInheritance inheritance) { return; }

            inheritance.BaseTypeReference = type.BaseType is null ? default : builder.GetReference(type.BaseType);
            inheritance.Interfaces = new(type.GetInterfaces().SelectMany(BuildInterfaces));

            IEnumerable<TypeModelReference> BuildInterfaces(Type @interface)
            {
                yield return builder.GetReference(@interface);

                if (@interface.IsGenericType)
                {
                    yield return builder.GetReference(@interface.GetGenericTypeDefinition());
                }
            }
        }
    }
}
