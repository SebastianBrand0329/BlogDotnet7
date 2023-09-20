using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Data;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _workContainer.categoryRepository.Add(category);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);  
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = new ();
            category = _workContainer.categoryRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _workContainer.categoryRepository.Update(category);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }








        #region Llamadas a la Api
        [HttpGet]
        public IActionResult GetAll()
        {
            //Opción 1
            return Json(new {data = _workContainer.categoryRepository.GetAll()});
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var category = _workContainer.categoryRepository.Get(id);

            if (category == null)
            {
                return Json(new { success = false, message = "Error borrando categoría" });
            }

            _workContainer.categoryRepository.Remove(category);
            _workContainer.Save();
            return Json(new { success = true, message = "Categoría eliminada correctamente" });
        }

        #endregion
    }
}
