using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_quanlynhathuoc.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace web_quanlynhathuoc.Controllers
{
    public class LoaiThuocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoaiThuocController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.LoaiThuocs.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.NhaCungCapList = new SelectList(_context.NhaCungCaps, "MaNCC_ID", "TenNCC");
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(LoaiThuoc model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    // Bắt lỗi cập nhật DB và hiển thị thông báo chung
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu dữ liệu. Vui lòng kiểm tra lại.");
                }
            }

            return View(model);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var loai = await _context.LoaiThuocs.FindAsync(id);
            if (loai == null) return NotFound();
            return View(loai);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, LoaiThuoc model)
        {
            if (id != model.MaLoaiThuoc_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Không thể cập nhật dữ liệu. Hãy kiểm tra lại.");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var loai = await _context.LoaiThuocs.FindAsync(id);
            if (loai == null) return NotFound();
            return View(loai);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loai = await _context.LoaiThuocs.FindAsync(id);
            _context.LoaiThuocs.Remove(loai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var loai = await _context.LoaiThuocs.FindAsync(id);
            if (loai == null) return NotFound();
            return View(loai);
        }
    }
}
