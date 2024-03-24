using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class ModelConventionProcessors(
    ModelConventionProcessor<TypeModel> _type,
    ModelConventionProcessor<MethodModel> _method,
    ModelConventionProcessor<ParameterModel> _parameter,
    ModelConventionProcessor<PropertyModel> _property
) : IDomainService
{
    static IDomainService IDomainService.New(DomainServiceProvider sp) =>
        new ModelConventionProcessors(
            sp.Get<ModelConventionProcessor<TypeModel>>().Initialize(),
            sp.Get<ModelConventionProcessor<MethodModel>>().Initialize(),
            sp.Get<ModelConventionProcessor<ParameterModel>>().Initialize(),
            sp.Get<ModelConventionProcessor<PropertyModel>>().Initialize()
        );

    internal ModelConventionProcessor<TypeModel> Type { get; } = _type;
    internal ModelConventionProcessor<MethodModel> Method { get; } = _method;
    internal ModelConventionProcessor<ParameterModel> Parameter { get; } = _parameter;
    internal ModelConventionProcessor<PropertyModel> Property { get; } = _property;
}
