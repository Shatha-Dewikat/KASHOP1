using KASHOP.DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.Utils
{
    public class UserSeedData : ISeedData
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSeedData(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task DataSeed()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser
                {
                    UserName = "tshreem",
                    Email = "t@gmail.com",
                    FullName = "tariq shreem",
                    EmailConfirmed = true
                };

                var user2 = new ApplicationUser
                {
                    UserName = "DRabaya",
                    Email = "d@gmail.com",
                    FullName = "Duha Rabaya",
                    EmailConfirmed = true
                };

                var user3 = new ApplicationUser
                {
                    UserName = "Abed",
                    Email = "a@gmail.com",
                    FullName = "Abed Edaily",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user1, "Admin@123");
                await _userManager.CreateAsync(user2, "Admin@123");
                await _userManager.CreateAsync(user3, "Admin@123");

                await _userManager.AddToRoleAsync(user1, "SuperAdmin");
                await _userManager.AddToRoleAsync(user2, "Admin");
                await _userManager.AddToRoleAsync(user3, "User");
            }
        }
    }
}
