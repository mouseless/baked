using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class DomainModelBuilder(DomainModelBuilderOptions _options)
{
    readonly TypeModelBuildQueue _buildQueue = new();
    readonly HashSet<Type> _domainTypes = [];
    readonly ModelCollection<TypeModelReference>.KeyedCollection _references = [];

    public DomainModelBuilderOptions Options => _options;

    public DomainModel Build(IDomainTypeCollection types)
    {
        foreach (var type in types)
        {
            _domainTypes.Add(type);
        }

        _buildQueue.EnqueueAll(types);

        do
        {
            var references = _buildQueue.DequeueAll();
            foreach (var reference in references)
            {
                _references.Add(reference);
            }

            foreach (var reference in references)
            {
                reference.Apply(t => reference.Init(GetFactory(t).Create(t, this)));
            }
        }
        while (!_buildQueue.IsEmpty);

        return new(new(_references.Select(t => t.Model)));

    }

    public void PostBuild(DomainModel result)
    {
        ApplyConventions(result);
        BuildIndices(result);
    }

    TypeModel.Factory GetFactory(Type t)
    {
        var context = new TypeModelBuildContext(t, this);

        return _options.BuildLevels.FirstOrDefault(blf => blf.Filter(context))?.BuildLevel ?? BuildLevels.Basics;
    }

    internal bool DomainTypesContain(Type t) =>
        _domainTypes.Contains(t);

    internal TypeModelReference GetReference(Type type)
    {
        if (_references.TryGetValue(TypeModelReference.IdFrom(type), out var result))
        {
            return result;
        }

        return _buildQueue.Enqueue(type);
    }

    void ApplyConventions(DomainModel model)
    {
        foreach (var convention in _options.Conventions.OrderBy(c => c.Order))
        {
            if (convention is IDomainModelConvention<TypeModelContext> typeConvention)
            {
                foreach (var type in model.Types)
                {
                    typeConvention.Apply(new() { Domain = model, Type = type });
                }
            }

            if (convention is IDomainModelConvention<TypeModelGenericsContext> typeGenericsConvention)
            {
                foreach (var type in model.Types.OfType<TypeModelGenerics>())
                {
                    typeGenericsConvention.Apply(new() { Domain = model, Type = type });
                }
            }

            if (convention is IDomainModelConvention<TypeModelInheritanceContext> typeInheritanceConvention)
            {
                foreach (var type in model.Types.OfType<TypeModelInheritance>())
                {
                    typeInheritanceConvention.Apply(new() { Domain = model, Type = type });
                }
            }

            if (convention is IDomainModelConvention<TypeModelMetadataContext> typeMetadataConvention)
            {
                foreach (var type in model.Types.OfType<TypeModelMetadata>())
                {
                    typeMetadataConvention.Apply(new() { Domain = model, Type = type });
                }
            }

            if (convention is IDomainModelConvention<TypeModelMembersContext> typeMembersConvention)
            {
                foreach (var type in model.Types.OfType<TypeModelMembers>())
                {
                    typeMembersConvention.Apply(new() { Domain = model, Type = type });
                }
            }

            if (convention is IDomainModelConvention<PropertyModelContext> propertyConvention)
            {
                foreach (var (type, property) in model.Types.OfType<TypeModelMembers>()
                                                            .SelectMany(t => t.Properties.Select(p => (t, p)))
                )
                {
                    propertyConvention.Apply(new() { Domain = model, Type = type.GetMembers(), Property = property });
                }

            }

            if (convention is IDomainModelConvention<MethodModelContext> methodConvention)
            {
                foreach (var (type, method) in model.Types.OfType<TypeModelMembers>()
                                                          .SelectMany(t => t.Methods.Select(m => (t, m)))
                )
                {
                    methodConvention.Apply(new() { Domain = model, Type = type, Method = method });
                }
            }

            if (convention is IDomainModelConvention<ParameterModelContext> parameterConvention)
            {
                foreach (var (type, method, overload, parameter) in model.Types.OfType<TypeModelMembers>()
                                                                               .SelectMany(t => t.Methods.Select(m => (t, m)))
                                                                               .SelectMany(x => x.m.Overloads.Select(o => (x.t, x.m, o)))
                                                                               .SelectMany(x => x.o.Parameters.Select(p => (x.t, x.m, x.o, p)))
                )
                {
                    parameterConvention.Apply(new() { Domain = model, Type = type, Method = method, MethodOverload = overload, Parameter = parameter });
                }
            }
        }
    }

    void BuildIndices(DomainModel model)
    {
        foreach (var index in _options.Index.Type)
        {
            model.Types.AddIndex(index);
        }

        foreach (var index in _options.Index.Property)
        {
            foreach (var properties in model.Types.Where(t => t.HasMembers())
                                                  .Select(m => m.GetMembers().Properties)
            )
            {
                properties.AddIndex(index);
            }
        }

        foreach (var index in _options.Index.Method)
        {
            foreach (var methods in model.Types.Where(t => t.HasMembers())
                                               .Select(m => m.GetMembers().Methods)
            )
            {
                methods.AddIndex(index);
            }
        }

        foreach (var index in _options.Index.Parameter)
        {
            foreach (var overload in model.Types.Where(t => t.HasMembers())
                                                .SelectMany(t => t.GetMembers().Methods)
                                                .SelectMany(m => m.Overloads)
            )
            {
                overload.Parameters.AddIndex(index);
            }
        }
    }
}