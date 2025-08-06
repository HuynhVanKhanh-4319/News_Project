using Web_ProjectName.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Web_ProjectName.Models
{
    public class M_RecruitmentCategory : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? parentId { get; set; }
        public int? supplierId { get; set; }
        public string name { get; set; }
        public string nameSlug { get; set; }
        public string remark { get; set; }
        public int? sort { get; set; }
    }
    public class EM_RecruitmentCategory : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? parentId { get; set; }
        public int? supplierId { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        [StringLength(250, ErrorMessage = "Tên tối đa 250 ký tự!")]
        public string name { get; set; }
        [StringLength(150, ErrorMessage = "Ghi chú tối đa 150 ký tự!")]
        public string remark { get; set; }
        [Required(ErrorMessage = "Sắp xếp không được để trống!")]
        public int? sort { get; set; }
    }
}
