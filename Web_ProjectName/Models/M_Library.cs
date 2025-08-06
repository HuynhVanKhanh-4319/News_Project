using Web_ProjectName.Models.Common;
using System.ComponentModel.DataAnnotations;
using static Web_ProjectName.ExtensionMethods.ValidationAttribute;

namespace Web_ProjectName.Models
{
    public class M_Library : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string nameSlug { get; set; }
        public int? libraryCategoryId { get; set; }
        public int? supplierId { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public EN_LibraryTypeLink typeLink { get; set; }
        public int? imageId { get; set; }
        public M_LibraryCategory libraryCategoryObj { get; set; }
        public M_Image imageObj { get; set; }
    }
    public class EM_Library : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        [StringLength(250, ErrorMessage = "Tên tối đa 250 ký tự!")]
        public string name { get; set; }
        public int? libraryCategoryId { get; set; }
        public int? supplierId { get; set; }
        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự!")]
        public string description { get; set; }
        [Required(ErrorMessage = "Link không được để trống!")]
        [StringLength(200, ErrorMessage = "Link có độ dài tối đa 200 ký tự")]
        public string link { get; set; }
        public EN_LibraryTypeLink typeLink { get; set; }
        public int? imageId { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(maxFileSize: Lib.CommonConstants.MAX_FILE_SIZE_IMAGE_UPLOAD * 1024 * 1024, errorMessage: "Dung lượng ảnh tối đa 5MB!")]
        [AllowedExtensions(extensions: new string[] { ".jpg", ".jpeg", ".png" }, errorMessage: "Ảnh không hợp lệ!")]
        public IFormFile imageFile { get; set; }
        public M_LibraryCategory libraryCategoryObj { get; set; }
        public M_Image imageObj { get; set; }
    }
    public enum EN_LibraryTypeLink
    {
        Image,
        Video,
        Document,
        Other
    }
}
