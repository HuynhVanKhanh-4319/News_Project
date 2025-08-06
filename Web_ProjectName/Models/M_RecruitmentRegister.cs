using Web_ProjectName.Models.Common;
using System.ComponentModel.DataAnnotations;
using static Web_ProjectName.ExtensionMethods.ValidationAttribute;

namespace Web_ProjectName.Models
{
    public class M_RecruitmentRegister : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? recruitmentId { get; set; }
        public int? supplierId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string imageList { get; set; }
        public string remark { get; set; }
        public M_Recruitment recruitmentObj { get; set; }
        public List<M_Image> imageListObjs { get; set; }
    }
    public class EM_RecruitmentRegister : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int recruitmentId { get; set; }
        public int? supplierId { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        [StringLength(50, ErrorMessage = "Tên tối đa 50 ký tự!")]
        public string name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email không được để trống!")]
        [StringLength(100, ErrorMessage = "Email tối đa 100 ký tự!")]
        public string email { get; set; }
        [Required(ErrorMessage = "Điện thoại không được để trống!")]
        [StringLength(20, ErrorMessage = "Điện thoại tối đa 20 ký tự!")]
        public string phone { get; set; }
        [StringLength(150, ErrorMessage = "Ghi chú tối đa 150 ký tự!")]
        public string remark { get; set; }
        public string imageList { get; set; }
        [DataType(DataType.Upload)]
        [MaxFile(maxFile: 2, errorMessage: "Giới hạn tải lên tối đa 2 tệp!")]
        [MaxFileSizeInList(maxFileSize: Lib.CommonConstants.MAX_FILE_SIZE_IMAGE_UPLOAD * 1024 * 1024, errorMessage: "Dung lượng tệp tối đa 5MB!")]
        [AllowedExtensionsInList(extensions: new string[] { ".jpg", ".jpeg", ".png", ".pdf", ".pdf", ".doc", ".docx", ".xls", ".xlsx" }, errorMessage: "Tệp không hợp lệ!")]
        public List<IFormFile> listFile { get; set; }
        public M_Recruitment recruitmentObj { get; set; }
        public List<M_Image> imageListObjs { get; set; }
    }
}
