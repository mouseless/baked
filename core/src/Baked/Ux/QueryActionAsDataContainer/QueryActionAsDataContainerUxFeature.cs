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
                schema: rd => rd.Query += Context.Parent(options: cd => cd.Prop = "container-parameters"),
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

            // Remove inputs other than `Sort` or `Paging` from DataPanel and
            // from DataPanel
            builder.Conventions.AddMethodComponentConfiguration<DataPanel>(
                when: c => c.Method.Has<QueryMethodAttribute>(),
                component: (dp, c) =>
                {
                    if (dp.Schema.Content.Schema is not DataContainer dc) { return; }

                    var dpInputs = dp.Schema.Inputs.ToDictionary(i => i.Name, i => i);
                    var dcInputs = dc.Inputs.ToDictionary(i => i.Name, i => i);
                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        if (parameter.Has<SortingAttribute>() || parameter.Has<PagingAttribute>())
                        {
                            if (!dpInputs.TryGetValue(parameter.Name, out var input)) { continue; }

                            dp.Schema.Inputs.Remove(input);
                        }
                        else
                        {
                            if (!dcInputs.TryGetValue(parameter.Name, out var input)) { continue; }

                            dc.Inputs.Remove(input);
                        }
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
                component: p =>
                {
                    p.Data = Context.Page(o =>
                    {
                        o.Prop = _lengthContextKey;
                        o.TargetProp = "length";
                    });

                    p.ReloadWhen(_lengthContextKey);
                }
            );
            // When there is no take parameter, set take to 10
            builder.Conventions.AddParameterComponentConfiguration<Paginator>(
                when: c => !c.Method.DefaultOverload.Parameters.Having<PagingAttribute>().Any(p => p.Get<PagingAttribute>().IsTake),
                component: p => p.Data += Inline(new { take = 10 })
            );
            // When there is take parameter, use take parameter's value from page context
            builder.Conventions.AddParameterComponentConfiguration<Paginator>(
                when: c => c.Method.DefaultOverload.Parameters.Having<PagingAttribute>().Any(p => p.Get<PagingAttribute>().IsTake),
                component: (p, c) =>
                {
                    p.Data += Context.Page(o =>
                    {
                        o.Prop = _takeContextKey;
                        o.TargetProp = "take";
                    });
                    p.ReloadWhen(_takeContextKey);
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