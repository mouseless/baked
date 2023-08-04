using FluentNHibernate;
using FluentNHibernate.Automapping;

namespace Do.DataAccess;

public class DelegatedAutomappingConfiguration : DefaultAutomappingConfiguration
{
    readonly AutomappingConfiguration _configuration;

    public DelegatedAutomappingConfiguration(AutomappingConfiguration configuration) =>
        _configuration = configuration;

    public override bool ShouldMap(Type type) => _configuration.ShouldMapType.Any(shouldMap => shouldMap(type));
    public override bool IsId(Member member) => _configuration.MemberIsId.Any(isId => isId(member));
    public override bool ShouldMap(Member member) => _configuration.ShouldMapMember.Any(shouldMap => shouldMap(member));
}
