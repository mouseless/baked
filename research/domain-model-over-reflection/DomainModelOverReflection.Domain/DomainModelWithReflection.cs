using DomainModelOverReflection.Models.Domain;
using System.Reflection;

namespace DomainModelOverReflection.Domain;

public class DomainModelWithReflection : IDomainModel
{
    public static TypeModel[] Build(Assembly assembly) =>
        assembly.GetTypes().Where(t => t.Namespace is not null && t.Namespace.EndsWith("Business")).Select(t => new TypeModel(t)).ToArray();

    static readonly TypeModel[] _typeModels = Build(typeof(DomainModelWithReflection).Assembly);

    public TypeModel[] TypeModels => _typeModels;
}
