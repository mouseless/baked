using Baked.Ui;

using static Baked.Theme.Admin.Components;

namespace Baked.Test.Theme.Custom;

public static class Pages
{
    public static IComponentDescriptor Menu(PageContext context)
    {
        var l = context.NewLocaleKey;

        if (context.Page.Index)
        {
            return MenuPage(context.Page.Name,
                links: context.Sitemap
                    .Where(smp => !smp.Index && smp.SideMenu)
                    .Select(smp => smp.AsCardLink(l))
            );
        }

        var sections = context.Sitemap.GroupBy(smp => smp.Section ?? string.Empty);
        if (sections.Count() <= 1)
        {
            return MenuPage(context.Page.Name,
                links: context.Sitemap
                    .Where(p => p.ParentRoute == context.Page.Route)
                    .Select(p => p.AsCardLink(l)),
                options: mp =>
                {
                    mp.Header = PageTitle(
                        title: l(context.Page.Title),
                        options: pt => pt.Description = l(context.Page.Description)
                    );
                }
            );
        }

        return MenuPage(context.Page.Name,
            options: mp =>
            {
                mp.Header = PageTitle(context.Page.Title, options: pt =>
                {
                    pt.Description = l(context.Page.Description);
                    pt.Actions.Add(Filter("menu-filter", options: f => f.Placeholder = l($"{nameof(MenuPage)}.Filter")));
                });
                mp.FilterPageContextKey = "menu-filter";
                mp.Sections.AddRange(
                    sections.Select(g => MenuPageSection(
                        options: mps =>
                        {
                            mps.Title = l(g.Key);
                            mps.Links.AddRange(g
                                .Where(p => p.ParentRoute == context.Page.Route)
                                .Select(p => Filterable(p.AsCardLink(l), options: f => f.Title = l(p.Title)))
                            );
                        }
                    )).Where(s => s.Links.Any())
                );
            }
        );
    }
}