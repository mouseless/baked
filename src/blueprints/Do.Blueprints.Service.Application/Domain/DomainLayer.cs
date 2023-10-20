using Do.Architecture;

using static Do.Configuration.ConfigurationLayer;
using static Do.DependencyInjection.DependencyInjectionLayer;

namespace Do.Domain;

public class DomainLayer : LayerBase<BuildConfiguration, AddServices>
{
    readonly DomainDescriptor _domainDescriptor = new();
    readonly DomainServiceDescriptor _domainServiceDescriptor = new();

    protected override PhaseContext GetContext(BuildConfiguration phase) =>
        phase
            .CreateContextBuilder()
            .Add(_domainDescriptor)
            .OnDispose(() =>
            {
                Context.Add(DomainModelBuilder.CreateBuilder(_domainDescriptor).Build());
                Context.Add(_domainDescriptor);
                Context.Add(_domainServiceDescriptor);
            })
            .Build()
        ;

    protected override PhaseContext GetContext(AddServices phase) =>
        phase
           .CreateContextBuilder()
           .Add(_domainServiceDescriptor)
           .OnDispose(() =>
           {
               ApplyConventions();
           })
           .Build()
       ;

    void ApplyConventions()
    {
        var _domainServiceDescriptor = Context.Get<DomainServiceDescriptor>();
        var domainModel = Context.Get<DomainModel>();

        foreach (var typeModel in domainModel.TypeModels.Values)
        {
            foreach (var convention in _domainServiceDescriptor.ServiceConventions)
            {
                if (convention.AppliesTo(typeModel))
                {
                    convention.Apply(typeModel);
                }
            }
        }
    }
}
