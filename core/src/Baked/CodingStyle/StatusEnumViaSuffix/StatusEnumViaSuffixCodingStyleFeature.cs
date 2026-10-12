using Baked.Architecture;
using Baked.Business;

namespace Baked;

public static class StatusEnumViaSuffixCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> StatusEnumViaSuffix(this CodingStyleFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureBuilder(builder =>
        {
            builder.Index.Property.Add<Status>();
        });

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                when: c => c.Type.IsEnum && c.Type.Name.EndsWith("Status"),
                attribute: () => new Severities()
            );
            conventions.EditTypeAttribute<Severities>(
                when: c => c.Type.IsEnum && c.Type.Name.EndsWith("Status"),
                attribute: (sm, c) =>
                {
                    var values = c.Type.SkipNullable().Apply(t => Enum.GetValues(t));
                    foreach (var value in values)
                    {
                        var severity = (int)value switch
                        {
                            >= 100 and < 200 => "info",
                            >= 200 and < 300 => "success",
                            >= 400 and < 500 => "warn",
                            >= 500 and < 600 => "danger",
                            _ => null
                        };
                        if (severity is null) { continue; }

                        sm.Value[$"{value}"] = severity;
                    }
                }
            );

            conventions.SetPropertyAttribute(
                when: c => c.Property.PropertyType.SkipNullable().IsEnum && c.Property.PropertyType.Name.EndsWith("Status"),
                attribute: () => new Status()
            );
        });
    });
}