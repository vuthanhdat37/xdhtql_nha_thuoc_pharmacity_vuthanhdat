using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_quanlynhathuoc.Models;


namespace web_quanlynhathuoc.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhanVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.NhanViens.ToListAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            ViewBag.CuaHangList = new SelectList(_context.CuaHangs, "MaCH_ID", "TenCH");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NhanVien model)
        {
            if (ModelState.IsValid)
            {
                _context.NhanViens.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CuaHangList = new SelectList(_context.CuaHangs, "MaCH_ID", "TenCH"); // 👈 Load lại danh sách nếu lỗi
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            if (nv == null) return NotFound();

            ViewBag.CuaHangs = new SelectList(_context.CuaHangs, "MaCH_ID", "TenCH", nv.MaCH_ID);
            return View(nv);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NhanVien model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CuaHangs = new SelectList(_context.CuaHangs, "MaCH_ID", "TenCH", model.MaCH_ID);
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            return View(nv);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            _context.NhanViens.Remove(nv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var nv = await _context.NhanViens
                .Include(nv => nv.CuaHang)
                .FirstOrDefaultAsync(nv => nv.MaNV_ID == id);

            return View(nv);
        }
    }

}
