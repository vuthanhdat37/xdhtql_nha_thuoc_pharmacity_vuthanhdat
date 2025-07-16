using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_quanlynhathuoc.Models;
using System.Threading.Tasks;
using System.Linq;

namespace web_quanlynhathuoc.Controllers
{
    public class CuaHangController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuaHangController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.CuaHangs.ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenCH,DiaChi,SDT,TieuChuanGPP,CoSoVatChat,HoatDongNhaThuoc")] CuaHang model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var ch = await _context.CuaHangs.FindAsync(id);
            if (ch == null) return NotFound();
            return View(ch);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CuaHang model)
        {
            if (id != model.MaCH_ID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ch = await _context.CuaHangs.FindAsync(id);
            if (ch == null) return NotFound();
            return View(ch);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ch = await _context.CuaHangs.FindAsync(id);
            _context.CuaHangs.Remove(ch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var cuaHang = await _context.CuaHangs.FindAsync(id);
            var cttc = await _context.Set<CTTC>()
                .Where(x => x.MaCH_ID == id)
                .Include(x => x.TieuChuan)
                .ToListAsync();

            ViewBag.CTTC = cttc;
            return View(cuaHang);
        }
    }
}
