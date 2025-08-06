using Web_ProjectName.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Web_ProjectName.Models
{
    public class M_SupplierConfig : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        public string mailDisplayName { get; set; }
        public string mailFrom { get; set; }
        public string mailPassword { get; set; }
        public string mailHost { get; set; }
        public int? mailPort { get; set; }
        public string mailTo { get; set; }
    }
    public class EM_SupplierConfig : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        [StringLength(250, ErrorMessage = "Tên hiển thị tối đa 250 ký tự!")]
        public string mailDisplayName { get; set; }
        [Required(ErrorMessage = "Tài khoản không được để trống!")]
        [StringLength(100, ErrorMessage = "Tài khoản tối đa 100 ký tự!")]
        public string mailFrom { get; set; }
        [Required(ErrorMessage = "Token không được để trống!")]
        [StringLength(50, ErrorMessage = "Token tối đa 50 ký tự!")]
        public string mailPassword { get; set; }
        [Required(ErrorMessage = "Host không được để trống!")]
        [StringLength(50, ErrorMessage = "Host tối đa 50 ký tự!")]
        public string mailHost { get; set; }
        [Required(ErrorMessage = "Port không được để trống!")]
        public int? mailPort { get; set; }
        [Required(ErrorMessage = "Mail nhận thông báo không được để trống!")]
        [StringLength(100, ErrorMessage = "Mail nhận thông báo tối đa 100 ký tự!")]
        public string mailTo { get; set; }
    }
}
