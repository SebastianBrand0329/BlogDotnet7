using BlogCore.AccesoDatos.Data.Repository;
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

                if (articleVM.Article.Id == 0)
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

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticleVM articleVM = new()
            {
                Article = new BlogCore.Models.Article(),
                ListArticles = _workContainer.categoryRepository.GetListCategory()
            };

            if (id != null)
            {
                articleVM.Article = _workContainer.articleRepository.Get(id.GetValueOrDefault());
            }

            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                string routeMain = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var article = _workContainer.articleRepository.Get(articleVM.Article.Id);

                if (files.Count() > 0)
                {
                    // new Image for article
                    string fileName = Guid.NewGuid().ToString();
                    var load = Path.Combine(routeMain, @"Images\articles");
                    var extension = Path.GetExtension(files[0].FileName);
                    var newExtension = Path.GetExtension(files[0].FileName);
                    if (article.UrlImage != null && article != null && routeMain != null)
                    {
                        var routeImage = Path.Combine(routeMain, article.UrlImage.TrimStart('\\'));

                        if (System.IO.File.Exists(routeImage))
                        {
                            System.IO.File.Delete(routeImage);
                        }

                    }

                    // Load New Image
                    using (var filesStreams = new FileStream(Path.Combine(load, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    articleVM.Article.UrlImage = @"\Images\articles\" + fileName + extension;
                    articleVM.Article.DateCreation = DateTime.Now.ToString();
                    _workContainer.articleRepository.Update(articleVM.Article);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Here when Image exists and not is update

                    articleVM.Article.UrlImage = article.UrlImage;

                }

                _workContainer.articleRepository.Update(articleVM.Article);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(articleVM);
        }




        #region Llamadas a la Api
        [HttpGet]
        public IActionResult GetAll()
        {
            //Opción 1
            return Json(new { data = _workContainer.articleRepository.GetAll(includeProperties: "Category") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var article = _workContainer.articleRepository.Get(id);
            string routeFile = _webHostEnvironment.WebRootPath;
            var routeImage = Path.Combine(routeFile, article.UrlImage.TrimStart('\\'));

            if (System.IO.File.Exists(routeImage))
            {
                System.IO.File.Delete(routeImage);
            }

            if (article == null)
            {
                return Json(new { Success = false, message = "Error borrando artículo" });
            }

            _workContainer.articleRepository.Remove(article);
            _workContainer.Save();
            return Json(new { Success = true, message = "Artículo eliminado correctamente" });

        }

        #endregion
    }
}
