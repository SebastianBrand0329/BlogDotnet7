using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Data;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly ApplicationDbContext _context;

        public ArticlesController(IWorkContainer workContainer, ApplicationDbContext context)
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
            ArticleVM articleVM = new()
            {
                Article = new BlogCore.Models.Article(),
                ListArticles = _workContainer.categoryRepository.GetListCategory()
            };

            return View(articleVM); 
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            return View();
        }







        #region Llamadas a la Api
        [HttpGet]
        public IActionResult GetAll()
        {
            //Opción 1
            return Json(new { data = _workContainer.articleRepository.GetAll() });
        }

        #endregion
    }
}
