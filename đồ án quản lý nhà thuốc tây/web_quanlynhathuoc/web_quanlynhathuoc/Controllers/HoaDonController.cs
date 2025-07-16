using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_quanlynhathuoc.Models;

public class HoaDonController : Controller
{
    private readonly ApplicationDbContext _context;

    public HoaDonController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Tạo hóa đơn từ phiếu mua hàng
    [HttpGet]
    public IActionResult CreateFromPhieu(int id)
    {
        ViewBag.NhanVienList = new SelectList(_context.NhanViens.ToList(), "MaNV_ID", "TenNV");
        return View(model: id); // Truyền id sang view
    }


    [HttpPost]
    public IActionResult CreateFromPhieu(int id, int MaNV_ID)
    {
        var pmh = _context.PhieuMuaHangs
            .Include(p => p.KhachHang)
            .Include(p => p.ChiTiet).ThenInclude(c => c.Thuoc)
            .FirstOrDefault(p => p.MaPMH_ID == id);

        if (pmh == null || pmh.TrangThai != "Đã thanh toán") return NotFound();

        var hd = new HoaDon
        {
            TenKH = pmh.KhachHang.TenKH,
            NgayLap = pmh.NgayMuaHang,
            TongTien = pmh.TongTien,
            ThuocMua = string.Join(", ", pmh.ChiTiet.Select(c => $"{c.Thuoc.TenThuoc} x{c.SLThuocMua}")),
            MaNV_ID = MaNV_ID,
            MaPMH_ID = pmh.MaPMH_ID
        };

        _context.HoaDons.Add(hd);
        _context.SaveChanges();

        foreach (var ct in pmh.ChiTiet)
        {
            _context.CTHDs.Add(new CTHD
            {
                MaHD_ID = hd.MaHD_ID,
                MaThuoc_ID = ct.MaThuoc_ID,
                SLThuoc = ct.SLThuocMua,
                TongTien = ct.SLThuocMua * ct.Thuoc.GiaBan
            });
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }



    // Xem danh sách hóa đơn
    public IActionResult Index()
    {
        var ds = _context.HoaDons
            .Include(h => h.PhieuMuaHang)
            .Include(h => h.PhieuMuaHang.KhachHang)
            .Include(h => h.NhanVien)
            .ToList();
        return View(ds);
    }

    // Xem chi tiết thuốc trong hóa đơn
    public IActionResult ChiTiet(int id)
    {
        var ct = _context.CTHDs
            .Include(c => c.Thuoc)
            .Where(c => c.MaHD_ID == id)
            .ToList();

        var hoadon = _context.HoaDons
            .Include(h => h.NhanVien)
            .Include(h => h.PhieuMuaHang).ThenInclude(p => p.KhachHang)
            .FirstOrDefault(h => h.MaHD_ID == id);

        if (hoadon != null)
        {
            ViewBag.MaHD = hoadon.MaHD_ID;
            ViewBag.KhachHang = hoadon.TenKH;
            ViewBag.NhanVien = hoadon.NhanVien?.TenNV;
        }

        return View(ct);
    }

}
