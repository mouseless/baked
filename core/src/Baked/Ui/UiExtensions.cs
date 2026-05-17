using Baked.Architecture;
using Baked.Ui;
using Baked.Ui.Configuration;

namespace Baked;

public static class UiExtensions
{
    public class Configurator(LayerConfigurator _configurator)
    {
        public void ConfigureAppDescriptor(Action<AppDescriptor> configure) =>
            _configurator.Configure(configure);

        public void ConfigureComponentExports(Action<ComponentExports> configure) =>
            _configurator.Configure(configure);

        public void ConfigureLayoutDescriptors(Action<LayoutDescriptors> configure) =>
            _configurator.Configure(configure);

        public void ConfigurePageDescriptors(Action<PageDescriptors> configure) =>
            _configurator.Configure(configure);

        public void UsingLocaleTemplate(Action<ILocaleTemplate> localeTemplate) =>
            _configurator.Use(localeTemplate);

        public void UsingLocalization(Action<NewLocaleKey> l) =>
            _configurator.Use(l);
    }

    extension(LayerConfigurator configurator)
    {
        public Configurator Ui => new(configurator);
    }

    extension(List<ILayer> layers)
    {
        public void AddUi() =>
            layers.Add(new UiLayer());
    }

    extension(ISupportsReaction source)
    {
        public void ReloadOn(string @event,
            IConstraint? constraint = default
        ) => source.AddReaction("reload", new OnTrigger(@event) { Constraint = constraint });

        public void ReloadWhen(string key,
            IConstraint? constraint = default
        ) => source.AddReaction("reload", new WhenTrigger(key) { Constraint = constraint });

        public void ShowOn(string @event,
            IConstraint? constraint = default
        ) => source.AddReaction("show", new OnTrigger(@event) { Constraint = constraint });

        public void ShowWhen(string key,
            IConstraint? constraint = default
        ) => source.AddReaction("show", new WhenTrigger(key) { Constraint = constraint });

        public void AddReaction(string reaction, ITrigger trigger)
        {
            source.Reactions ??= new();

            source.Reactions.TryGetValue(reaction, out var current);
            source.Reactions[reaction] = current + trigger;
        }
    }

    extension<T>(List<T> schemas) where T : IOrderableSchema
    {
        public T Get(string key) =>
            schemas.Find(i => i.Key == key) ??
            throw DiagnosticCode.MissingItem.Exception($"{key} not found in {typeof(T).Name} list");

        public int GetIndex(string key)
        {
            var result = schemas.FindIndex(i => i.Key == key);
            if (result < 0)
            {
                throw DiagnosticCode.MissingItem.Exception($"{key} not found in {typeof(T).Name} list");
            }

            return result;
        }

        public void Move(string key,
            bool toTop = false,
            bool toBottom = false,
            string? before = default,
            string? after = default
        )
        {
            int? index = null;
            if (before is not null)
            {
                index = schemas.GetIndex(before);
            }
            else if (after is not null)
            {
                index = schemas.GetIndex(after) + 1;
            }
            else if (toTop)
            {
                index = 0;
            }
            else if (toBottom)
            {
                index = schemas.Count;
            }

            if (index is null) { return; }

            schemas.Move(key, index.Value);
        }

        public void Move(string key, int index)
        {
            var oldIndex = schemas.GetIndex(key);
            if (oldIndex == index) { return; }

            if (oldIndex < index)
            {
                index--;
            }

            var schema = schemas[oldIndex];
            schemas.RemoveAt(oldIndex);
            schemas.Insert(index, schema);
        }
    }

    extension(ILabeler labeler)
    {
        public void LabelFloatIn(string label) =>
            labeler.LabelMode("float", label, variant: "in");

        public void LabelFloatOn(string label) =>
            labeler.LabelMode("float", label, variant: "on");

        public void LabelFLoatOver(string label) =>
            labeler.LabelMode("float", label, variant: "over");

        public void LabelIfta(string label) =>
            labeler.LabelMode("ifta", label);

        public void LabelNone() =>
            labeler.LabelMode(null, null);

        // WARNING
        //
        // Do NOT remove this warning disable section unintentionally.
        // Without this, GitHub Actions fails on dotnet format
#pragma warning disable IDE0051
        void LabelMode(string? mode, string? label,
            string? variant = default
        )
        {
            labeler.LabelMode = mode;
            labeler.Label = label;
            labeler.LabelVariant = variant;
        }
#pragma warning restore IDE0051
    }
}