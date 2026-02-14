using FidenzApp.Domain.Data;
using FidenzApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace FidenzApp.Infranstructure.Seed
{
    public class Seed : ISeeder
    {
        private ApplicationDbContext _db;
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> roleManager;

        public Seed(ApplicationDbContext dbContext, UserManager<ApplicationUser> UserManager,
        RoleManager<IdentityRole> RoleManager)
        {
            _db = dbContext;
            userManager = UserManager;
            roleManager = RoleManager;
        }

        public async Task SeedUserAsync()
        {
            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if (await userManager.FindByNameAsync("FidenzAdmin") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "FidenzAdmin",
                    Department = "IT"
                };

                var adminResult = await userManager.CreateAsync(admin, "Admin123");

                if (adminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            if (await userManager.FindByNameAsync("FidenzUser") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "FidenzUser",
                    Department = "HR"
                };

                var userResult = await userManager.CreateAsync(user, "User123");

                if (userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }


        public async Task SeedCustomerAsync()
        {
            if (await _db.Customers_Tb.AnyAsync()) return;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "json", "UserData.json");

            if (!File.Exists(path))
                throw new FileNotFoundException($"Seed file not found: {path}");

          
            var json = await File.ReadAllTextAsync(path);
            var customers = JsonConvert.DeserializeObject<List<Customers>>(json);

            if (customers.Count != 0)
            {
                foreach (var c in customers)
                {
                    if (!await _db.Customers_Tb.AnyAsync(x => x._id == c._id))
                    {
                        await _db.Customers_Tb.AddAsync(c);
                    }
                }
                await _db.SaveChangesAsync();
            }
        }


    }
}
