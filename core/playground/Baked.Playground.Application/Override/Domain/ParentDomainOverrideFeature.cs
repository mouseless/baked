using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.Orm;
using Baked.RestApi.Model;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

namespace Baked.Playground.Override.Domain;

public class ParentDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddLocateAction<Parent>();
            conventions.AddEntityRemoteData<Parent>();

            conventions.SetPropertyAttribute(
                when: c => c.Type.Is<Parent>() && c.Property.Name == nameof(Parent.Surname),
                attribute: () => new LabelAttribute(),
                order: Order.At.Override
            );

            conventions.RemoveMethodAttribute<ActionAttribute>(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.RemoveChild),
                order: Order.At.Override
            );

            // Move `AddChild` action to contents
            {
                conventions.AddTypeComponentConfiguration<PageTitle>(
                    when: c => c.Type.Is<Parent>(),
                    component: (pt, c) =>
                    {
                        var addChild = c.Type.GetMembers().Methods[nameof(Parent.AddChild)];
                        var addChildRoute = addChild.Get<ActionModelAttribute>().GetRoute();

                        pt.Schema.Actions.RemoveAll(a => a.Action is RemoteAction ra && ra.Path == addChildRoute);
                    },
                    order: Order.At.Override
                );

                conventions.AddTypeComponentConfiguration<SimplePage>(
                    when: c => c.Type.Is<Parent>(),
                    component: (dp, c, cc) =>
                    {
                        var addChild = c.Type.GetMembers().Methods[nameof(Parent.AddChild)];

                        dp.Schema.Contents.Add(
                            addChild.GenerateRequiredSchema<Content>(cc.Drill(nameof(SimplePage), nameof(SimplePage.Contents)))
                        );
                    },
                    order: Order.At.Override
                );
            }

            conventions.AddMethodSchemaConfiguration<Content>(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.AddChild),
                schema: s => s.Side = true,
                order: Order.At.Override
            );

            conventions.AddTypeComponentConfiguration<Fieldset>(
                when: c => c.Type.Is<Parent>(),
                component: dt => dt.ReloadOn(nameof(Parent.Update).Kebaberize()),
                order: Order.At.Override
            );

            conventions.AddMethodComponentConfiguration<DataTable>(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.GetChildren),
                component: dt => dt.ReloadOn(nameof(Parent.AddChild).Kebaberize()),
                order: Order.At.Override
            );
        });
    }
}