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

    extension(ComponentExports exports)
    {
        public void AddFromExtensions(Type type)
        {
            var extensions = type.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public) ?? [];
            var componentTypes = extensions
                .Where(m =>
                    m.ReturnType.IsAssignableTo(typeof(IComponentDescriptor)) &&
                    !m.GetGenericArguments().Any()
                )
                .Select(m => m.Name);

            exports.AddRange(componentTypes);
        }
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

    extension(List<Input> inputs)
    {
        public void Move(string name, int index)
        {
            var input = inputs.Find(i => i.Name == name) ?? throw new($"{name} not found");
            inputs.Remove(input);
            inputs.Insert(index, input);
        }
    }
}