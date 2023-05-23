using Microsoft.AspNetCore.Identity;

namespace NewZealandWalks.API.Core.Repository
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
