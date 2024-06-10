using Baked.Domain.Configuration;

namespace Baked.Domain.Model;

public class TypeModelGenerics : TypeModel
{
    protected TypeModelGenerics() { }

    public TypeModelReference? ElementTypeReference { get; private set; } = default!;
    public TypeModelReference? GenericTypeDefinitionReference { get; private set; } = default!;
    public List<TypeModelReference> GenericTypeArguments { get; private set; } = default!;

    public TypeModel? ElementType => ElementTypeReference?.Model;
    public TypeModel? GenericTypeDefinition => GenericTypeDefinitionReference?.Model;

    public new class Factory : TypeModel.Factory
    {
        protected override TypeModelGenerics Create() => new();

        protected override void Fill(TypeModel result, Type type, DomainModelBuilder builder)
        {
            base.Fill(result, type, builder);

            if (result is not TypeModelGenerics generics) { return; }

            if (type.IsGenericType)
            {
                generics.GenericTypeArguments = new(type.GenericTypeArguments.Select(builder.GetReference));
                generics.GenericTypeDefinitionReference = !type.IsGenericTypeDefinition ? builder.GetReference(type.GetGenericTypeDefinition()) : default;
            }
            else
            {
                generics.GenericTypeArguments = [];
            }

            var elementType = type.GetElementType();
            if (elementType is not null)
            {
                generics.ElementTypeReference = builder.GetReference(elementType);
            }
        }
    }
}