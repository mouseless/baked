using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class ModelConfigurer(ModelConventionProcessors _processors) : IDomainService
{
    static IDomainService IDomainService.New(DomainServiceProvider sp) =>
        new ModelConfigurer(sp.Get<ModelConventionProcessors>());

    internal void Execute(DomainModel model)
    {
        _processors.Type.Apply(model.Types);

        foreach (var methods in model.Types.Select(t => t.Methods))
        {
            _processors.Method.Apply(methods);

            foreach (var overloads in methods.Select(m => m.Overloads))
            {
                foreach (var overload in overloads)
                {
                    _processors.Parameter.Apply(overload.Parameters);
                }
            }
        }

        foreach (var properties in model.Types.Select(t => t.Properties))
        {
            _processors.Property.Apply(properties);
        }
    }
}