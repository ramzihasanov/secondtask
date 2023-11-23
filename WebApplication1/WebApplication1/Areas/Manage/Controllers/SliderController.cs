using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            string fileName = slider.formFile.FileName;
            if (slider.formFile.ContentType != "image/jpeg" && slider.formFile.ContentType != "image/png")
            {
                ModelState.AddModelError("FormFile", "ancaq sekil yukle :)");
            }

            if (slider.formFile.Length > 1048576)
            {
                ModelState.AddModelError("FormFile", "guce salma 1 mb az yukle");
            }

            if (slider.formFile.FileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            fileName = Guid.NewGuid().ToString() + fileName;

            string path = "C:\\Users\\ll novbe\\Desktop\\secondtask\\WebApplication1\\WebApplication1\\wwwroot\\assets\\images\\" + fileName;
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                slider.formFile.CopyTo(fileStream);
            }


            slider.Image = fileName;

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Update(int id)
        {
            Slider wantedSilider = _context.Sliders.FirstOrDefault(s => s.Id == id);

            if (wantedSilider == null) return NotFound();

            return View(wantedSilider);
        }

        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            if (!ModelState.IsValid) return View();

            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);

            if (existSlider == null) return NotFound();

            existSlider.Title1 = slider.Title1;
            existSlider.Title2 = slider.Title2;
            existSlider.Title3 = slider.Title3;
            existSlider.Description = slider.Description;
            existSlider.RedirctUrl1 = slider.RedirctUrl1;
            existSlider.Image = slider.Image;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Slider wantedSilider = _context.Sliders.FirstOrDefault(s => s.Id == id);

            if (wantedSilider == null) return NotFound();

            return View(wantedSilider);
        }

        [HttpPost]
        public IActionResult Delete(Slider slider)
        {
            var existSilider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);

            if (existSilider == null)
            {
                return NotFound();
            }

            _context.Sliders.Remove(existSilider);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
