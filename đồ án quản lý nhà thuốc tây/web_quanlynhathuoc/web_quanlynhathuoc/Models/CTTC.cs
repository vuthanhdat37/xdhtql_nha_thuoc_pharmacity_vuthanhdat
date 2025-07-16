using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("CTTC")]
    public class CTTC
    {
        public int MaTC_ID { get; set; }
        public int MaCH_ID { get; set; }
        public string? TrangThai { get; set; }

        [ForeignKey("MaTC_ID")]
        public TieuChuan? TieuChuan { get; set; }

        [ForeignKey("MaCH_ID")]
        public CuaHang? CuaHang { get; set; }
    }
}