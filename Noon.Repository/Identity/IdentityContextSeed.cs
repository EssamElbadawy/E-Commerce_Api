using Microsoft.AspNetCore.Identity;
using Noon.Core.Entities.Identity;

namespace Noon.Repository.Identity
{
    public static class IdentityContextSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {
            var user = new AppUser()
            {
                Email = "mohamed@mail.com",
                DisplayName = "Mohamed",
                PhoneNumber = "012332611515",
                UserName = "mohamed"

            };

            await userManager.CreateAsync(user,"P@ssw0rd");
        }
    }
}
