using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("PhieuGiaoHang")]
    public class PhieuGiaoHang
    {
        [Key]
        public int MaPGH_ID { get; set; }

        public DateTime NgayGiaoHang { get; set; }
        public string TrangThaiGiaoHang { get; set; }

        public int MaDDH_ID { get; set; }
    }
}
