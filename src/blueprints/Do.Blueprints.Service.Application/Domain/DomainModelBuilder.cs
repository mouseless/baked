using static Do.Domain.DomainModel;

namespace Do.Domain;

public class DomainModelBuilder
{
    public static DomainModelBuilder CreateBuilder(DomainBuilderConfiguration configuration, DomainDescriptor descriptor) =>
        new(configuration, descriptor);

    readonly DomainBuilderConfiguration _configuration;
    readonly DomainDescriptor _descriptor;

    List<Type> _types = new();

    DomainModelBuilder(DomainBuilderConfiguration configuration, DomainDescriptor descriptor)
    {
        _configuration = configuration;
        _descriptor = descriptor;
    }

    public DomainModel Build()
    {
        _descriptor.IncludedTypes.ForEach(t => _types.Add(t));

        return new DomainModel(
            TypeModels: GenerateTypeModelList()
        );
    }

    Dictionary<Type, TypeModel> GenerateTypeModelList()
    {
        var result = new Dictionary<Type, TypeModel>();

        foreach (var type in _types)
        {
            result.Add(type, new(type, type.GetInterfaces(), _configuration.SingletonConvention(type), _configuration.PersistedTypeConvention(type)));
        }

        return result;
    }
}
