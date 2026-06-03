using Baked.Buildtime.Diagnostics;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Export;
using Baked.Domain.Model;
using Baked.Test.Domain;
using Baked.Testing;

namespace Baked.Test;

public static class DomainModelExtensions
{
    extension(Stubber giveMe)
    {
        public AttributeCollection AnAttributeCollection(
            string? name = default,
            Attribute? item = default,
            IEnumerable<Attribute>? items = default
        )
        {
            name ??= "Test";
            items ??= [];

            var result = new AttributeCollection(name);

            if (item is not null)
            {
                AddOrSet(item);
            }

            foreach (var current in items)
            {
                AddOrSet(current);
            }

            void AddOrSet(Attribute attribute)
            {
                if (attribute.AllowsMultiple())
                {
                    ((IMutableAttributeCollection)result).Add(attribute);
                }
                else
                {
                    ((IMutableAttributeCollection)result).Set(attribute);
                }

            }

            return result;
        }

        public DomainModelBuilder ADomainModelBuilder(
            Action<DomainModelBuilderOptions>? options = default,
            bool conventionMatrixDefaults = true,
            Action<IDomainModelConventionCollection>? conventions = default,
            Action<DiagnosticsResult>? onConventionsFinalized = default
        )
        {
            var optionsInstance = giveMe.ADomainModelBuilderOptions(conventionMatrixDefaults: conventionMatrixDefaults);

            if (options is not null)
            {
                options(optionsInstance);
            }

            var conventionsInstance = new DomainModelConventionCollection(optionsInstance);
            using (var diagnostics = Diagnostics.Start(nameof(ReportingErrorsInConventions), onDispose: onConventionsFinalized))
            {
                if (conventions is not null)
                {
                    conventions(conventionsInstance);
                }
            }

            return new DomainModelBuilder(optionsInstance, conventionsInstance);
        }

        public DomainModelBuilderOptions ADomainModelBuilderOptions(
            bool conventionMatrixDefaults = true
        )
        {
            var options = new DomainModelBuilderOptions();
            options.BuildLevels.Add(BuildLevels.Metadata);
            options.OnComplete = _ => { };

            if (conventionMatrixDefaults)
            {
                options.ConventionMatrix.Bases.Add("B1");
                options.ConventionMatrix.Levels.Add("L1");
                options.ConventionMatrix.Extensions.Add("E1");

                options.ConventionMatrix.FallbackBase = _ => "B1";
                options.ConventionMatrix.FallbackLevel = _ => "L1";
                options.ConventionMatrix.FallbackExtension = _ => "E1";

                options.DefaultConventionLevel = "B1.L1.E1";
            }

            return options;
        }
    }

    extension(DomainModelBuilder builder)
    {
        public DomainModelPostBuilder StartBuild(IEnumerable<Type> types)
        {
            var collection = new DomainTypeCollection();
            collection.AddRange(types);

            return builder.StartBuild(collection);
        }
    }

    extension(List<IAttributeExport> filters)
    {
        public void ShouldContain<T>() where T : Attribute
        {
            filters.ShouldContain(f => f.Type == typeof(T));
        }

        public void ShouldNotContain<T>() where T : Attribute
        {
            filters.ShouldNotContain(f => f.Type == typeof(T));
        }
    }
}