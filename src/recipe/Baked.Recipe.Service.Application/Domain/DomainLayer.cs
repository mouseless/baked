using Baked.Architecture;
using Baked.Domain.Configuration;

using static Baked.Domain.DomainLayer;

namespace Baked.Domain;

public class DomainLayer : LayerBase<AddDomainTypes>
{
    readonly IDomainTypeCollection _domainTypes = new DomainTypeCollection();
    readonly DomainModelBuilderOptions _builderOptions = new();

    protected override PhaseContext GetContext(AddDomainTypes phase) =>
        phase.CreateContextBuilder()
            .Add(_domainTypes)
            .Add(_builderOptions)
            .Build();

    protected override IEnumerable<IPhase> GetGeneratePhases()
    {
        yield return new AddDomainTypes(_domainTypes);
        yield return new BuildDomainModel(_builderOptions);
    }

    public class AddDomainTypes(IDomainTypeCollection _domainTypes)
        : PhaseBase(PhaseOrder.Earliest)
    {
        protected override void Initialize()
        {
            Context.Add(_domainTypes);
        }
    }

    public class BuildDomainModel(DomainModelBuilderOptions _builderOptions)
        : PhaseBase<IDomainTypeCollection>(PhaseOrder.Latest)
    {
        protected override void Initialize(IDomainTypeCollection domainTypes)
        {
            var builder = new DomainModelBuilder(_builderOptions);
            var model = builder.Build(domainTypes);

            Context.Add(model);
            builder.PostBuild(model);
        }
    }
}