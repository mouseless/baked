using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class ConventionConfiguration(DomainConventionCollection _conventions) : IDomainConfiguration
{
    IModelCollectionConfigurer<TypeModel> IDomainConfiguration.Type { get; } = new ModelConventionProcessor<TypeModel>(_conventions.Type);
    IModelCollectionConfigurer<MethodModel> IDomainConfiguration.Method { get; } = new ModelConventionProcessor<MethodModel>(_conventions.Method);
    IModelCollectionConfigurer<ParameterModel> IDomainConfiguration.Parameter { get; } = new ModelConventionProcessor<ParameterModel>(_conventions.Parameter);
    IModelCollectionConfigurer<PropertyModel> IDomainConfiguration.Property { get; } = new ModelConventionProcessor<PropertyModel>(_conventions.Property);
}
