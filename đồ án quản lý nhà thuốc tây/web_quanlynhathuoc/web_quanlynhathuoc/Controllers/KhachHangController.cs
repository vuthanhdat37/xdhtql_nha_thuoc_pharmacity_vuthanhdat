using Microsoft.AspNetCore.Mvc;
using web_quanlynhathuoc.Models;
using System.Linq;

namespace web_quanlynhathuoc.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhachHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.KhachHangs.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                _context.KhachHangs.Add(kh);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kh);
        }

        public IActionResult Edit(int id)
        {
            var kh = _context.KhachHangs.Find(id);
            return View(kh);
        }

        [HttpPost]
        public IActionResult Edit(KhachHang kh)
        {
            _context.KhachHangs.Update(kh);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var kh = _context.KhachHangs.Find(id);
            return View(kh);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var kh = _context.KhachHangs.Find(id);
            _context.KhachHangs.Remove(kh);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var kh = _context.KhachHangs.Find(id);
            return View(kh);
        }
    }
}
