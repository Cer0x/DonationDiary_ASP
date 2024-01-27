using DonationDiary_ASP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Writers;

namespace DonationDiary_ASP
{
    public class Initializer
    {
        public async Task SendUsersAndRolesAsync(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                
                if (!await roleManager.RoleExistsAsync(Role.Admin))
                    await roleManager.CreateAsync(new IdentityRole<int>(Role.Admin));
                if(!await roleManager.RoleExistsAsync(Role.User))
                    await roleManager.CreateAsync(new IdentityRole<int>(Role.User));



                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                string adminEmail = "admin@example.com";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                
                if (adminUser == null) {
                    var newAdmin = new User() {
                        Email = adminEmail,
                        UserName = "Admin",
                        UserDetails = new User_Details() {
                            Name = "Admin",
                            Surname = "Admin",
                            Phone = "111111111"
                        }
                    };

                    var result = await userManager.CreateAsync(newAdmin, "Qazxsw12345!");
                    if (!result.Succeeded)
                    {
                        // Log the errors
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                    }
                    await userManager.AddToRoleAsync(newAdmin, Role.Admin);
                }


                string userEmail = "user@example.com";
                var userUser = await userManager.FindByEmailAsync(userEmail);

                if (userUser == null)
                {
                    var newUser = new User()
                    {
                        Email = userEmail,
                        UserName = "User",
                        UserDetails = new User_Details()
                        {
                            Name = "User",
                            Surname = "User",
                            Phone = "222222222"
                        }
                    };

                    var result = await userManager.CreateAsync(newUser, "Qazxsw12345!");
                    if (!result.Succeeded)
                    {
                        // Log the errors
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                    }
                    await userManager.AddToRoleAsync(newUser, Role.User);
                }
            }
        }
    }
}
