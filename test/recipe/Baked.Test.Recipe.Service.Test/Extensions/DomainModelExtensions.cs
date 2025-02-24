using Baked.Domain.Model;
using Baked.Testing;

namespace Baked.Test;

public static class DomainModelExtensions
{
    public static DomainModel TheDomainModel(this Stubber giveMe) =>
        giveMe.Spec.GenerateContext.GetDomainModel();

    public static TypeModel TheTypeModel(this Stubber giveMe, Type type) =>
        giveMe.TheDomainModel().Types[type];
}