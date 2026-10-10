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
                when: c => c.Type.Is<Parent>() && c.Property.Name is nameof(Parent.Surname),
                attribute: () => new LabelAttribute(),
                order: Order.At.Override
            );

            conventions.RemoveMethodAttribute<UiAction>(
                when: c => c.Type.Is<Parent>() && c.Method.Name is nameof(Parent.RemoveChild),
                order: Order.At.Theme.Override
            );

            // Move `AddChild` action to contents
            {
                conventions.EditTypeComponent<PageTitle>(
                    when: c => c.Type.Is<Parent>(),
                    component: (pt, c) =>
                    {
                        var addChild = c.Type.GetMembers().Methods[nameof(Parent.AddChild)];
                        var addChildRoute = addChild.Get<ApiAction>().GetRoute();

                        pt.Schema.Actions.RemoveAll(a => a.Action is RemoteAction ra && ra.Path == addChildRoute);
                    },
                    order: Order.At.Override
                );

                conventions.EditTypeComponent<SimplePage>(
                    when: c => c.Type.Is<Parent>(),
                    component: (sp, c, cc) =>
                    {
                        var addChild = c.Type.GetMembers().Methods[nameof(Parent.AddChild)];

                        sp.Schema.Contents.Add(
                            addChild.GenerateRequiredSchema<Content>(cc.Drill("simple-page", "contents", sp.Schema.Contents.Count))
                        );
                    },
                    order: Order.At.Override
                );
            }

            conventions.EditMethodSchema<Content>(
                when: c => c.Type.Is<Parent>() && c.Method.Name is nameof(Parent.AddChild),
                schema: s => s.Side = true,
                order: Order.At.Override
            );

            conventions.EditMethodComponent<SimpleForm>(
                when: c => c.Type.Is<Parent>() && c.Method.Name is nameof(Parent.AddChild),
                component: sf => sf.Schema.AlwaysShowTitle = true,
                order: Order.At.Override
            );

            conventions.EditTypeComponent<Fieldset>(
                when: c => c.Type.Is<Parent>(),
                component: dt => dt.ReloadOn(nameof(Parent.Update).Kebaberize()),
                order: Order.At.Override
            );

            conventions.EditMethodComponent<DataTable>(
                when: c => c.Type.Is<Parent>() && c.Method.Name is nameof(Parent.GetChildren),
                component: dt => dt.ReloadOn(nameof(Parent.AddChild).Kebaberize()),
                order: Order.At.Override
            );
        });
    }
}