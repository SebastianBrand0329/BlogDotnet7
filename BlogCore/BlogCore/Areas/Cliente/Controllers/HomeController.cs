using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogCore.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public HomeController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                Sliders = _workContainer.sliderRepository.GetAll(),
                listArticles = _workContainer.articleRepository.GetAll()
            };

            //This line is for can know if is Index of client and home page main
            ViewBag.IsHome = true;

            return View(homeVM);
        }


        public IActionResult Details(int id)
        {
            var article = _workContainer.articleRepository.Get(id);

            return View(article);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}