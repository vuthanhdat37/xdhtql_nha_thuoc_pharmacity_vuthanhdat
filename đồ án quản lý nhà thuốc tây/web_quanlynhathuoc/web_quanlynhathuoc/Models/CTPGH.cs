using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("CTPGH")]
    public class CTPGH
    {
        [Key, Column(Order = 0)]
        public int MaPGH_ID { get; set; }

        [Key, Column(Order = 1)]
        public int MaThuoc_ID { get; set; }

        public int SLGiao { get; set; }
    }
}
