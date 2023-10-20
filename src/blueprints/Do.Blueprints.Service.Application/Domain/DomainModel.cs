using static Do.Domain.DomainModel;

namespace Do.Domain;

public record DomainModel(
    Dictionary<Type, TypeModel> TypeModels
)
{
    public static DomainModel From(params Type[] args)
    {
        var descriptor = new DomainDescriptor();
        args.ToList().ForEach(t => descriptor.AddType(t));

        return DomainModelBuilder.CreateBuilder(descriptor).Build();
    }

    public record TypeModel(Type Type, Type[]? Interfaces);
    public record PropertyModel(TypeModel Owner, Type Type);
    public record MethodModel(TypeModel Target, Type ReturnType, List<ParameterModel>? Parameters, List<Type>? GenericArguements);
    public record ParameterModel(MethodModel Owner, Type Type);
}
