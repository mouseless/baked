using Do.Domain.Configuration;

namespace Do.Domain.Model;

public class TypeModelGenerics : TypeModel
{
    protected TypeModelGenerics() { }

    public TypeModelReference? GenericTypeDefinitionReference { get; private set; } = default!;
    public TypeModel? GenericTypeDefinition => GenericTypeDefinitionReference?.Model;
    public ModelCollection<TypeModelReference> GenericTypeArguments { get; private set; } = default!;

    public new class Factory : TypeModel.Factory
    {
        protected override TypeModelGenerics Create() => new();

        protected override void Fill(TypeModel result, Type type, DomainModelBuilder builder)
        {
            base.Fill(result, type, builder);

            if (result is not TypeModelGenerics generics) { return; }

            if (!type.IsGenericType)
            {
                generics.GenericTypeDefinitionReference = default;
                generics.GenericTypeArguments = [];

                return;
            }

            generics.GenericTypeDefinitionReference = !type.IsGenericTypeDefinition ? builder.GetReference(type.GetGenericTypeDefinition()) : default;
            generics.GenericTypeArguments = new(type.GenericTypeArguments.Select(builder.GetReference));
        }
    }
}
