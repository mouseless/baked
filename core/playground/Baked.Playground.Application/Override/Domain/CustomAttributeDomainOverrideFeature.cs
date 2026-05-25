using Baked.Architecture;
using Baked.Playground.Business;

namespace Baked.Playground.Override.Domain;

public class CustomAttributeDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                attribute: () => new CustomAttribute(),
                when: c => c.Type.Is<Class>()
            );
            conventions.AddTypeAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c => c.Type.Is<Class>()
            );

            conventions.SetPropertyAttribute(
                attribute: () => new CustomAttribute(),
                when: c =>
                    c.Type.Is<Record>() &&
                    c.Property.Name == nameof(Record.Text)
            );
            conventions.AddPropertyAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c =>
                    c.Type.Is<Record>() &&
                    c.Property.Name == nameof(Record.Text)
            );

            conventions.SetMethodAttribute(
                attribute: () => new CustomAttribute(),
                when: c =>
                    c.Type.Is<Class>() &&
                    c.Method.Name == nameof(Class.Method)
            );
            conventions.AddMethodAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c =>
                    c.Type.Is<Class>() &&
                    c.Method.Name == nameof(Class.Method)
            );

            conventions.SetParameterAttribute(
                attribute: () => new CustomAttribute(),
                when: c =>
                    c.Type.Is<MethodSamples>() &&
                    c.Method.Name == nameof(MethodSamples.PrimitiveParameters) &&
                    c.Parameter.Name == "string"
            );
            conventions.AddParameterAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c =>
                    c.Type.Is<MethodSamples>() &&
                    c.Method.Name == nameof(MethodSamples.PrimitiveParameters) &&
                    c.Parameter.Name == "string"
            );
        });
    }
}