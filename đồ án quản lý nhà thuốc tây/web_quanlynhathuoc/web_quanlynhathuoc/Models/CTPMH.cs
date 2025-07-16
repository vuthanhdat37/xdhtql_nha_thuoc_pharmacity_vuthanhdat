using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("CTPMH")]
    public class CTPMH
    {
        [Key, Column(Order = 0)]
        public int MaPMH_ID { get; set; }
        public PhieuMuaHang? PhieuMuaHang { get; set; }

        [Key, Column(Order = 1)]
        public int MaThuoc_ID { get; set; }
        public Thuoc? Thuoc { get; set; }

        public int SLThuocMua { get; set; }

    }
}
