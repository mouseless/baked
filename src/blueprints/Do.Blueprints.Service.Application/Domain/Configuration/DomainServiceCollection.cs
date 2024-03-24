using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class DomainServiceCollection
{
    record DomainServiceDescriptor(Type Type, Func<DomainServiceProvider, object> Factory);

    readonly List<DomainServiceDescriptor> _descriptors = [];

    internal DomainServiceCollection AddConventions(DomainConventions domainConventions)
    {
        AddService<ModelConventionProcessor<TypeModel>>(sp => new ModelConventionProcessor<TypeModel>(sp, domainConventions.Type));
        AddService<ModelConventionProcessor<MethodModel>>(sp => new ModelConventionProcessor<MethodModel>(sp, domainConventions.Method));
        AddService<ModelConventionProcessor<PropertyModel>>(sp => new ModelConventionProcessor<PropertyModel>(sp, domainConventions.Property));
        AddService<ModelConventionProcessor<ParameterModel>>(sp => new ModelConventionProcessor<ParameterModel>(sp, domainConventions.Parameter));

        return this;
    }

    internal DomainServiceCollection AddIndexers(ModelIndexerCollection domainIndexers)
    {
        _descriptors.Add(new(typeof(ModelIndexerCollection), _ => domainIndexers));

        return this;
    }

    internal DomainServiceCollection AddOptions(DomainBuilderOptions domainBuilderOptions)
    {
        _descriptors.Add(new(typeof(DomainBuilderOptions), _ => domainBuilderOptions));

        return this;
    }

    internal DomainServiceCollection AddService<T>()
        where T : IDomainService
    {
        AddService<T>(T.New);

        return this;
    }

    internal DomainServiceCollection AddService<T>(Func<DomainServiceProvider, object> factory)
        where T : IDomainService
    {
        _descriptors.Add(new(typeof(T), factory));

        return this;
    }

    internal DomainServiceCollection ForwardService<T1, T2>()
        where T1 : notnull
        where T2 : class, T1, IDomainService
    {
        _descriptors.Add(new(typeof(T1), sp => sp.Get<T2>()));

        return this;
    }

    internal DomainModelBuilder Build()
    {
        AddService<DomainModelBuilder>();
        AddService<ModelConfigurer>();
        AddService<ModelIndexer>();
        AddService<ModelIndexerProcessor>();
        ForwardService<ITypeModelFactory, DomainModelBuilder>();
        AddService<AttributeAdder>();
        AddService<ModelConventionProcessors>();

        var provider = new DomainServiceProvider();
        foreach (var item in _descriptors)
        {
            provider.Add(item.Type, item.Factory);
        }

        return provider.Get<DomainModelBuilder>();
    }
}

