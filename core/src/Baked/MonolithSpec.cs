using Baked.Architecture;
using Baked.Business;
using Baked.Caching;
using Baked.CodingStyle;
using Baked.CodingStyle.CommandPattern;
using Baked.CodingStyle.Initializable;
using Baked.CodingStyle.Label;
using Baked.CodingStyle.ScopedBySuffix;
using Baked.CodingStyle.UseBuiltInTypes;
using Baked.Communication;
using Baked.Core;
using Baked.Database;
using Baked.ExceptionHandling;
using Baked.Localization;
using Baked.MockOverrider;
using Baked.Orm;
using Baked.Testing;
using Baked.Theme;

namespace Baked;

public abstract class MonolithSpec : Spec
{
    public class Enum<T> where T : notnull
    {
        public static IEnumerable<T> Values() =>
            Enum.GetValues(typeof(T)).Cast<int>().Where(it => it > 0).Cast<T>();
    }

    protected static void Init(
        Func<BusinessConfigurator, IFeature<BusinessConfigurator>> business,
        IEnumerable<Func<CachingConfigurator, IFeature<CachingConfigurator>>>? cachings = default,
        Func<CommunicationConfigurator, IFeature<CommunicationConfigurator>>? communication = default,
        Func<CoreConfigurator, IFeature<CoreConfigurator>>? core = default,
        Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? database = default,
        Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>>? exceptionHandling = default,
        Func<LocalizationConfigurator, IFeature<LocalizationConfigurator>>? localization = default,
        Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>>? mockOverrider = default,
        Func<OrmConfigurator, IFeature<OrmConfigurator>>? orm = default,
        Func<ThemeConfigurator, IFeature<ThemeConfigurator>>? theme = default,

        Func<CodingStyleConfigurator, CommandPatternCodingStyleFeature>? commandPattern = default,
        Func<CodingStyleConfigurator, InitializableCodingStyleFeature>? initializable = default,
        Func<CodingStyleConfigurator, LabelCodingStyleFeature>? label = default,
        Func<CodingStyleConfigurator, ScopedBySuffixCodingStyleFeature>? scopedBySuffix = default,
        Func<CodingStyleConfigurator, UseBuiltInTypesCodingStyleFeature>? useBuiltInTypes = default,

        Action<ApplicationDescriptor>? configure = default
    )
    {
        cachings ??= [c => c.InMemory(), c => c.ScopedMemory()];
        communication ??= c => c.Mock();
        core ??= c => c.Mock();
        database ??= c => c.InMemory();
        exceptionHandling ??= c => c.ProblemDetails();
        localization ??= c => c.Dotnet();
        mockOverrider ??= c => c.FirstInterface();
        orm ??= c => c.AutoMap();

        commandPattern ??= c => c.CommandPattern();
        initializable ??= c => c.Initializable();
        label ??= c => c.Label();
        scopedBySuffix ??= c => c.ScopedBySuffix();
        useBuiltInTypes ??= c => c.UseBuiltInTypes();

        configure ??= _ => { };

        Init(app =>
        {
            app.Layers.AddCodeGeneration();
            app.Layers.AddDataAccess();
            app.Layers.AddDomain();
            app.Layers.AddRuntime();
            app.Layers.AddTesting();

            app.Features.AddBinding(c => c.Rest());
            app.Features.AddBusiness(business);
            app.Features.AddCachings(cachings);
            app.Features.AddCodingStyles(
            [
                c => c.AddRemoveChild(),
                c => c.Client(),
                commandPattern,
                c => c.EntitySubclass(),
                c => c.Id(),
                initializable,
                label,
                c => c.Locatable(),
                c => c.LocatableExtension(),
                c => c.NamespaceAsRoute(),
                c => c.ObjectAsJson(),
                c => c.Query(),
                c => c.RecordsAreDtos(),
                c => c.RemainingServicesAreSingleton(),
                c => c.RichEntity(),
                c => c.RichTransient(),
                scopedBySuffix,
                c => c.Unique(),
                c => c.UriReturnIsRedirect(),
                useBuiltInTypes,
                c => c.UseNullableTypes(),
                c => c.ValueType()
            ]);
            app.Features.AddCommunication(communication);
            app.Features.AddCore(core);
            app.Features.AddDatabase(database);
            app.Features.AddExceptionHandling(exceptionHandling);
            app.Features.AddLifetimes(
            [
                c => c.Scoped(),
                c => c.Singleton(),
                c => c.Transient()
            ]);
            app.Features.AddLocalization(localization);
            app.Features.AddMockOverrider(mockOverrider);
            app.Features.AddOrm(orm);

            if (theme is not null)
            {
                app.Features.AddUx(
                [
                    c => c.ActionsAsButtons(),
                    c => c.ActionsAreContents(),
                    c => c.ActionsAsDataPanels(),
                    c => c.DataTableDefaults(),
                    c => c.DescriptionProperty(),
                    c => c.EnumParameterIsSelect(),
                    c => c.InitializerParametersAreInPageTitle(),
                    c => c.LabelsAreFrozen(),
                    c => c.ListIsDataTable(),
                    c => c.NumericValuesAreFormatted(),
                    c => c.ObjectWithListIsDataTable(),
                    c => c.PanelParametersAreStateful(),
                    c => c.PropertiesAsFieldset(),
                    c => c.RoutedTypesAsNavLinks()
                ]);

                app.Features.AddTheme(theme);
            }

            configure(app);
        });
    }

    public override void SetUp()
    {
        base.SetUp();

        // overrides configuration mock in `MockCoreFeature` with below default value provider
        MockMe.TheConfiguration(defaultValueProvider: GetDefaultSettingsValue);
    }

    protected virtual string? GetDefaultSettingsValue(string key) =>
        "test value";
}