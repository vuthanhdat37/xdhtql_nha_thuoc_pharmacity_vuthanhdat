using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using web_quanlynhathuoc.Models;

namespace web_quanlynhathuoc.Controllers
{
    public class TieuChuanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TieuChuanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.TieuChuans.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TieuChuan model)
        {
            if (ModelState.IsValid)
            {
                _context.TieuChuans.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var tc = _context.TieuChuans.FirstOrDefault(x => x.MaTC_ID == id);
            if (tc == null) return NotFound();
            return View(tc);
        }

        [HttpPost]
        public IActionResult Edit(TieuChuan model)
        {
            if (ModelState.IsValid)
            {
                _context.TieuChuans.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var tc = _context.TieuChuans.Find(id);
            if (tc != null)
            {
                _context.TieuChuans.Remove(tc);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult ChiTietTrangThai(int id)
        {
            var list = _context.CTTCs
                .Include(c => c.CuaHang)
                .Where(c => c.MaTC_ID == id)
                .Select(c => new
                {
                    MaTC_ID = c.MaTC_ID,
                    MaCH_ID = c.MaCH_ID,
                    TenCH = c.CuaHang.TenCH,
                    TrangThai = c.TrangThai
                })
                .ToList();

            return View(list);
        }

        public IActionResult SuaTrangThai(int maTC_ID, int maCH_ID)
        {
            var cttc = _context.CTTCs
                .Include(c => c.CuaHang)
                .Include(c => c.TieuChuan)
                .FirstOrDefault(c => c.MaTC_ID == maTC_ID && c.MaCH_ID == maCH_ID);

            if (cttc == null) return NotFound();

            return View(cttc);
        }

        [HttpPost]
        public IActionResult SuaTrangThai(CTTC model)
        {
            if (ModelState.IsValid)
            {
                var existing = _context.CTTCs
                    .FirstOrDefault(c => c.MaTC_ID == model.MaTC_ID && c.MaCH_ID == model.MaCH_ID);

                if (existing != null)
                {
                    existing.TrangThai = model.TrangThai;
                    _context.SaveChanges();
                }

                return RedirectToAction("ChiTietTrangThai", new { id = model.MaTC_ID });
            }

            model.CuaHang = _context.CuaHangs.Find(model.MaCH_ID);
            model.TieuChuan = _context.TieuChuans.Find(model.MaTC_ID);

            return View(model);
        }

        public IActionResult ThemTrangThai()
        {
            ViewBag.CuaHangList = new SelectList(_context.CuaHangs, "MaCH_ID", "TenCH");
            ViewBag.TieuChuanList = new SelectList(_context.TieuChuans, "MaTC_ID", "TenTC");
            return View();
        }

        [HttpPost]
        public IActionResult ThemTrangThai(CTTC model)
        {
            if (ModelState.IsValid)
            {
                _context.CTTCs.Add(model);
                _context.SaveChanges();
                return RedirectToAction("ChiTietTrangThai", new { id = model.MaTC_ID });
            }

            ViewBag.CuaHangList = new SelectList(_context.CuaHangs, "MaCH_ID", "TenCH");
            ViewBag.TieuChuanList = new SelectList(_context.TieuChuans, "MaTC_ID", "TenTC");
            return View(model);
        }
    }
}