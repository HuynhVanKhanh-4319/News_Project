using Web_ProjectName.Models.Common;
using System.ComponentModel.DataAnnotations;
using static Web_ProjectName.ExtensionMethods.ValidationAttribute;

namespace Web_ProjectName.Models
{
    public class M_Introduce : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        public EN_IntroduceType type { get; set; }
        public string name { get; set; }
        public string nameSlug { get; set; }
        public string description { get; set; }
        public string detail { get; set; }
        public int? sort { get; set; }
        public bool? isHot { get; set; }
        public int? viewNumber { get; set; }
        public int? catalogueId { get; set; }
        public int? imageId { get; set; }
        public DateTime? publishedAt { get; set; }
        public string metaKeywords { get; set; }
        public string metaDescription { get; set; }
        public string metaTitle { get; set; }
        public string metaUrl { get; set; }
        public string metaImagePreview { get; set; }
        public M_Image catalogueObj { get; set; }
        public M_Image imageObj { get; set; }
    }
    public class EM_Introduce : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        public EN_IntroduceType type { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        [StringLength(250, ErrorMessage = "Tên tối đa 250 ký tự!")]
        public string name { get; set; }
        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự!")]
        public string description { get; set; }
        public string detail { get; set; }
        [Required(ErrorMessage = "Sắp xếp không được để trống!")]
        public int? sort { get; set; }
        public bool isHot { get; set; }
        public int? viewNumber { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày đăng")]
        public DateTime publishedAt { get; set; }
        public int? catalogueId { get; set; }
        public int? imageId { get; set; }
        [StringLength(100, ErrorMessage = "Meta Keywords có độ dài tối đa 100 ký tự")]
        public string metaKeywords { get; set; }
        [StringLength(160, ErrorMessage = "Meta Description có độ dài tối đa 160 ký tự")]
        public string metaDescription { get; set; }
        [StringLength(100, ErrorMessage = "Meta Title có độ dài tối đa 100 ký tự")]
        public string metaTitle { get; set; }
        [Required(ErrorMessage = "Meta URL không được để trống!")]
        [StringLength(200, ErrorMessage = "Meta URL có độ dài tối đa 200 ký tự")]
        public string metaUrl { get; set; }
        [StringLength(200, ErrorMessage = "Meta Image có độ dài tối đa 200 ký tự")]
        public string metaImagePreview { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(maxFileSize: Lib.CommonConstants.MAX_FILE_SIZE_IMAGE_UPLOAD * 1024 * 1024, errorMessage: "Dung lượng ảnh tối đa 5MB!")]
        [AllowedExtensions(extensions: new string[] { ".jpg", ".jpeg", ".png" }, errorMessage: "Ảnh không hợp lệ!")]
        public IFormFile imageFile { get; set; }
        public M_Image catalogueObj { get; set; }
        public M_Image imageObj { get; set; }
    }

    public enum EN_IntroduceType
    {
        Introduce,
        Support
    }
}
