using Baked.Domain.Model;
using Baked.Testing;

namespace Baked.Test;

public static class DomainModelExtensions
{
    public static DomainModel TheDomainModel(this Stubber giveMe) =>
        giveMe.Spec.GenerateContext.GetDomainModel();
}