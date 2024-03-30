using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class IndexerConfiguration(DomainIndexOptions _options) : IDomainConfiguration
{
    IModelCollectionConfigurer<TypeModel> IDomainConfiguration.Type { get; } = new ModelCollectionIndexer<TypeModel>(_options.Type);
    IModelCollectionConfigurer<MethodGroupModel> IDomainConfiguration.MethodGroup { get; } = new ModelCollectionIndexer<MethodGroupModel>(_options.MethodGroup);
    IModelCollectionConfigurer<ParameterModel> IDomainConfiguration.Parameter { get; } = new ModelCollectionIndexer<ParameterModel>(_options.Parameters);
    IModelCollectionConfigurer<PropertyModel> IDomainConfiguration.Property { get; } = new ModelCollectionIndexer<PropertyModel>(_options.Property);
}
