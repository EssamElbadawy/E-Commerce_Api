using Microsoft.AspNetCore.Identity;
using Noon.Core.Entities.Identity;

namespace Noon.Core.Services
{
    public interface ITokenService
    {
        Task<String> CreateToken(AppUser user,UserManager<AppUser> userManager);
    }
}
