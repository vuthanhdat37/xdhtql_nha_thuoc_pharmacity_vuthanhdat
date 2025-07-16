using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("CTHD")]
    public class CTHD
    {
        [Key, Column(Order = 0)]
        public int MaHD_ID { get; set; }

        [Key, Column(Order = 1)]
        public int MaThuoc_ID { get; set; }

        public int SLThuoc { get; set; }
        public decimal TongTien { get; set; }
        [ForeignKey("MaHD_ID")]
        public HoaDon HoaDon { get; set; }

        [ForeignKey("MaThuoc_ID")]
        public Thuoc Thuoc { get; set; }
    }
}
