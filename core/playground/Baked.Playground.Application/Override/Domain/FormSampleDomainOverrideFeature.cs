using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.Orm;
using Baked.Playground.Theme;
using Baked.Theme.Default;
using Baked.Ui;

using B = Baked.Ui.Components;

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
                attribute: () => new QueryMethodAttribute(),
                order: Order.At.Infra
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

            // Properties
            conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().IsEnum,
                where: cc => cc.Path.StartsWith("page", "form-sample"),
                component: () => B.Text(),
                order: Order.At.Override
            );
        });
    }
}