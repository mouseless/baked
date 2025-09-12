using Baked.Caching;
using Baked.Domain.Model;
using Baked.Theme.Admin;
using Baked.Ui;

using static Baked.Ui.Datas;

namespace Baked.Test.Theme.Custom;

public static class DomainDatas
{
    public static IData ActionRemote(MethodModel method,
        Action<RemoteData>? options = default
    ) => Remote(method.GetAction().GetRoute(),
        options: rd =>
        {
            rd.Query = method.DefaultOverload.Parameters.Any()
                ? Composite(options: cd => cd.Parts.AddRange([Computed(Composables.UseQuery), Injected()]))
                : Computed(Composables.UseQuery);

            if (method.TryGet<ClientCacheAttribute>(out var clientCache))
            {
                rd.SetAttribute("client-cache", clientCache.Type);
            }

            options.Apply(rd);
        }
    );
}