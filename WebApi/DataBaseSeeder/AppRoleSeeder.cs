using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataBaseSeeder
{
    public static class AppRoleSeeder
    {
        public static async Task Seed(RoleManager<IdentityRole<int>> roleManager, AgreementsContext context)
        {
            // Developer Role
            if (!await roleManager.RoleExistsAsync("Notarizer"))
            {
                var role = new IdentityRole<int>
                {
                    Name = "Notarizer"
                };

                // await context.Roles.AddAsync(role);
                IdentityResult result = await roleManager.CreateAsync(role);
                // in real world migth be claims associated with roles
                //roleManager.AddClaimAsync(role, new System.Security.Claims.Claim());
                //  await context.SaveChangesAsync();
            }

            // Admin Role
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole<int>
                {
                    Name = "Admin"
                };

                await roleManager.CreateAsync(role);
                // await context.SaveChangesAsync();
            }

        }
    }
}
