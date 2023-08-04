using FluentNHibernate;

namespace Do.DataAccess;

public class AutomappingConfiguration
{
    public List<Func<Type, bool>> ShouldMapType { get; } = new();
    public List<Func<Member, bool>> ShouldMapMember { get; } = new();
    public List<Func<Member, bool>> MemberIsId { get; } = new();
}
