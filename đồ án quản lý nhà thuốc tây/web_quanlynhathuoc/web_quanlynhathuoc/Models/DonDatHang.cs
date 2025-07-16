using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("DonDatHang")]
    public class DonDatHang
    {
        [Key]
        public int MaDDH_ID { get; set; }

        public string TenThuoc { get; set; }
        public DateTime NgayLap { get; set; }
        public int SoLuong { get; set; }

        public int MaNV_ID { get; set; }
        public int MaNCC_ID { get; set; }
    }
}
