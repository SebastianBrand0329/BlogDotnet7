using BlogCore.Data;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.Razor.Generator;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void BlockUser(string IdUser)
        {
            var user = _context.applicationUsers.FirstOrDefault(u => u.Id == IdUser);
            user!.LockoutEnd = DateTime.Now.AddYears(1);
            _context.SaveChanges(); 
        }

        public void UnlockUser(string IdUser)
        {
            var user = _context.applicationUsers.FirstOrDefault(u => u.Id == IdUser);
            user!.LockoutEnd = DateTime.Now;
            _context.SaveChanges();
        }

    }
}
