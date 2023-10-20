namespace Do.Domain;

public class DomainServiceDescriptor
{
    readonly List<IDomainServiceConvention> _domainServiceConventions = new();

    internal List<IDomainServiceConvention> ServiceConventions => _domainServiceConventions;

    public DomainServiceDescriptor AddConvention<T>(Func<object, bool> appliesTo, Action<T> apply)
        where T : notnull
    {
        _domainServiceConventions.Add(
            new DomainServiceConvention<T> { AppliesTo = appliesTo, Apply = apply });

        return this;
    }

    public class DomainServiceConvention<T> : IDomainServiceConvention
        where T : notnull
    {
        public Action<T> Apply { get; init; } = default!;
        public Func<object, bool> AppliesTo { get; init; } = default!;

        bool IDomainServiceConvention.AppliesTo(object model) => AppliesTo(model);
        void IDomainServiceConvention.Apply(object model) => Apply((T)model);
    }

    public interface IDomainServiceConvention
    {
        bool AppliesTo(object model);
        void Apply(object model);
    }
}
