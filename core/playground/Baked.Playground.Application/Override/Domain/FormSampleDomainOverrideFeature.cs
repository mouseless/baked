using Baked.Architecture;
using Baked.Business;
using Baked.Playground.Orm;
using Baked.Playground.Theme;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;

using B = Baked.Ui.Components;

namespace Baked.Playground.Override.Domain;

public class FormSampleDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddMethodAttributeConfiguration<ActionAttribute>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.NewParent),
                attribute: (a, c) => a.RoutePathBack = "/form-sample"
            );

            builder.Conventions.AddMethodAttributeConfiguration<ActionAttribute>(
                when: c => c.Type.Is<Parent>() && c.Method.Name.Contains("Child"),
                attribute: a => a.HideInLists = true
            );

            builder.Conventions.SetMethodAttribute(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                attribute: () => new QueryMethodAttribute()
            );

            builder.Conventions.AddMethodComponentConfiguration<FormPage>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.NewParent),
                component: fp =>
                {
                    fp.Schema.ForEachInputGroup(g => g.Wide = true);
                    fp.Schema.Sections[0].InputGroups.Move("name", toTop: true);
                }
            );

            // Properties
            builder.Conventions.AddPropertyComponent(
                when: c => c.Property.PropertyType.SkipNullable().IsEnum,
                where: cc => cc.Path.StartsWith(nameof(Page), nameof(FormSample)),
                component: () => B.Text()
            );
        });
    }
}