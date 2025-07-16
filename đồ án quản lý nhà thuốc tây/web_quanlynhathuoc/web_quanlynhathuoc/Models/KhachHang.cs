using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("KhachHang")] // ánh xạ chính xác với tên bảng trong SQL
    public class KhachHang
    {
        [Key]
        public int MaKH_ID { get; set; }

        [Required]
        public string TenKH { get; set; } = string.Empty;

        public string GioiTinh { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        public string SDT { get; set; } = string.Empty;

        public string BenhLyNen { get; set; } = string.Empty;
    }
}
