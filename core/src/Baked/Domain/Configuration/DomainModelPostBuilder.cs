using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class DomainModelPostBuilder(DomainModelBuilderOptions options, DomainModel model)
{
    readonly DomainModelBuilderOptions _options = options;

    public DomainModel Model { get; } = model;

    public void EndBuild()
    {
        using (Diagnostics.Start(nameof(DomainModelPostBuilder), onDispose: _options.OnComplete))
        {
            var contexts = new DomainModelConventionContexts(Model);
            var conventionsRequiringIndex = new List<IDomainModelConvention>();
            var restOfTheConventions = new List<IDomainModelConvention>();

            foreach (var convention in _options.Conventions.OrderBy(c => c.Order).Select(c => c.Convention))
            {
                if (convention is IAddRemoveAttributeConvention addRemove && addRemove.AttributeRequiresIndex)
                {
                    conventionsRequiringIndex.Add(convention);
                }
                else
                {
                    restOfTheConventions.Add(convention);
                }
            }

            foreach (var convention in conventionsRequiringIndex)
            {
                contexts.Apply(convention);
            }

            BuildIndices();

            foreach (var convention in restOfTheConventions)
            {
                contexts.Apply(convention);
            }
        }
    }

    void BuildIndices()
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