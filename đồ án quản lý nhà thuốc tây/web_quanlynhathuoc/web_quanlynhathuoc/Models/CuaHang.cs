using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("CuaHang")]
    public class CuaHang
    {
        [Key]
        public int MaCH_ID { get; set; }

        [Required(ErrorMessage = "Tên cửa hàng không được để trống")]
        public string TenCH { get; set; }

        public string DiaChi { get; set; }

        public string SDT { get; set; }

        public string TieuChuanGPP { get; set; }

        public string CoSoVatChat { get; set; }

        public string HoatDongNhaThuoc { get; set; }
    }
}
