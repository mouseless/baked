using Baked.Domain.Model;
using Baked.Ui;
using Humanizer;

using static Baked.Ui.Actions;

namespace Baked.Theme.Default;

public static class DomainActions
{
    public static RemoteAction MethodRemote(MethodModel method,
        Action<RemoteAction>? options = default
    ) => Remote(method.GetAction().GetRoute(), Emit(method.Name.Kebaberize()),
        options: ra =>
        {
            ra.Method = method.GetAction().Method.Method;

            options.Apply(ra);
        }
    );
}