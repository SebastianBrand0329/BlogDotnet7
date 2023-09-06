using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategorysController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly ApplicationDbContext _context;

        public CategorysController(IWorkContainer workContainer, ApplicationDbContext context)
        {
            _workContainer = workContainer;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }










        #region Llamadas a la Api
        [HttpGet]
        public IActionResult GetAll()
        {
            //Opción 1
            return Json(new {data = _workContainer.categoryRepository.GetAll()});
        }


        #endregion
    }
}
