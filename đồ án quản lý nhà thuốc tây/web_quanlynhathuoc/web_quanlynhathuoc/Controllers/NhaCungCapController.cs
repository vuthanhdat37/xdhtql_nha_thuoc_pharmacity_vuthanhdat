using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_quanlynhathuoc.Models;

namespace web_quanlynhathuoc.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly ApplicationDbContext _context;
        public NhaCungCapController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.NhaCungCaps.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var ncc = await _context.NhaCungCaps
                .Include(n => n.LoaiThuocs)
                .FirstOrDefaultAsync(n => n.MaNCC_ID == id);

            if (ncc == null) return NotFound();
            return View(ncc);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(NhaCungCap model)
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
            var ncc = await _context.NhaCungCaps.FindAsync(id);
            if (ncc == null) return NotFound();
            return View(ncc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NhaCungCap model)
        {
            if (id != model.MaNCC_ID) return NotFound();

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
            var ncc = await _context.NhaCungCaps.FindAsync(id);
            return View(ncc);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ncc = await _context.NhaCungCaps
                .Include(n => n.LoaiThuocs)
                .FirstOrDefaultAsync(n => n.MaNCC_ID == id);

            if (ncc == null) return NotFound();

            if (ncc.LoaiThuocs != null && ncc.LoaiThuocs.Any())
            {
                ModelState.AddModelError("", "Không thể xóa vì còn loại thuốc liên kết với nhà cung cấp.Hãy xóa loại thuốc rồi mới xóa được nhà cung cấp.");
                return View("Delete", ncc);
            }

            _context.NhaCungCaps.Remove(ncc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
