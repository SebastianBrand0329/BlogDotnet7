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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticlesController(IWorkContainer workContainer, IWebHostEnvironment webHostEnvironment)
        {
            _workContainer = workContainer;
            _webHostEnvironment = webHostEnvironment;
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

        //[HttpPost]
        //public IActionResult Create(Article article)
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleVM articleVM)
        {
            if (ModelState.IsValid) 
            {
                string routeMain = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if(articleVM.Article.Id == 0)
                {
                    // Create New Article
                    string fileName = Guid.NewGuid().ToString();
                    var load = Path.Combine(routeMain, @"Images\articles");   
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var filesStreams = new FileStream(Path.Combine(load, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    articleVM.Article.UrlImage = @"\Images\articles\" + fileName + extension;
                    articleVM.Article.DateCreation = DateTime.Now.ToString();
                    _workContainer.articleRepository.Add(articleVM.Article);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));   
                }
            }
            articleVM.ListArticles = _workContainer.categoryRepository.GetListCategory();
            return View(articleVM);
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
