using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class DomainModelBuilder(DomainModelBuilderOptions _options)
{
    readonly TypeModelBuildQueue _buildQueue = new();
    readonly HashSet<Type> _domainTypes = [];
    readonly ModelKeyedCollection<TypeModelReference> _references = [];

    public DomainModelBuilderOptions Options => _options;

    public DomainModel BuildFrom(IDomainTypeCollection types)
    {
        foreach (var type in types)
        {
            _domainTypes.Add(type);
        }

        _buildQueue.EnqueueAll(types);

        do
        {
            var currentReferences = _buildQueue.DequeueAll();
            foreach (var reference in currentReferences)
            {
                _references.Add(reference);
            }

            foreach (var reference in currentReferences)
            {
                reference.Apply(t => reference.Init(GetFactory(t).Create(t, this)));
            }
        }
        while (!_buildQueue.IsEmpty);

        return new(new(_references.Select(t => t.Model)));
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
}
