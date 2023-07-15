using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Noon.Core.Entities.Identity;

namespace Noon.Api.Extensions
{
    public static class UserManagerExtension
    {
        public static Task<AppUser> GetUserWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);

            return user!;
        }
    }
}
