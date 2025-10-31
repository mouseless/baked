﻿using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Ui;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Theme.Default.DomainDatas;

namespace Baked.Ux.ActionsAsDataPanels;

public class ActionsAsDataPanelsUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddMethodComponent(
                component: (c, cc) => MethodDataPanel(c.Method, cc),
                when: c => c.Method.Has<ActionModelAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(ReportPage.Tabs), "*", nameof(ReportPage.Tab.Contents), "*", "*", nameof(ReportPage.Tab.Content.Component))
            );
            builder.Conventions.AddMethodSchema(
                schema: (c, cc) => MethodNameInline(c.Method, cc),
                when: c => c.Method.Has<ActionModelAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Title))
            );
            builder.Conventions.AddMethodComponentConfiguration<DataPanel>(
                component: (dp, c, cc) =>
                {
                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        dp.Schema.Parameters.Add(
                            parameter.GetRequiredSchema<Parameter>(cc.Drill(nameof(DataPanel), nameof(DataPanel.Parameters)))
                        );
                    }
                },
                when: c => c.Method.GetAction().Method == HttpMethod.Get
            );
        });
    }
}