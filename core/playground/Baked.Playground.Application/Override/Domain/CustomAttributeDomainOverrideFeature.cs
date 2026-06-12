using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.Playground.Business;

namespace Baked.Playground.Override.Domain;

public class CustomAttributeDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                attribute: () => new CustomAttribute(),
                when: c => c.Type.Is<Class>(),
                order: Order.At.Override
            );
            conventions.AddTypeAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c => c.Type.Is<Class>(),
                order: Order.At.Override
            );

            conventions.SetPropertyAttribute(
                attribute: () => new CustomAttribute(),
                when: c =>
                    c.Type.Is<Record>() &&
                    c.Property.Name == nameof(Record.Text),
                    order: Order.At.Override
            );
            conventions.AddPropertyAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c =>
                    c.Type.Is<Record>() &&
                    c.Property.Name == nameof(Record.Text),
                    order: Order.At.Override
            );

            conventions.SetMethodAttribute(
                attribute: () => new CustomAttribute(),
                when: c =>
                    c.Type.Is<Class>() &&
                    c.Method.Name == nameof(Class.Method),
                    order: Order.At.Override
            );
            conventions.AddMethodAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c =>
                    c.Type.Is<Class>() &&
                    c.Method.Name == nameof(Class.Method),
                    order: Order.At.Override
            );

            conventions.SetParameterAttribute(
                attribute: () => new CustomAttribute(),
                when: c =>
                    c.Type.Is<MethodSamples>() &&
                    c.Method.Name == nameof(MethodSamples.PrimitiveParameters) &&
                    c.Parameter.Name == "string",
                    order: Order.At.Override
            );
            conventions.AddParameterAttributeConfiguration<CustomAttribute>(
                attribute: attr => attr.Value = "FROM CONVENTION",
                when: c =>
                    c.Type.Is<MethodSamples>() &&
                    c.Method.Name == nameof(MethodSamples.PrimitiveParameters) &&
                    c.Parameter.Name == "string",
                    order: Order.At.Override
            );
        });
    }
}