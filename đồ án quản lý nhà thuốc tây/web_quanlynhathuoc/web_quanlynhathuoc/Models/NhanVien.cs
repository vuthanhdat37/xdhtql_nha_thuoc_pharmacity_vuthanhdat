using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        public int MaNV_ID { get; set; }

        [Required]
        public string TenNV { get; set; }

        public string GioiTinh { get; set; }

        public DateTime NgaySinh { get; set; }

        public string SDT { get; set; }

        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Mã cửa hàng không được để trống")]
        public int MaCH_ID { get; set; }

        [ForeignKey("MaCH_ID")]
        public CuaHang? CuaHang { get; set; } // ✅ Dùng nullable và KHÔNG dùng [NotMapped]

    }
}
