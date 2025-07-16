using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using web_quanlynhathuoc.Models;
using Microsoft.EntityFrameworkCore;


public class PhieuMuaHangController : Controller
{
    private readonly ApplicationDbContext _context;

    public PhieuMuaHangController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var ds = _context.PhieuMuaHangs.Include(p => p.KhachHang).ToList();
        return View(ds);
    }
    public IActionResult CapNhatTrangThai(int id)
    {
        var pmh = _context.PhieuMuaHangs.Find(id);
        if (pmh == null) return NotFound();
        return View(pmh);
    }

    [HttpPost]
    public IActionResult CapNhatTrangThai(int id, string trangThai)
    {
        var pmh = _context.PhieuMuaHangs
            .Include(p => p.KhachHang)
            .FirstOrDefault(p => p.MaPMH_ID == id);

        if (pmh == null) return NotFound();

        pmh.TrangThai = trangThai;
        _context.SaveChanges();

        if (trangThai == "Đã thanh toán")
        {
            // Kiểm tra hóa đơn đã tồn tại chưa
            bool hoaDonTonTai = _context.HoaDons.Any(h => h.MaPMH_ID == id);
            if (!hoaDonTonTai)
            {
                // Tạo hóa đơn mới
                var hoaDon = new HoaDon
                {
                    TenKH = pmh.KhachHang.TenKH,
                    NgayLap = DateTime.Now,
                    TongTien = pmh.TongTien,
                    ThuocMua = "", // Tạm thời
                    MaNV_ID = 1, // Nếu bạn có đăng nhập nhân viên thì dùng User.Identity
                    MaPMH_ID = pmh.MaPMH_ID
                };
                _context.HoaDons.Add(hoaDon);
                _context.SaveChanges();

                // Lấy danh sách chi tiết phiếu
                var ctpmhList = _context.CTPMHs
                    .Include(c => c.Thuoc)
                    .Where(c => c.MaPMH_ID == id)
                    .ToList();

                string thuocMua = "";

                foreach (var ct in ctpmhList)
                {
                    // Gộp tên thuốc và số lượng vào hóa đơn
                    thuocMua += $"{ct.Thuoc.TenThuoc} x{ct.SLThuocMua}, ";

                    var cthd = new CTHD
                    {
                        MaHD_ID = hoaDon.MaHD_ID,
                        MaThuoc_ID = ct.MaThuoc_ID,
                        SLThuoc = ct.SLThuocMua,
                        TongTien = ct.Thuoc.GiaBan * ct.SLThuocMua
                    };
                    _context.CTHDs.Add(cthd);
                }

                // Cập nhật lại chuỗi thuốc mua
                hoaDon.ThuocMua = thuocMua.TrimEnd(',', ' ');
                _context.SaveChanges();
            }
        }

        return RedirectToAction("Index");
    }




    public IActionResult Create()
    {
        ViewBag.KhachHangList = _context.KhachHangs.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(PhieuMuaHang pmh)
    {
        if (ModelState.IsValid)
        {
            _context.PhieuMuaHangs.Add(pmh);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.KhachHangList = _context.KhachHangs.ToList();
        return View(pmh);
    }

    public IActionResult Edit(int id)
    {
        var pmh = _context.PhieuMuaHangs.Find(id);
        if (pmh == null) return NotFound();
        ViewBag.KhachHangList = _context.KhachHangs.ToList();
        return View(pmh);
    }

    [HttpPost]
    public IActionResult Edit(PhieuMuaHang pmh)
    {
        _context.PhieuMuaHangs.Update(pmh);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var pmh = _context.PhieuMuaHangs.Find(id);
        if (pmh == null) return NotFound();
        return View(pmh);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var pmh = _context.PhieuMuaHangs
            .Include(p => p.ChiTiet) // load chi tiết
            .FirstOrDefault(p => p.MaPMH_ID == id);

        if (pmh != null)
        {
            // Xóa toàn bộ chi tiết phiếu mua hàng trước
            if (pmh.ChiTiet != null && pmh.ChiTiet.Any())
            {
                _context.CTPMHs.RemoveRange(pmh.ChiTiet);
            }

            _context.PhieuMuaHangs.Remove(pmh);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }



    public IActionResult ChiTiet(int id)
    {
        var ct = _context.CTPMHs
            .Include(c => c.Thuoc)
            .Where(c => c.MaPMH_ID == id)
            .ToList();

        ViewBag.PMH_ID = id;
        ViewBag.ThuocList = new SelectList(_context.Thuocs.ToList(), "MaThuoc_ID", "TenThuoc");
        return View(ct);
    }

    [HttpPost]
    public IActionResult ThemChiTiet(CTPMH ct)
    {
        _context.CTPMHs.Add(ct);
        _context.SaveChanges();
        return RedirectToAction("ChiTiet", new { id = ct.MaPMH_ID });
    }

    public IActionResult XoaChiTiet(int pmhId, int thuocId)
    {
        var ct = _context.CTPMHs.Find(pmhId, thuocId);
        if (ct != null)
        {
            _context.CTPMHs.Remove(ct);
            _context.SaveChanges();
        }
        return RedirectToAction("ChiTiet", new { id = pmhId });
    }
}
