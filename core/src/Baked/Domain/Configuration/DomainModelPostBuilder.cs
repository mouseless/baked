using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class DomainModelPostBuilder(DomainModelBuilderOptions options, IDomainModelConventionCollection conventions, DomainModel model)
{
    readonly DomainModelBuilderOptions _options = options;
    readonly IDomainModelConventionCollection _conventions = conventions;

    public DomainModel Model { get; } = model;

    public void EndBuild()
    {
        using (Diagnostics.Start(nameof(DomainModelPostBuilder), onDispose: _options.OnComplete))
        {
            var contexts = new DomainModelConventionContexts(Model);
            var conventionsBeforeBuildingIndexes = new List<IDomainModelConvention>();
            var conventionsAfterBuildingIndexes = new List<IDomainModelConvention>();

            foreach (var convention in _conventions.OrderBy(c => c.Order).Select(c => c.Convention))
            {
                if (convention.BeforeBuildingIndexes)
                {
                    conventionsBeforeBuildingIndexes.Add(convention);
                }
                else
                {
                    conventionsAfterBuildingIndexes.Add(convention);
                }
            }

            foreach (var convention in conventionsBeforeBuildingIndexes)
            {
                contexts.Apply(convention);
            }

            BuildIndexes();

            foreach (var convention in conventionsAfterBuildingIndexes)
            {
                contexts.Apply(convention);
            }
        }
    }

    void BuildIndexes()
    {
        foreach (var index in _options.Index.Type)
        {
            Model.Types.AddIndex(index);
        }

        foreach (var index in _options.Index.Property)
        {
            foreach (var properties in Model.Types
                .Where(t => t.HasMembers())
                .Select(m => m.GetMembers().Properties)
            )
            {
                properties.AddIndex(index);
            }
        }

        foreach (var index in _options.Index.Method)
        {
            foreach (var methods in Model.Types
                .Where(t => t.HasMembers())
                .Select(m => m.GetMembers().Methods)
            )
            {
                methods.AddIndex(index);
            }
        }

        foreach (var index in _options.Index.Parameter)
        {
            foreach (var overload in Model.Types
                .Where(t => t.HasMembers())
                .SelectMany(t => t.GetMembers().Methods)
                .SelectMany(m => m.Overloads)
            )
            {
                overload.Parameters.AddIndex(index);
            }
        }
    }
}