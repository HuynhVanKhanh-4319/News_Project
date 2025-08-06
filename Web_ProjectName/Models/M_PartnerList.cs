using Web_ProjectName.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Web_ProjectName.Models
{
    public class M_PartnerList : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        public string title { get; set; }
        public string remark { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
        public int? sort { get; set; }
    }
    public class EM_PartnerList : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        [StringLength(150, ErrorMessage = "Tên tối đa 150 ký tự!")]
        public string title { get; set; }
        [StringLength(150, ErrorMessage = "Mô tả tối đa 150 ký tự!")]
        public string remark { get; set; }
        [StringLength(200, ErrorMessage = "URL tối đa 200 ký tự!")]
        public string url { get; set; }
        [StringLength(200, ErrorMessage = "URL tối đa 200 ký tự!")]
        public string imageUrl { get; set; }
        public int? sort { get; set; }
    }
}
