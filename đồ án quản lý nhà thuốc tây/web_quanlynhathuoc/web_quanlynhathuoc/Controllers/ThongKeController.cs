using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_quanlynhathuoc.Models;
using System.Linq;
using System;

namespace web_quanlynhathuoc.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThongKeController(ApplicationDbContext context)
        {
            _context = context;
        }


        //doanh thu theo khoảng 
        public IActionResult DoanhThuTheoKhoang(DateTime? tuNgay, DateTime? denNgay)
        {
            ViewBag.TuNgay = tuNgay?.ToString("yyyy-MM-dd");
            ViewBag.DenNgay = denNgay?.ToString("yyyy-MM-dd");

            var query = _context.PhieuMuaHangs.AsQueryable();

            if (tuNgay.HasValue && denNgay.HasValue)
            {
                query = query.Where(p => p.NgayMuaHang >= tuNgay && p.NgayMuaHang <= denNgay);
            }

            var tongDoanhThu = query.Sum(p => (decimal?)p.TongTien) ?? 0;

            ViewBag.TongDoanhThu = tongDoanhThu;
            ViewBag.DanhSach = query.Include(p => p.KhachHang).ToList();

            return View();
        }

        // 1. Thống kê doanh thu theo ngày
        public IActionResult DoanhThuTheoNgay(DateTime? ngay)
        {
            var dt = ngay ?? DateTime.Today;

            var doanhThu = _context.HoaDons
                .Where(h => h.NgayLap.Date == dt.Date)
                .Sum(h => (decimal?)h.TongTien) ?? 0;

            ViewBag.Ngay = dt;
            ViewBag.DoanhThu = doanhThu;

            return View();
        }

        // 2. Thống kê thuốc bán chạy
        public IActionResult ThuocBanChay()
        {
            var allThuoc = _context.HoaDons
                .Where(h => !string.IsNullOrEmpty(h.ThuocMua))
                .Select(h => h.ThuocMua)
                .ToList();

            var thongKe = new Dictionary<string, int>();

            foreach (var line in allThuoc)
            {
                var thuocs = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in thuocs)
                {
                    // Ví dụ: "Paracetamol 500mg x10"
                    var parts = t.Trim().Split('x');
                    if (parts.Length == 2)
                    {
                        var tenThuoc = parts[0].Trim();
                        if (int.TryParse(parts[1].Trim(), out int sl))
                        {
                            if (thongKe.ContainsKey(tenThuoc))
                                thongKe[tenThuoc] += sl;
                            else
                                thongKe[tenThuoc] = sl;
                        }
                    }
                }
            }

            // Chuyển sang danh sách object
            var result = thongKe
                .OrderByDescending(x => x.Value)
                .Take(10)
                .Select(x => new
                {
                    TenThuoc = x.Key,
                    TongSoLuong = x.Value
                }).ToList();

            return View(result);
        }


        // 3. Báo cáo thuốc sắp hết hạn
        public IActionResult ThuocSapHetHan()
        {
            var today = DateTime.Today;
            var soon = today.AddMonths(1);

            var list = _context.Thuocs
                .Where(t => t.NgayHetHan >= today && t.NgayHetHan <= soon)
                .ToList();

            return View(list);
        }

        // 4. Báo cáo tồn kho
        public IActionResult TonKho()
        {
            var tonKho = _context.Thuocs
                .Select(t => new { t.TenThuoc, t.SLTonKho })
                .ToList();

            return View(tonKho);
        }
        //thuốc hết hạn 
        public IActionResult ThuocHetHan()
        {
            var today = DateTime.Today;

            var list = _context.Thuocs
                .Where(t => t.NgayHetHan < today)
                .ToList();

            return View(list);
        }

        // 5. Thống kê hiệu suất nhân viên
        public IActionResult HieuSuatNhanVien()
        {
            var hieuSuat = _context.HoaDons
                .Include(h => h.NhanVien)
                .GroupBy(h => h.MaNV_ID)
                .Select(g => new
                {
                    TenNV = g.First().NhanVien.TenNV,
                    SoHoaDon = g.Count(),
                    TongDoanhThu = g.Sum(x => x.TongTien)
                })
                .OrderByDescending(x => x.TongDoanhThu)
                .ToList();

            return View(hieuSuat);
        }
    }
}
