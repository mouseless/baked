using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class ModelConventionProcessor<T>(DomainServiceProvider _sp, ModelConventionCollection<T> _conventions) : IDomainService
    where T : IModel
{
    static IDomainService IDomainService.New(DomainServiceProvider sp) =>
        new ModelConventionProcessor<T>(sp, sp.Get<ModelConventionCollection<T>>());

    internal ModelConventionProcessor<T> Initialize()
    {
        foreach (var item in _conventions)
        {
            item.Initialize(_sp);
        }

        _conventions.Sort((l, r) => l.Order - r.Order);

        return this;
    }

    internal void Apply(ModelCollection<T> collection)
    {
        foreach (var convention in _conventions)
        {
            foreach (var model in collection)
            {
                if (convention.AppliesTo(model))
                {
                    convention.Apply(model);
                }
            }
        }
    }
}