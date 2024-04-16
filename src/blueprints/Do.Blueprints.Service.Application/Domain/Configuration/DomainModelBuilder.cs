using Do.Domain.Model;

namespace Do.Domain.Configuration;

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

        var result = new DomainModel(new(_references.Select(t => t.Model)));

        ApplyConventions(result);
        BuildIndices(result);

        return result;
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
            if (convention is IDomainModelConvention<TypeModel> typeConvention)
            {
                foreach (var type in model.Types)
                {
                    typeConvention.Apply(type);
                }
            }

            if (convention is IDomainModelConvention<PropertyModel> propertyConvention)
            {
                foreach (var property in model.Types.Where(t => t.HasMembers())
                                                    .SelectMany(t => t.GetMembers().Properties)
                )
                {
                    propertyConvention.Apply(property);
                }

            }

            if (convention is IDomainModelConvention<MethodModel> methodConvention)
            {
                foreach (var method in model.Types.Where(t => t.HasMembers())
                                                  .SelectMany(t => t.GetMembers().Methods)
                )
                {
                    methodConvention.Apply(method);
                }
            }

            if (convention is IDomainModelConvention<ParameterModel> parameterConvention)
            {
                foreach (var parameter in model.Types.Where(t => t.HasMembers())
                                                     .SelectMany(t => t.GetMembers().Methods)
                                                     .SelectMany(m => m.Overloads)
                                                     .SelectMany(o => o.Parameters)
                )
                {
                    parameterConvention.Apply(parameter);
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