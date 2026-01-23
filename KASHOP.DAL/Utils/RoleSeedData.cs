using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Utils
{
    public class RoleSeedData : ISeedData
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleSeedData(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task DataSeed()
        {
            string[] roles = { "SuperAdmin", "Admin", "User" };

            if (!_roleManager.Roles.Any())
            {
                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
