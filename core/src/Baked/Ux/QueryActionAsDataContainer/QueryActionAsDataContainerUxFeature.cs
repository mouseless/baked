using Baked.Architecture;
using Baked.Business;
using Baked.Ui;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Ui.Actions;
using static Baked.Ui.Datas;

using B = Baked.Ui.Components;

namespace Baked.Ux.QueryActionAsDataContainer;

public class QueryActionAsDataContainerUxFeature(int[] _pageSizeOptions)
    : IFeature<UxConfigurator>
{
    static readonly string _lengthContextKey = "length-context-key";
    static readonly string _takeContextKey = "take-context-key";

    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            // Order is set to -10 to allow DataPanel override
            builder.Conventions.AddMethodComponent(
                when: c => c.Method.Has<QueryMethodAttribute>(),
                where: cc => cc.Path.EndsWith("Contents", "*", "*", nameof(Content.Component)),
                component: (c, cc) => MethodDataContainer(c.Method, cc),
                order: -10
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Method.Has<QueryMethodAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Content)),
                component: (c, cc) => MethodDataContainer(c.Method, cc)
            );

            // Add sort and paging parameters to RemoteData query
            builder.Conventions.AddMethodSchemaConfiguration<RemoteData>(
                when: c => c.Method.Has<QueryMethodAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(DataContainer), nameof(DataContainer.Content), "*", nameof(IComponentDescriptor.Data)),
                schema: rd => rd.Query += Context.Parent(options: cd => cd.Prop = "sort-paging-parameters"),
                order: 20
            );

            // Add all inputs to DataContainer
            builder.Conventions.AddMethodComponentConfiguration<DataContainer>(
                component: (dc, c, cc) =>
                {
                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        var input = parameter.GenerateRequiredSchema<Input>(cc.Drill(nameof(DataContainer), nameof(DataContainer.Inputs)));
                        dc.Schema.Inputs.Add(input);
                    }
                }
            );

            // When skip parameter exists, add take data to skip component
            builder.Conventions.AddMethodComponentConfiguration<DataContainer>(
                when: c => c.Method.DefaultOverload.Parameters.Having<PagingAttribute>().Any(p => p.Get<PagingAttribute>().IsTake),
                component: (dc, c, cc) =>
                {
                    var skipParameter = c.Method.DefaultOverload.Parameters.Having<PagingAttribute>().FirstOrDefault(p => p.Get<PagingAttribute>().IsSkip);
                    if (skipParameter is null) { return; }

                    var skipInput = dc.Schema.Inputs.First(i => i.Name == skipParameter.Name);
                    skipInput.Component.Data += Context.Page(o =>
                    {
                        o.Prop = _takeContextKey;
                        o.TargetProp = "take";
                    });
                    skipInput.Component.ReloadWhen(_takeContextKey);
                }
            );

            // Set paging inputs to be required and numeric
            builder.Conventions.AddParameterSchemaConfiguration<Input>(
                when: c => c.Parameter.Has<PagingAttribute>(),
                schema: input =>
                {
                    input.Required = true;
                    input.Numeric = true;
                },
                order: 10
            );

            // Remove inputs other than `Sort` or `Paging` when in DataPanel
            builder.Conventions.AddMethodComponentConfiguration<DataContainer>(
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Content)),
                component: (dc, c, cc) =>
                {
                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        if (parameter.Has<SortingAttribute>() || parameter.Has<PagingAttribute>()) { continue; }

                        var input = dc.Schema.Inputs.FirstOrDefault(i => i.Name == parameter.Name);
                        if (input is null) { continue; }

                        dc.Schema.Inputs.Remove(input);
                    }
                }
            );

            // Remove`Sort` or `Paging` inputs from DataPanel
            builder.Conventions.AddMethodComponentConfiguration<DataPanel>(
                when: c => c.Method.Has<QueryMethodAttribute>(),
                component: (dp, c, cc) =>
                {
                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        if (!parameter.Has<SortingAttribute>() && !parameter.Has<PagingAttribute>()) { continue; }

                        var input = dp.Schema.Inputs.FirstOrDefault(i => i.Name == parameter.Name);
                        if (input is null) { continue; }

                        dp.Schema.Inputs.Remove(input);
                    }
                },
                order: 10
            );

            // Disable virtual scroll, configure paginator and publish 
            // data length when skip parameter exists
            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                where: cc => cc.Path.Contains(nameof(DataContainer)),
                component: (dt, c) =>
                {
                    dt.Schema.VirtualScrollerOptions = default;

                    if (c.Method.DefaultOverload.Parameters.Any(p => p.TryGet<PagingAttribute>(out var paging) && paging.IsSkip))
                    {
                        dt.Schema.Paginator = default;
                        dt.Schema.DataLengthContextKey = _lengthContextKey;
                    }
                },
                order: 10
            );

            // Skip
            builder.Conventions.AddParameterComponent(
                when: c => c.Parameter.TryGet<PagingAttribute>(out var paging) && paging.IsSkip,
                component: () => B.Paginator()
            );
            builder.Conventions.AddParameterComponentConfiguration<Paginator>(
                component: paginator =>
                {
                    paginator.Data = Context.Page(o =>
                    {
                        o.Prop = _lengthContextKey;
                        o.TargetProp = "length";
                    });
                    paginator.Data += Inline(new { take = 10 });

                    paginator.ReloadWhen(_lengthContextKey);
                }
            );

            // Take
            builder.Conventions.AddParameterComponent(
                when: c => c.Parameter.TryGet<PagingAttribute>(out var paging) && paging.IsTake,
                component: (c, cc) =>
                {
                    cc = cc.Drill(nameof(Select));
                    var (_, l) = cc;

                    return B.Select(Inline(_pageSizeOptions, options: i => i.RequireLocalization = false));
                }
            );
            builder.Conventions.AddParameterComponentConfiguration<Select>(
                when: c => c.Parameter.TryGet<PagingAttribute>(out var paging) && paging.IsTake,
                component: s => s.Override(B.PageSize())
            );
            builder.Conventions.AddParameterComponentConfiguration<Select>(
                when: c => c.Parameter.TryGet<PagingAttribute>(out var paging) && paging.IsTake,
                component: s =>
                {
                    s.Schema.ShowClear = null;
                    s.Schema.Stateful = true;
                    s.Action = Publish.PageContextValue(_takeContextKey, o => o.Data = Context.Model());
                },
                order: 10
            );
        });
    }
}