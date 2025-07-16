using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace web_quanlynhathuoc.Models
{
    [Table("LoaiThuoc")]
    public class LoaiThuoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng
        public int MaLoaiThuoc_ID { get; set; }

        [Required(ErrorMessage = "Tên loại thuốc không được để trống")]
        public string TenLoaiThuoc { get; set; }

        [Required(ErrorMessage = "Mã nhà cung cấp là bắt buộc")]

        // FK đúng với CSDL
        [ForeignKey("NhaCungCap")]
        public int MaNCC_ID { get; set; }
        public NhaCungCap? NhaCungCap { get; set; }

    }
}
