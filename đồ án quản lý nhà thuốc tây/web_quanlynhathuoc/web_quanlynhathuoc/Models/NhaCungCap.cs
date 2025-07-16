using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_quanlynhathuoc.Models
{
    [Table("NhaCungCap")]
    public class NhaCungCap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNCC_ID { get; set; }

        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        public string TenNCC { get; set; }

        public string DiaChi { get; set; }

        public string SDT { get; set; }

        public string Email { get; set; }

        public ICollection<LoaiThuoc>? LoaiThuocs { get; set; }



    }
}
