using Baked.Domain.Model;
using Baked.Ui;

using static Baked.Test.Theme.Custom.DomainDatas;
using static Baked.Theme.Admin.Components;

namespace Baked.Test.Theme.Custom;

public static class DomainComponents
{
    public static ComponentDescriptorAttribute<Baked.Theme.Admin.String> ActionString(MethodModel method) =>
        String(data: ActionRemote(method));
}