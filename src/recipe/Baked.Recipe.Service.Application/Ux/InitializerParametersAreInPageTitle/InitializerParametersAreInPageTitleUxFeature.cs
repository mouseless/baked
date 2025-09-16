using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;
using Baked.Theme.Admin;

namespace Baked.Ux.InitializerParametersAreInPageTitle;

public class InitializerParametersAreInPageTitleUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeComponentConfiguration<ReportPage>(
                component: (reportPage, c, cc) =>
                {
                    var members = c.Type.GetMembers();
                    var initializer = members.Methods.Having<InitializerAttribute>().Single();

                    reportPage.Schema.QueryParameters.AddRange(
                        initializer
                            .DefaultOverload.Parameters
                            .Select(p => p.GetRequiredSchema<Parameter>(cc.Drill(nameof(ReportPage), nameof(ReportPage.QueryParameters))))
                    );
                },
                whenType: c => c.Type.Has<TransientAttribute>() && c.Type.HasMembers()
            );
        });
    }
}