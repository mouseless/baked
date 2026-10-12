using Baked.Architecture;
using Baked.Binding;
using Baked.Domain.Configuration;

namespace Baked;

public static class RecordsAreDtosCodingStyleFeature
{
    public static IFeature<CodingStyleFeature.Configurator> RecordsAreDtos(this CodingStyleFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                attribute: () => new Bindable(),
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Contains("<Clone>$"), // if type is record
                order: Order.At.Infra
            );
        });
    });
}