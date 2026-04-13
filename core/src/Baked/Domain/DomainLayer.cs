using Baked.Architecture;
using Baked.CodeGeneration;
using Baked.Domain.Configuration;
using Baked.Domain.Export;

using static Baked.CodeGeneration.CodeGenerationLayer;
using static Baked.Domain.DomainLayer;
using static Baked.Runtime.RuntimeLayer;

namespace Baked.Domain;

public class DomainLayer : LayerBase<AddDomainTypes, GenerateCode, AddServices>
{
    readonly IDomainTypeCollection _domainTypes = new DomainTypeCollection();
    readonly DomainModelBuilderOptions _builderOptions = new();
    readonly DomainServiceCollection _domainServiceCollection = new();
    readonly AttributeDataBuilderCollection _attributeDataBuilderCollection = new();
    readonly AttributeExportCollection _attributeExportCollection = new();

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
            .Add(_attributeDataBuilderCollection)
            .Add(_attributeExportCollection)
            .OnDispose(() =>
            {
                generatedAssemblies.Add(nameof(DomainLayer),
                    assembly => assembly.AddCodes(new ServiceAdderTemplate(_domainServiceCollection)),
                    compilerOptions => compilerOptions.WithUsings(_domainServiceCollection.Usings)
                );

                GenerateMetadataFiles();
            })
            .Build();
    }

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.GetServiceCollection();
        services.AddFromAssembly(Context.GetGeneratedAssembly(nameof(DomainLayer)));

        return phase.CreateEmptyContext();
    }

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

    void GenerateMetadataFiles()
    {
        var domain = Context.GetDomainModel();
        var files = Context.Get<IGeneratedFileCollection>();

        foreach (var (key, set) in _attributeExportCollection)
        {
            set.Builders = _attributeDataBuilderCollection;
            var model = new AttributeExportSetBuilder(set).Build(domain);
            var contentGenerator = new AttributeExportFileContentGenerator(set.Serializer, set.ContentGroupName);
            var contents = contentGenerator.Generate(model);
            foreach (var (fileName, content) in contents)
            {
                files.Add($"{fileName}", content, extension: "kdl", outdir: Path.Join("Export", $"{key}"));
            }
        }
    }
}