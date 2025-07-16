using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("Thuoc")] // <<== dòng này là quan trọng
    public class Thuoc
    {
        [Key]
        public int MaThuoc_ID { get; set; }

        public string TenThuoc { get; set; }

        public string DVT { get; set; }

        public decimal GiaNhap { get; set; }

        public decimal GiaBan { get; set; }

        public int SLTonKho { get; set; }

        public int MaLoaiThuoc_ID { get; set; }

        public DateTime NgayHetHan { get; set; }
    }
}
