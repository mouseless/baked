namespace Do.Domain;

public class DomainModel : IDomainModel
{
    public static DomainModel From(params Type[] args) => new DomainModel().AddTypes(args);

    private readonly HashSet<Type> _types = new();

    public DomainModel AddTypes(Type[] types)
    {
        foreach (var type in types)
        {
            _types.Add(type);
        }

        return this;
    }

    public List<Type> GetTypes() => _types.ToList();
}
