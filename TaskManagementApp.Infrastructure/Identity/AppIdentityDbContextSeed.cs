using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        const string DEFAULT_PASSWORD = "P@ssw0rd";
        const string USERS_Role = "USERS";

        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            var usersRole = new IdentityRole("USERS");

            await roleManager.CreateAsync(usersRole);


            string userName1 = "user1@taskManagement.com";
            var user1 = new AppUser { UserName = userName1, Email = userName1 };
            await userManager.CreateAsync(user1, DEFAULT_PASSWORD);
            user1 = await userManager.FindByNameAsync(userName1);
            await userManager.AddToRoleAsync(user1, USERS_Role);

            string userName2 = "user2@taskManagement.com";
            var user2 = new AppUser { UserName = userName2, Email = userName2 };
            await userManager.CreateAsync(user2, DEFAULT_PASSWORD);
            user2 = await userManager.FindByNameAsync(userName2);
            await userManager.AddToRoleAsync(user2, USERS_Role);

            string userName3 = "user3@taskManagement.com";
            var user3 = new AppUser { UserName = userName3, Email = userName3 };
            await userManager.CreateAsync(user3, DEFAULT_PASSWORD);
            user3 = await userManager.FindByNameAsync(userName3);
            await userManager.AddToRoleAsync(user3, USERS_Role);

            string userName4 = "user4@taskManagement.com";
            var user4 = new AppUser { UserName = userName4, Email = userName4 };
            await userManager.CreateAsync(user4, DEFAULT_PASSWORD);
            user4 = await userManager.FindByNameAsync(userName4);
            await userManager.AddToRoleAsync(user4, USERS_Role);


        }
    }
}
