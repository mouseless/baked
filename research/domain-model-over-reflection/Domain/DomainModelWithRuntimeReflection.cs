using DomainModelOverReflection.Models.Domain;
using System.Reflection;

namespace Domain;

public class DomainModelWithRuntimeReflection : IDomainModel
{
    public static DomainModelWithRuntimeReflection Build(Assembly assembly) =>
        new(assembly.GetTypes().Where(t => t.Namespace is not null && t.Namespace.EndsWith("Business")).Select(t => new TypeModel(t)).ToArray());

    readonly TypeModel[] _typeModels;

    public DomainModelWithRuntimeReflection(TypeModel[] typeModels)
    {
        _typeModels = typeModels;
    }

    public TypeModel[] TypeModels => _typeModels;
}