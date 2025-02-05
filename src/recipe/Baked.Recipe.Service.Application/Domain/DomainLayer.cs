using Baked.Architecture;
using Baked.Domain.Configuration;

using static Baked.CodeGeneration.CodeGenerationLayer;
using static Baked.Domain.DomainLayer;
using static Baked.Runtime.RuntimeLayer;

namespace Baked.Domain;

public class DomainLayer : LayerBase<AddDomainTypes, GenerateCode, AddServices>
{
    readonly IDomainTypeCollection _domainTypes = new DomainTypeCollection();
    readonly DomainModelBuilderOptions _builderOptions = new();
    readonly DomainServiceCollection _domainServiceCollection = new();

    protected override PhaseContext GetContext(AddDomainTypes phase) =>
        phase.CreateContextBuilder()
            .Add(_domainTypes)
            .Add(_builderOptions)
            .Build();

    protected override PhaseContext GetContext(GenerateCode phase)
    {
        var generatedAssemblies = Context.GetGeneratedAssemblyCollection();
        _domainServiceCollection.References.Add<DomainLayer>();
        _domainServiceCollection.Usings.AddRange(
        [
            "Baked",
            "System",
            "System.Linq",
            "System.Collections",
            "System.Collections.Generic",
            "System.Threading.Tasks"
        ]);

        var domain = Context.GetDomainModel();

        return phase.CreateContextBuilder()
            .Add(_domainServiceCollection, domain)
            .OnDispose(() =>
            {
                generatedAssemblies.Add(nameof(DomainLayer),
                    assembly => assembly
                        .AddReferences(_domainServiceCollection.References)
                        .AddCodes(new ServiceAdderTemplate(_domainServiceCollection)),
                    compilerOptions => compilerOptions.WithUsings(_domainServiceCollection.Usings)
                );
            })
            .Build();
    }

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.GetServiceCollection();
        services.AddFromAssembly(Context.GetGeneratedAssembly(nameof(DomainLayer)));

        return phase.CreateEmptyContext();
    }

    protected override IEnumerable<IPhase> GetBakePhases()
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