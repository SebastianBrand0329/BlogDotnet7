using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly ApplicationDbContext _context;

        public UsersController(IWorkContainer workContainer, ApplicationDbContext context)
        {
            _workContainer = workContainer;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ////Option 1
            //return View(_workContainer.userRepository.GetAll());

            //Option 2
            var claims = (ClaimsIdentity)this.User.Identity!;
            var userActual = claims.FindFirst(ClaimTypes.NameIdentifier);
            return View(_workContainer.userRepository.GetAll(u => u.Id != userActual!.Value));    
        }

        [HttpGet]
        public IActionResult BlockUser(string Id)
        {
            if (Id == null) 
            {
                return NotFound();            
            }

            _workContainer.userRepository.BlockUser(Id);
            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public IActionResult UnlockUser(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            _workContainer.userRepository.UnlockUser(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
