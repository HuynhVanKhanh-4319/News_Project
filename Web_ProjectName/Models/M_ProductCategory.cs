using Web_ProjectName.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Web_ProjectName.Models
{
    public class M_ProductCategory : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? parentId { get; set; }
        public int? supplierId { get; set; }
        public string name { get; set; }
        public string nameSlug { get; set; }
        public string remark { get; set; }
        public string imageUrl { get; set; }
        public int? sort { get; set; }
        public List<M_ProductCategory> children { get; set; }
    }

    public class M_ProductCategory_WithCountProduct
    {
        public int? id { get; set; }
        public int? parentId { get; set; }
        public int? supplierId { get; set; }
        public string name { get; set; }
        public string nameSlug { get; set; }
        public string imageUrl { get; set; }
        public int? sort { get; set; }
        public int countProduct { get; set; }
        public List<M_ProductCategory_WithCountProduct> children { get; set; }
    }

    public class EM_ProductCategory : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? parentId { get; set; }
        public int? supplierId { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        [StringLength(250, ErrorMessage = "Tên tối đa 250 ký tự!")]
        public string name { get; set; }
        [StringLength(200, ErrorMessage = "URL ảnh tối đa 200 ký tự!")]
        public string imageUrl { get; set; }
        [StringLength(150, ErrorMessage = "Ghi chú tối đa 150 ký tự!")]
        public string remark { get; set; }
        [Required(ErrorMessage = "Sắp xếp không được để trống!")]
        public int? sort { get; set; }
    }
}
