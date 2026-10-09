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
                when: c => c.Type.Is<Class>(),
                attribute: () => new CustomAttribute(),
                order: Order.At.Override
            );
            conventions.EditTypeAttribute<CustomAttribute>(
                when: c => c.Type.Is<Class>(),
                attribute: attr => attr.Value = "FROM CONVENTION",
                order: Order.At.Override
            );

            conventions.SetPropertyAttribute(
                when: c =>
                    c.Type.Is<Record>() &&
                    c.Property.Name is nameof(Record.Text),
                    order: Order.At.Override,
                attribute: () => new CustomAttribute()
            );
            conventions.EditPropertyAttribute<CustomAttribute>(
                when: c =>
                    c.Type.Is<Record>() &&
                    c.Property.Name is nameof(Record.Text),
                    order: Order.At.Override,
                attribute: attr => attr.Value = "FROM CONVENTION"
            );

            conventions.SetMethodAttribute(
                when: c =>
                    c.Type.Is<Class>() &&
                    c.Method.Name is nameof(Class.Method),
                    order: Order.At.Override,
                attribute: () => new CustomAttribute()
            );
            conventions.EditMethodAttribute<CustomAttribute>(
                when: c =>
                    c.Type.Is<Class>() &&
                    c.Method.Name is nameof(Class.Method),
                    order: Order.At.Override,
                attribute: attr => attr.Value = "FROM CONVENTION"
            );

            conventions.SetParameterAttribute(
                when: c =>
                    c.Type.Is<MethodSamples>() &&
                    c.Method.Name is nameof(MethodSamples.PrimitiveParameters) &&
                    c.Parameter.Name is "string",
                    order: Order.At.Override,
                attribute: () => new CustomAttribute()
            );
            conventions.EditParameterAttribute<CustomAttribute>(
                when: c =>
                    c.Type.Is<MethodSamples>() &&
                    c.Method.Name is nameof(MethodSamples.PrimitiveParameters) &&
                    c.Parameter.Name is "string",
                    order: Order.At.Override,
                attribute: attr => attr.Value = "FROM CONVENTION"
            );
        });
    }
}