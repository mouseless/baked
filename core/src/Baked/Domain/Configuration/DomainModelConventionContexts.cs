using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class DomainModelConventionContexts
{
    public record Context<TModel>(IEnumerable<TModel> Models, DomainModelBuilderDiagnostics Diagnostics)
    {
        public void Apply(IDomainModelConvention<TModel> convention)
        {
            foreach (var model in Models)
            {
                try
                {
                    convention.Apply(model);
                }
                catch (Exception ex)
                {
                    Diagnostics.Errors.Add(ex);
                }
            }
        }
    }

    readonly Context<TypeModelContext> _basics;
    readonly Context<TypeModelGenericsContext> _generics;
    readonly Context<TypeModelInheritanceContext> _inheritance;
    readonly Context<TypeModelMetadataContext> _metadata;
    readonly Context<TypeModelMembersContext> _members;
    readonly Context<PropertyModelContext> _property;
    readonly Context<MethodModelContext> _method;
    readonly Context<ParameterModelContext> _parameter;

    public DomainModelBuilderDiagnostics Diagnostics { get; } = new();

    public DomainModelConventionContexts(DomainModel result)
    {
        _basics = new(
            result.Types
                .Select(t => new TypeModelContext { Domain = result, Type = t }),
            Diagnostics
        );
        _generics = new(
            result.Types
                .OfType<TypeModelGenerics>()
                .Select(t => new TypeModelGenericsContext { Domain = result, Type = t }),
            Diagnostics
        );
        _inheritance = new(
            result.Types
                .OfType<TypeModelInheritance>()
                .Select(t => new TypeModelInheritanceContext { Domain = result, Type = t }),
            Diagnostics
        );
        _metadata = new(
            result.Types
                .OfType<TypeModelMetadata>()
                .Select(t => new TypeModelMetadataContext { Domain = result, Type = t }),
            Diagnostics
        );
        _members = new(
            result.Types
                .OfType<TypeModelMembers>()
                .Select(t => new TypeModelMembersContext { Domain = result, Type = t }),
            Diagnostics
        );
        _property = new(
            result.Types
                .OfType<TypeModelMembers>()
                .SelectMany(t => t.Properties.Select(p => new PropertyModelContext { Domain = result, Type = t, Property = p })),
            Diagnostics
        );
        _method = new(
            result.Types
                .OfType<TypeModelMembers>()
                .SelectMany(t => t.Methods.Select(m => new MethodModelContext { Domain = result, Type = t, Method = m })),
            Diagnostics
        );
        _parameter = new(
            result.Types
                .OfType<TypeModelMembers>()
                .SelectMany(t => t.Methods.Select(m => (t, m)))
                .SelectMany(tm => tm.m.Overloads.Select(o => (tm.t, tm.m, o)))
                .SelectMany(tmo => tmo.o.Parameters.Select(p => new ParameterModelContext { Domain = result, Type = tmo.t, Method = tmo.m, MethodOverload = tmo.o, Parameter = p })),
            Diagnostics
        );
    }

    public void Apply(IDomainModelConvention convention)
    {
        if (convention is IDomainModelConvention<TypeModelContext> basics)
        {
            _basics.Apply(basics);
        }

        if (convention is IDomainModelConvention<TypeModelGenericsContext> generics)
        {
            _generics.Apply(generics);
        }

        if (convention is IDomainModelConvention<TypeModelInheritanceContext> inheritance)
        {
            _inheritance.Apply(inheritance);
        }

        if (convention is IDomainModelConvention<TypeModelMetadataContext> metadata)
        {
            _metadata.Apply(metadata);
        }

        if (convention is IDomainModelConvention<TypeModelMembersContext> members)
        {
            _members.Apply(members);
        }

        if (convention is IDomainModelConvention<PropertyModelContext> property)
        {
            _property.Apply(property);
        }

        if (convention is IDomainModelConvention<MethodModelContext> method)
        {
            _method.Apply(method);
        }

        if (convention is IDomainModelConvention<ParameterModelContext> parameter)
        {
            _parameter.Apply(parameter);
        }
    }
}