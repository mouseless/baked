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

    public record TypeModel(Type Type, string Name, bool IsValueType,
        Type[]? Interfaces = default
    )
    {
        public List<MethodModel>? Constructors { get; init; }
        public List<FieldModel>? Dependencies { get; init; }
        public List<MethodModel>? Methods { get; init; }
        public List<PropertyModel>? Properties { get; init; }

        public TypeModel(Type Type, string Name, bool IsValueType,
            Type[]? Interfaces = default,
            Func<TypeModel, List<MethodModel>?>? Constructors = default,
            Func<TypeModel, List<FieldModel>?>? Dependencies = default,
            Func<TypeModel, List<MethodModel>?>? Methods = default,
            Func<TypeModel, List<PropertyModel>?>? Properties = default
        ) : this(Type, Name, IsValueType, Interfaces)
        {
            this.Constructors = Constructors?.Invoke(this);
            this.Dependencies = Dependencies?.Invoke(this);
            this.Methods = Methods?.Invoke(this);
            this.Properties = Properties?.Invoke(this);
        }

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
        public List<ParameterModel>? Parameters { get; init; } = default!;

        public MethodModel(TypeModel Target, string Name, Type ReturnType, bool IsPublic, bool IsConstructor,
            Type[]? GenericArguements = default,
            Func<MethodModel, List<ParameterModel>?>? ParametersFactory = default
        ) : this(Target, Name, ReturnType, IsPublic, IsConstructor, GenericArguements)
        {
            Parameters = ParametersFactory?.Invoke(this);
        }
    }

    public record ParameterModel(MethodModel Owner, string Name, Type Type);

    public record FieldModel(TypeModel Owner, string Name, Type Type);
}
