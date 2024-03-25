using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class IndexerConfiguration(DomainIndexerCollection _collection) : IDomainConfiguration
{
    IModelCollectionConfigurer<TypeModel> IDomainConfiguration.Type { get; } = new ModelIndexerProcessor<TypeModel>(_collection);
    IModelCollectionConfigurer<MethodModel> IDomainConfiguration.Method { get; } = new ModelIndexerProcessor<MethodModel>(_collection);
    IModelCollectionConfigurer<ParameterModel> IDomainConfiguration.Parameter { get; } = new ModelIndexerProcessor<ParameterModel>(_collection);
    IModelCollectionConfigurer<PropertyModel> IDomainConfiguration.Property { get; } = new ModelIndexerProcessor<PropertyModel>(_collection);
}
