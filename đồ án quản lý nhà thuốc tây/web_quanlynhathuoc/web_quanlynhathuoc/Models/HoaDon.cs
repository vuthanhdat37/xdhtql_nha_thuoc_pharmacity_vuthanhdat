using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        public int MaHD_ID { get; set; }

        public string TenKH { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public string ThuocMua { get; set; }

        public int MaNV_ID { get; set; }
        [ForeignKey("MaNV_ID")]
        public NhanVien NhanVien { get; set; }

        public int MaPMH_ID { get; set; }

        // ✅ Thêm dòng này để dùng .Include(h => h.PhieuMuaHang)
        [ForeignKey("MaPMH_ID")]
        public PhieuMuaHang? PhieuMuaHang { get; set; }


    }
}
