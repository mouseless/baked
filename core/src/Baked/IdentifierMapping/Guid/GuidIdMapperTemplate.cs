using Baked.CodeGeneration;
using Baked.Domain.Model;

namespace Baked.IdentifierMapping.Guid;

public class GuidIdMapperTemplate(TypeModel typeModel) : CodeTemplateBase
{
    protected override IEnumerable<string> Render() =>
        [ServiceAdder()];

    string ServiceAdder() => $$"""
        namespace GuidIdentifierMapping;

        public class {{typeModel.Name}}IdMapper : IIdMapper
        {
            public void Configure(AutoPersistenceModel model)
            {
                model.Conventions.Add(ConventionBuilder.Id.When(
                    x => x.Expect(p => p.Property.PropertyType == typeof(Id) && p.Property.DeclaringType == typeof({{typeModel.CSharpFriendlyFullName}})),
                    x => x.CustomType<GuidIdentifierUserType>()
                ));

                model.Conventions.Add(ConventionBuilder.Id.When(
                    x => x.Expect(p => p.Property.PropertyType == typeof(Id) && p.Property.DeclaringType == typeof({{typeModel.CSharpFriendlyFullName}})),
                    x => x.GeneratedBy.Custom<GuidIdentifierGenerator>()
                ));
            }
        }
    """;
}
