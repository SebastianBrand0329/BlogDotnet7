using BlogCore.Data;
using BlogCore.Models;
using BlogCore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.AccesoDatos.Seeder
{
    public class Seeder : ISeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Seeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Init()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();    
                }
            }
            catch (Exception)
            {

                throw;
            }

            if (_context.Roles.Any(ro => ro.Name == CNT.Admin)) return;

            //Create Role
            _roleManager.CreateAsync(new IdentityRole(CNT.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(CNT.User)).GetAwaiter().GetResult();

            _userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = "Admin@yopmail.com",
                    Email = "Admin@yopmail.com",
                    EmailConfirmed = true,
                    Name = "Sebastián"
                }, "Admin123+").GetAwaiter().GetResult();

            ApplicationUser user = _context.applicationUsers
                                  .Where(user =>  user.Email == "Admin@yopmail.com")
                                  .FirstOrDefault()!;

            _userManager.AddToRoleAsync(user, CNT.Admin).GetAwaiter().GetResult();
        }
    }
}
