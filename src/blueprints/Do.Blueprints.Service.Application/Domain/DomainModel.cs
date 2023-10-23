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

    public record TypeModel(Type Type, string Name,
        Type[]? Interfaces = default
    )
    {
        public List<MethodModel>? Constructors { get; internal set; }
        public List<FieldModel>? Dependencies { get; internal set; }
        public List<MethodModel>? Methods { get; internal set; }
        public List<PropertyModel>? Properties { get; internal set; }

        public bool HasMethod(Func<MethodModel, bool> predicate) =>
            Methods is not null && Methods.Any(predicate);

        public bool HasProperty(Func<PropertyModel, bool> predicate) =>
            Properties is not null && Properties.Any(predicate);
    }

    public record PropertyModel(TypeModel Owner, string Name, Type Type);

    public record MethodModel(TypeModel Target, string Name, Type ReturnType, bool IsPublic, bool IsConstructor,
        Type[]? GenericArguements = default
    )
    {
        public List<ParameterModel>? Parameters { get; internal set; }
    }

    public record ParameterModel(MethodModel Owner, string Name, Type Type);

    public record FieldModel(TypeModel Owner, string Name, Type Type);
}
