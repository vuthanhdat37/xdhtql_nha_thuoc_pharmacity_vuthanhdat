using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("PhieuMuaHang")]
    public class PhieuMuaHang
    {
        [Key]
        public int MaPMH_ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgayMuaHang { get; set; }

        [Required]
        public string TrangThai { get; set; } = string.Empty;

        [Required]
        public string PTThanhToan { get; set; } = string.Empty;

        public decimal TongTien { get; set; }

        [ForeignKey("KhachHang")]
        public int MaKH_ID { get; set; }

        public KhachHang? KhachHang { get; set; }

        public ICollection<CTPMH>? ChiTiet { get; set; }
    }
}
