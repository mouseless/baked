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

        BuildMetadata(result);
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

    void BuildMetadata(DomainModel model)
    {
        foreach (var convention in _options.Metadata.Type)
        {
            foreach (var type in model.Types)
            {
                convention.Apply(type);
            }
        }

        foreach (var convention in _options.Metadata.Property)
        {
            foreach (var property in model.Types.Where(t => t.HasMembers())
                                                .SelectMany(t => t.GetMembers().Properties)
            )
            {
                convention.Apply(property);
            }
        }

        foreach (var convention in _options.Metadata.Method)
        {
            foreach (var method in model.Types.Where(t => t.HasMembers())
                                              .SelectMany(t => t.GetMembers().Methods)
            )
            {
                convention.Apply(method);
            }
        }

        foreach (var convention in _options.Metadata.Parameter)
        {
            foreach (var parameter in model.Types.Where(t => t.HasMembers())
                                                 .SelectMany(t => t.GetMembers().Methods)
                                                 .SelectMany(m => m.Overloads)
                                                 .SelectMany(o => o.Parameters)
            )
            {
                convention.Apply(parameter);
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
