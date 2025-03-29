using System.Security.Claims;

namespace Baked.Authentication;

public interface ITokenBuilder
{
    string Build(string tokenType, List<Claim> claims);
}