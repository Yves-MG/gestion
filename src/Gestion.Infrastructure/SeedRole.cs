using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestion.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gestion.Infrastructure
{
    public class SeedRole
    {
        public static async Task Initialize(IServiceProvider serviceProvider, RoleManager<Role> roleManager)
        {
            var roles = new[] { "Admin", "Acheteur", "Vendeur", "Visiteur" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Role { Name = role });
                }
            }
        }
    }
}
