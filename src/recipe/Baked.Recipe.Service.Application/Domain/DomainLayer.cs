using Baked.Architecture;
using Baked.Domain.Configuration;

using static Baked.CodeGeneration.CodeGenerationLayer;
using static Baked.Domain.DomainLayer;

namespace Baked.Domain;

public class DomainLayer : LayerBase<AddDomainTypes, GenerateCode>
{
    readonly IDomainTypeCollection _domainTypes = new DomainTypeCollection();
    readonly DomainModelBuilderOptions _builderOptions = new();
    readonly DomainServicesModel _domainServicesModel = new();

    protected override PhaseContext GetContext(AddDomainTypes phase) =>
        phase.CreateContextBuilder()
            .Add(_domainTypes)
            .Add(_builderOptions)
            .Build();

    protected override IEnumerable<IPhase> GetBakePhases()
    {
        yield return new AddDomainTypes(_domainTypes);
        yield return new BuildDomainModel(_builderOptions);
    }

    protected override PhaseContext GetContext(GenerateCode phase)
    {
        var generatedAssemblies = Context.GetGeneratedAssemblyCollection();
        _domainServicesModel.References.Add<DomainLayer>();
        _domainServicesModel.Usings.AddRange(
        [
            "Baked",
            "System",
            "System.Linq",
            "System.Collections",
            "System.Collections.Generic",
            "System.Threading.Tasks"
        ]);

        return phase.CreateContextBuilder()
            .Add(_domainServicesModel)
            .OnDispose(() =>
            {
                generatedAssemblies.Add(nameof(DomainLayer),
                    assembly => assembly
                        .AddReferences(_domainServicesModel.References)
                        .AddCodes(new ServiceAdderTemplate(_domainServicesModel.Services)),
                    compilerOptions => compilerOptions.WithUsings(_domainServicesModel.Usings)
                );
            })
            .Build();
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