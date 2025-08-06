using Web_ProjectName.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Web_ProjectName.Models
{
    public class M_Contact : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        public int? productId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string title { get; set; }
        public string detail { get; set; }
        public string remark { get; set; }
        public M_Product productObj { get; set; }
    }
    public class EM_Contact : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        public int? productId { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        [StringLength(50, ErrorMessage = "Tên tối đa 50 ký tự!")]
        public string name { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "Email tối đa 100 ký tự!")]
        public string email { get; set; }
        [Required(ErrorMessage = "Điện thoại không được để trống!")]
        [StringLength(20, ErrorMessage = "Điện thoại tối đa 20 ký tự!")]
        public string phone { get; set; }
        [StringLength(100, ErrorMessage = "Tiêu đề tối đa 100 ký tự!")]
        public string title { get; set; }
        [Required(ErrorMessage = "Nội dung không được để trống!")]
        [StringLength(500, ErrorMessage = "Nội dung tối đa 500 ký tự!")]
        public string detail { get; set; }
        [StringLength(150, ErrorMessage = "Ghi chú tối đa 150 ký tự!")]
        public string remark { get; set; }
        public M_Product productObj { get; set; }
    }
}
