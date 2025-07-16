using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("TieuChuan")]
    public class TieuChuan
    {
        [Key]
        public int MaTC_ID { get; set; }

        public string TenTC { get; set; }
        public string MoTa { get; set; }
        public string LoaiTC { get; set; }
        public DateTime NgayDanhGia { get; set; }
    }
}
