using static Do.Domain.DomainModel;

namespace Do.Domain;

public record DomainModel(
    Dictionary<Type, TypeModel> TypeModels
)
{
    public static DomainModel From(params Type[] args)
    {
        var config = new DomainBuilderConfiguration();
        var descriptor = new DomainDescriptor();
        args.ToList().ForEach(t => descriptor.IncludeType(t));

        return DomainModelBuilder.CreateBuilder(config, descriptor).Build();
    }

    public record TypeModel(Type Type, Type[]? Interfaces, bool IsSingleton, bool IsPersisted);
    public record PropertyModel(TypeModel Owner, Type Type);
    public record MethodModel(TypeModel Target, Type ReturnType, List<ParameterModel>? Parameters, List<Type> GenericArguements);
    public record ParameterModel(MethodModel Owner, Type Type);
}
