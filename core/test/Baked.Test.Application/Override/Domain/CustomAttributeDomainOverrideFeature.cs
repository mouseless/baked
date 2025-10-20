using Baked.Architecture;
using Baked.Test.Business;

namespace Baked.Test.Override.Domain;

public class CustomAttributeDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(
                attribute: () => new CustomAttribute(),
                when: c => c.Type.Is<Class>()
            );
            builder.Conventions.AddTypeAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c => c.Type.Is<Class>()
            );

            builder.Conventions.SetPropertyAttribute(
                attribute: () => new CustomAttribute(),
                when: c =>
                    c.Type.Is<Record>() &&
                    c.Property.Name == nameof(Record.Text)
            );
            builder.Conventions.AddPropertyAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c =>
                    c.Type.Is<Record>() &&
                    c.Property.Name == nameof(Record.Text)
            );

            builder.Conventions.SetMethodAttribute(
                attribute: () => new CustomAttribute(),
                when: c =>
                    c.Type.Is<Class>() &&
                    c.Method.Name == nameof(Class.Method)
            );
            builder.Conventions.AddMethodAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c =>
                    c.Type.Is<Class>() &&
                    c.Method.Name == nameof(Class.Method)
            );

            builder.Conventions.SetParameterAttribute(
                attribute: () => new CustomAttribute(),
                when: c =>
                    c.Type.Is<MethodSamples>() &&
                    c.Method.Name == nameof(MethodSamples.PrimitiveParameters) &&
                    c.Parameter.Name == "string"
            );
            builder.Conventions.AddParameterAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c =>
                    c.Type.Is<MethodSamples>() &&
                    c.Method.Name == nameof(MethodSamples.PrimitiveParameters) &&
                    c.Parameter.Name == "string"
            );
        });
    }
}