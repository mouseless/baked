using static Baked.Ui.Datas;

namespace Baked.Ui;

public static class Actions
{
    public static Publishes Publish { get; } = new();
    public static Composables Local { get; } = new();

    public static CompositeAction Composite(
        Action<CompositeAction>? options = default
    ) => options.Apply(new());

    public static RemoteAction Remote(string path, IAction postAction,
        Action<RemoteAction>? options = default
    ) => options.Apply(new(path, postAction));

    public class Composables
    {
        public LocalAction UseRedirectBack(
            string? expected = null
        ) => UseRedirect(
            expected: expected,
            options: la => la.Options = Inline(new { back = true })
        );

        public LocalAction UseRedirect(string route,
            string? expected = null
        ) => UseRedirect(
            expected: expected,
            options: la => la.Options = Inline(new { route })
        );

        public LocalAction UseRedirect(Action<LocalAction>? options,
            string? expected = null
        ) => Use("Redirect", la =>
            {
                options.Apply(la);

                if (expected is not null)
                {
                    la.Options += Inline(new { expected });
                    la.Options += Context.Model(options: m => m.TargetProp = "actual");
                }
            });

        public LocalAction Use(string composable,
            Action<LocalAction>? options = default
        ) => options.Apply(new(composable.StartsWith("use") ? composable : $"use{composable}"));
    }

    public class Publishes
    {
        public PublishAction Event(string @event,
            Action<PublishAction>? options = default
        ) => options.Apply(new() { Event = @event, Data = Context.Model() });

        public PublishAction PageContextValue(string key,
            Action<PublishAction>? options = default
        ) => options.Apply(new() { PageContextKey = key, Data = Context.Model() });
    }
}