using Baked.Binding.Rest;
using Baked.Playground.Business;
using System.Globalization;

namespace Baked.Test.Binding;

public class TypeSerializationIsAlwaysInInvariant
{
    CultureInfo _current = default!;

    [SetUp]
    public void SetUp()
    {
        _current = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("tr-TR");
    }

    [TearDown]
    public void TearDown()
    {
        CultureInfo.CurrentCulture = _current;
    }

    [Test]
    public void PolymorphicSerializationBinder_overrides_current_culture_to_use_invariant_during_camelization()
    {
        var binder = new PolymorphicSerializationBinder();

        binder.BindToName(typeof(ImplementedPolymorphic), out var assemblyName, out var typeName);

        assemblyName.ShouldBeNull();
        typeName.ShouldBe("implemented");
    }
}