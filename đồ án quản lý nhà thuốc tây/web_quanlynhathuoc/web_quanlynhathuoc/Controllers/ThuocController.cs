using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_quanlynhathuoc.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace web_quanlynhathuoc.Controllers
{
    public class ThuocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThuocController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.Thuocs.ToListAsync());

        public IActionResult Create()
        {
            ViewBag.LoaiThuocList = new SelectList(_context.LoaiThuocs, "MaLoaiThuoc_ID", "TenLoaiThuoc");
            return View();
        }

        public IActionResult Mua(int id)
        {
            var thuoc = _context.Thuocs.FirstOrDefault(t => t.MaThuoc_ID == id);
            if (thuoc == null) return NotFound();

            ViewBag.Thuoc = thuoc;
            ViewBag.KhachHangList = new SelectList(_context.KhachHangs, "MaKH_ID", "TenKH");
            return View();
        }
        [HttpPost]
        public IActionResult Mua(int MaThuoc_ID, int MaKH_ID, int SLThuocMua)
        {
            var thuoc = _context.Thuocs.FirstOrDefault(t => t.MaThuoc_ID == MaThuoc_ID);
            var kh = _context.KhachHangs.Find(MaKH_ID);

            if (thuoc == null || kh == null || SLThuocMua <= 0 || SLThuocMua > thuoc.SLTonKho)
                return BadRequest("Thông tin không hợp lệ hoặc thuốc không đủ tồn kho.");

            // 1. Tạo phiếu mua hàng mới
            var pmh = new PhieuMuaHang
            {
                NgayMuaHang = DateTime.Now,
                MaKH_ID = MaKH_ID,
                TrangThai = "Chờ thanh toán",
                PTThanhToan = "Tiền mặt",
                TongTien = thuoc.GiaBan * SLThuocMua
            };
            _context.PhieuMuaHangs.Add(pmh);
            _context.SaveChanges(); // để có MaPMH_ID

            // 2. Thêm vào bảng chi tiết phiếu mua hàng
            var ct = new CTPMH
            {
                MaPMH_ID = pmh.MaPMH_ID,
                MaThuoc_ID = MaThuoc_ID,
                SLThuocMua = SLThuocMua
            };
            _context.CTPMHs.Add(ct);

            // 3. Trừ tồn kho
            thuoc.SLTonKho -= SLThuocMua;

            // 4. Tạo hóa đơn để ghi nhận doanh thu
            var hoaDon = new HoaDon
            {
                TenKH = kh.TenKH,
                NgayLap = DateTime.Now,
                TongTien = pmh.TongTien,
                ThuocMua = $"{thuoc.TenThuoc} x{SLThuocMua}",
                MaNV_ID = 1, // Giả sử tạm là nhân viên số 1, bạn có thể lấy từ Session nếu có login
                MaPMH_ID = pmh.MaPMH_ID
            };
            _context.HoaDons.Add(hoaDon);

            _context.SaveChanges();

            return RedirectToAction("Index", "PhieuMuaHang");
        }






        [HttpPost]
        public async Task<IActionResult> Create(Thuoc thuoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // QUAN TRỌNG: Gán lại dropdown khi return View
            ViewBag.LoaiThuocList = new SelectList(_context.LoaiThuocs, "MaLoaiThuoc_ID", "TenLoaiThuoc", thuoc.MaLoaiThuoc_ID);
            return View(thuoc);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var thuoc = await _context.Thuocs.FindAsync(id);
            if (thuoc == null) return NotFound();

            ViewBag.LoaiThuocList = new SelectList(_context.LoaiThuocs, "MaLoaiThuoc_ID", "TenLoaiThuoc");

            return View(thuoc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Thuoc thuoc)
        {
            if (id != thuoc.MaThuoc_ID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(thuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thuoc);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var thuoc = await _context.Thuocs.FindAsync(id);
            if (thuoc == null) return NotFound();
            return View(thuoc);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thuoc = await _context.Thuocs.FindAsync(id);
            _context.Thuocs.Remove(thuoc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var thuoc = await _context.Thuocs.FindAsync(id);
            if (thuoc == null) return NotFound();
            return View(thuoc);
        }
    }
}
