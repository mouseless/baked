using FluentNHibernate;

namespace Baked.DataAccess;

public class AutomappingConfiguration
{
    public List<Func<Type, bool>> ShouldMapType { get; } = [];
    public List<Func<Member, bool>> ShouldMapMember { get; } = [];
    public List<Func<Member, bool>> MemberIsId { get; } = [];
}