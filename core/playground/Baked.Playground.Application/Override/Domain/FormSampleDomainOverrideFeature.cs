using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.Orm;
using Baked.Playground.Theme;
using Baked.Theme.Default;
using Baked.Ui;

namespace Baked.Playground.Override.Domain;

public class FormSampleDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.EditMethodAttribute<UiAction>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name is nameof(FormSample.NewParent),
                attribute: (a, c) => a.RoutePathBack = "/form-sample",
                order: Order.At.Override
            );

            conventions.EditMethodAttribute<UiAction>(
                when: c => c.Type.Is<Parent>() && c.Method.Name.Contains("Child"),
                attribute: a => a.HideInLists = true,
                order: Order.At.Override
            );

            conventions.SetMethodAttribute(
                when: c =>
                    c.Type.Is<FormSample>() &&
                    c.Method.Name is nameof(FormSample.GetParents) or nameof(FormSample.GetParentsRole),
                attribute: () => new QueryMethod(),
                order: Order.At.Infra
            );

            conventions.EditMethodSchema<RemoteAction>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name is nameof(FormSample.ClearParents),
                schema: b => b.PostAction = Execute.Publish.Event("parent:update", empty: true),
                order: Order.At.Override
            );
            conventions.EditMethodComponent<DataTable>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name is nameof(FormSample.GetParentsRole),
                component: dt => dt.ReloadOn("parent:update"),
                order: Order.At.Override
            );

            conventions.EditMethodComponent<FormPage>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name is nameof(FormSample.NewParent),
                component: fp =>
                {
                    fp.Schema.ForEachInputGroup(g => g.Wide = true);
                    fp.Schema.Sections[0].InputGroups.Move("name", toTop: true);
                    fp.Schema.Validations ??= [];
                    fp.Schema.Validations.AddFormSampleValidation();
                },
                order: Order.At.Override
            );
        });
    }
}