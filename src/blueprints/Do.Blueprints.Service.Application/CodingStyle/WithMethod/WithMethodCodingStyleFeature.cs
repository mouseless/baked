using Do.Architecture;
using Do.Business.Attributes;
using Do.Lifetime;
using Do.Orm;
using Do.RestApi.Configuration;
using Humanizer;

namespace Do.CodingStyle.WithMethod;

public class WithMethodCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddType(new TransientAttribute(),
                when: type =>
                    type.IsClass && !type.IsAbstract &&
                    type.TryGetMembers(out var members) &&
                    members.Has<ServiceAttribute>() &&
                    members.Methods.Contains("With")
            );
        });

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new EntityPublicWithIsPostResourceConvention());
        });
    }

    class EntityPublicWithIsPostResourceConvention : IApiModelConvention<ParameterModelContext>
    {
        public void Apply(ParameterModelContext context)
        {
            if (context.Parameter.IsInvokeMethodParameter) { return; }
            if (!context.Parameter.TypeModel.TryGetMetadata(out var metadata) || !metadata.Has<EntityAttribute>()) { return; }
            if (context.Action.Id != "With") { return; }

            context.Parameter.Name = "newTarget";
            context.Parameter.Type = $"Func<{context.Parameter.Type}>";
            context.Action.FindTargetStatement = "newTarget()";
            context.Action.Route = $"{context.Parameter.TypeModel.Name.Pluralize()}";
        }
    }
}