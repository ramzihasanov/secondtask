using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            List<Tag> tags = _context.Tags.ToList();
            return View(tags);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid) return View(tag);
            if (_context.Tags.Any(x => x.Name.Trim().ToLower() == tag.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "This tag already exist!");
                return View(tag);
            }
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var wanted = _context.Tags.FirstOrDefault(x => x.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Update(Tag tag)
        {
            var exist = _context.Tags.FirstOrDefault(x => x.Id == tag.Id);
            if (exist == null) return NotFound();
            if (ModelState.IsValid) return View(tag);
            if (_context.Tags.Any(x => x.Id != tag.Id && x.Name.ToLower().Trim() == tag.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "This tag already exist!");
                return View(tag);
            }
            exist.Name = tag.Name;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var wanted = _context.Tags.FirstOrDefault(x => x.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Tag tag)
        {
            var wanted = _context.Tags.FirstOrDefault(x => x.Id == tag.Id);
            if (wanted == null) return NotFound();
            _context.Tags.Remove(wanted);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
