using Baked.Domain.Model;
using Baked.Ui;

using static Baked.Ui.Datas;

namespace Baked.Theme.Admin;

public static class DomainDatas
{
    public static InlineData EnumInline(TypeModel type, ComponentContext context,
        bool requireLocalization = true
    )
    {
        var (_, l) = context;

        return Inline(
            value: !requireLocalization
                ? type.SkipNullable().GetEnumNames()
                : type
                    .SkipNullable()
                    .GetEnumNames()
                    .Select(name => new { text = l($"{type.SkipNullable().Name}.{name}"), value = name }),
            options: id => id.RequireLocalization = requireLocalization ? true : null
        );
    }
}