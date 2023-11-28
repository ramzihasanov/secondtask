using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {

            List<Product> products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult Create()
        {

            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {

            ViewBag.Tags = _context.Tags.ToList();

            if (!ModelState.IsValid) return View(product);

            var check = false;
            if (product.TagIds != null)
            {
                foreach (var item in product.TagIds)
                {
                    if (!_context.Tags.Any(x => x.Id == item))
                        check = true;
                }
            }
            if (check)
            {
                ModelState.AddModelError("TagId", "Tag not found!");
                return View(product);
            }
            else
            {
                if (product.TagIds != null)
                {
                    foreach (var item in product.TagIds)
                    {
                        ProductTag bookTag = new ProductTag
                        {
                            Product = product,
                            TagId = item
                        };
                        _context.ProductTags.Add(bookTag);
                    }
                }
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {

            ViewBag.Tags = _context.Tags.ToList();

            if (!ModelState.IsValid) return View();
            var existProduct = _context.Products.FirstOrDefault(x => x.Id == id);
            return View(existProduct);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {

            ViewBag.Tags = _context.Tags.ToList();


            var existProduct = _context.Products.Include(x => x.ProductTags).FirstOrDefault(b => b.Id == product.Id);
            if (existProduct == null) return NotFound();
            if (!ModelState.IsValid) return View(product);


            existProduct.ProductTags.RemoveAll(bt => !product.TagIds.Contains(bt.TagId));

            foreach (var tagId in product.TagIds.Where(tagId => !existProduct.ProductTags.Any(pt => pt.TagId == tagId)))
            {
                ProductTag productTag = new ProductTag
                {
                    TagId = tagId
                };
                existProduct.ProductTags.Add(productTag);
            }


            existProduct.Name = product.Name;
            existProduct.Description = product.Description;
            existProduct.CostPrice = product.CostPrice;
            existProduct.DisPrice = product.DisPrice;
            existProduct.Code = product.Code;
            existProduct.SalePrice = product.SalePrice;
            existProduct.Tax = product.Tax;
            existProduct.IsAvailable = product.IsAvailable;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var wanted = _context.Products.FirstOrDefault(x => x.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            var wanted = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (wanted == null) return NotFound();
            _context.Products.Remove(wanted);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
