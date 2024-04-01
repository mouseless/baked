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

    public TypeModelReference Get(Type type)
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
            foreach (var properties in model.Types.Where(t => t.HasMembers()).Select(t => t.GetMembers().Properties))
            {
                foreach (var property in properties)
                {
                    convention.Apply(property);
                }
            }
        }

        foreach (var convention in _options.Metadata.Method)
        {
            foreach (var methodGroups in model.Types.Where(t => t.HasMembers()).Select(t => t.GetMembers().Methods))
            {
                foreach (var methodGroup in methodGroups)
                {
                    convention.Apply(methodGroup);
                }
            }
        }

        foreach (var convention in _options.Metadata.Parameter)
        {
            foreach (var methods in model.Types.Where(t => t.HasMembers()).Select(t => t.GetMembers().Methods))
            {
                foreach (var method in methods)
                {
                    foreach (var parameter in method.Parameters)
                    {
                        convention.Apply(parameter);
                    }
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
            foreach (var properties in model.Types.Where(m => m.HasMembers()).Select(m => m.GetMembers().Properties))
            {
                properties.AddIndex(index);
            }
        }

        foreach (var index in _options.Index.Method)
        {
            foreach (var methods in model.Types.Where(m => m.HasMembers()).Select(m => m.GetMembers().Methods))
            {
                methods.AddIndex(index);
            }
        }

        foreach (var index in _options.Index.Parameter)
        {
            foreach (var methods in model.Types.Where(m => m.HasMembers()).Select(m => m.GetMembers().Methods))
            {
                foreach (var method in methods)
                {
                    method.Parameters.AddIndex(index);
                }
            }
        }
    }
}
