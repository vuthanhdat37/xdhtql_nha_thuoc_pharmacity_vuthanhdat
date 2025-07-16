using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("CTDDH")]
    public class CTDDH
    {
        [Key, Column(Order = 0)]
        public int MaDDH_ID { get; set; }

        [Key, Column(Order = 1)]
        public int MaThuoc_ID { get; set; }

        public int SLDat { get; set; }
    }
}
