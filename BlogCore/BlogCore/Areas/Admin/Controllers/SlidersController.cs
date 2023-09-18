using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SlidersController(IWorkContainer workContainer, IWebHostEnvironment webHostEnvironment)
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string routeMain = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (slider.Id == 0)
                {
                    // Create New Article
                    string fileName = Guid.NewGuid().ToString();
                    var load = Path.Combine(routeMain, @"Images\sliders");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var filesStreams = new FileStream(Path.Combine(load, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    
                    slider.UrlImage = @"\Images\sliders\" + fileName + extension;
                    _workContainer.sliderRepository.Add(slider);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(slider);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var slider = _workContainer.sliderRepository.Get(id.GetValueOrDefault());

            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string routeMain = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var slider1 = _workContainer.sliderRepository.Get(slider.Id);

                if (files.Count() > 0)
                {
                    // new Image for article
                    string fileName = Guid.NewGuid().ToString();
                    var load = Path.Combine(routeMain, @"Images\sliders");
                    var extension = Path.GetExtension(files[0].FileName);
                    var newExtension = Path.GetExtension(files[0].FileName);
                    if (slider1.UrlImage != null && slider1 != null && routeMain != null)
                    {
                        var routeImage = Path.Combine(routeMain, slider1.UrlImage.TrimStart('\\'));

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
                    slider.UrlImage = @"\Images\sliders\" + fileName + extension;
                    _workContainer.sliderRepository.Update(slider);
                    _workContainer.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Here when Image exists and not is update

                   slider.UrlImage = slider1.UrlImage;

                }

                _workContainer.sliderRepository.Update(slider);
                _workContainer.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }





        #region Calls Api

        [HttpGet]
        public IActionResult GetAll() 
        {
           return Json(new { data = _workContainer.sliderRepository.GetAll() }); 
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var slider = _workContainer.sliderRepository.Get(id);
            string routeFile = _webHostEnvironment.WebRootPath;
            var routeImage = Path.Combine(routeFile, slider.UrlImage.TrimStart('\\'));

            if (System.IO.File.Exists(routeImage))
            {
                System.IO.File.Delete(routeImage);
            }

            if (slider == null)
            {
                return Json(new { Success = false, message = "Error borrando el slider" });
            }

            _workContainer.sliderRepository.Remove(slider);
            _workContainer.Save();
            return Json(new { Success = true, message = "Slider eliminado correctamente" });

        }


        #endregion
    }
}
