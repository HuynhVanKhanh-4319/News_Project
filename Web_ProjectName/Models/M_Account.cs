using System.ComponentModel.DataAnnotations;
using Web_ProjectName.Lib;
using Web_ProjectName.Models.Common;
using static Web_ProjectName.ExtensionMethods.ValidationAttribute;

namespace Web_ProjectName.Models
{
    public class M_Account : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        /// <summary>
        /// DEV: Developer
        /// ADM: Admin
        /// </summary>
        public string accountType { get; set; }
        public int? addressId { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public M_AccountDetail accountDetailObj { get; set; }
        public M_Supplier supplierObj { get; set; }
    }
    public class M_AccountSignIn : M_Account
    {
        public string accessToken { get; set; }
        public string accessTokenRefresh { get; set; }
        public string accessTokenExpired { get; set; }
        public string accessTokenExpiredUtc7 { get; set; }
        public string accessTokenExpiredUtc7Long { get; set; }
        public M_VertifyPhone vertifyObj { get; set; }
    }
    public class M_AccountGetPermission : M_Account
    {
        public List<string> permissionCodes { get; set; }
    }
    public class M_AccountRegist : M_Account
    {
        public M_VertifyPhone vertifyObj { get; set; }
    }
    public class EM_Account
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        [StringLength(20, ErrorMessage = "Tên đăng nhập tối đa 20 ký tự!")]
        [RegularExpression("^[a-z0-9]{5,20}$", ErrorMessage = "Tên đăng nhập không hợp lệ, Tên đăng nhập hợp lệ chỉ chứa ký tự thường không dấu và số. Ví dụ: user01")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        [StringLength(50, ErrorMessage = "Mật khẩu có độ dài tối đa 50 ký tự")]
        [RegularExpression(@"^([^'ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêếìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳýỵỷỹ\-\s]+)$", ErrorMessage = "Mật khẩu không được chứa ký tự dấu và khoảng trắng")]
        public string password { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        [StringLength(10, ErrorMessage = "Tên tối đa 10 ký tự!")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Họ không được để trống!")]
        [StringLength(50, ErrorMessage = "Họ tối đa 50 ký tự!")]
        public string lastName { get; set; }
        public string accountType { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ!")]
        public DateTime? birthday { get; set; }
        public int gender { get; set; }
        public int? folkId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email!")]
        [StringLength(50, ErrorMessage = "Email có độ dài tối đa 50 ký tự")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Email không hợp lệ")]
        public string email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Điện thoại!")]
        [StringLength(12, ErrorMessage = "Điện thoại có độ dài tối đa 12 ký tự")]
        [RegularExpression("^[0-9]{0,12}$", ErrorMessage = "Điện thoại không hợp lệ")]
        public string phone { get; set; }
        public int? addressId { get; set; }
        [StringLength(150, ErrorMessage = "Ghi chú có độ dài tối đa 150 ký tự")]
        public string remark { get; set; }
        public string imageUrl { get; set; }
        public int? imageId { get; set; }
        public string qrCode { get; set; }
        [StringLength(20, ErrorMessage = "ID Card có độ dài tối đa 20 ký tự")]
        public string idCard { get; set; }
        [StringLength(20, ErrorMessage = "MST có độ dài tối đa 20 ký tự")]
        public string taxNumber { get; set; }
        [DataType(DataType.Upload)]
        [MaxFileSize(maxFileSize: CommonConstants.MAX_FILE_SIZE_IMAGE_UPLOAD * 1024 * 1024, errorMessage: "Dung lượng ảnh tối đa 5MB!")]
        [AllowedExtensions(extensions: new string[] { ".jpg", ".jpeg", ".png" }, errorMessage: "Ảnh không hợp lệ!")]
        public IFormFile imageFile { get; set; }
        public EM_Address addressObj { get; set; }
        public M_AccountDetail accountDetailObj { get; set; }
    }
    public class EM_AccountRegister
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        [StringLength(20, ErrorMessage = "Tên đăng nhập tối đa 20 ký tự!")]
        [RegularExpression("^[a-z0-9]{5,20}$", ErrorMessage = "Tên đăng nhập không hợp lệ, Tên đăng nhập hợp lệ chỉ chứa ký tự thường không dấu và số. Ví dụ: user01")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        [StringLength(10, ErrorMessage = "Tên tối đa 10 ký tự!")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Họ không được để trống!")]
        [StringLength(50, ErrorMessage = "Họ tối đa 50 ký tự!")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(50, ErrorMessage = "Mật khẩu có độ dài tối đa 50 ký tự")]
        [RegularExpression(@"^([^'ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêếìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳýỵỷỹ\-\s]+)$", ErrorMessage = "Mật khẩu không được chứa ký tự dấu và khoảng trắng")]
        public string password { get; set; }
        public string accountType { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ!")]
        public DateTime? birthday { get; set; }
        public int gender { get; set; }
        public int? folkId { get; set; }
        [StringLength(50, ErrorMessage = "Email có độ dài tối đa 50 ký tự")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Email không hợp lệ")]
        public string email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Điện thoại")]
        [StringLength(12, ErrorMessage = "Điện thoại có độ dài tối đa 12 ký tự")]
        [RegularExpression("^[0-9]{0,12}$", ErrorMessage = "Điện thoại không hợp lệ")]
        public string phone { get; set; }
        [Required(ErrorMessage = "Tên đơn vị không được để trống!")]
        [StringLength(150, ErrorMessage = "Tên đơn vị tối đa 150 ký tự!")]
        public string supplierName { get; set; }
        [StringLength(150, ErrorMessage = "Họ tên người đại diện pháp lý tối đa 150 ký tự!")]
        public string supplierHeaderFullName { get; set; }
        public int? supplierPositionId { get; set; }
        [StringLength(20, ErrorMessage = "Mã số thuế tối đa 20 ký tự!")]
        public string supplierBusinessNumber { get; set; }
        [StringLength(50, ErrorMessage = "Email đơn vị có độ dài tối đa 50 ký tự")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Email đơn vị không hợp lệ")]
        public string supplierEmail { get; set; }
        [StringLength(12, ErrorMessage = "Điện thoại có độ dài tối đa 12 ký tự")]
        [RegularExpression("^[0-9]{0,12}$", ErrorMessage = "Điện thoại không hợp lệ")]
        public string supplierPhone { get; set; }
        [StringLength(500, ErrorMessage = "Mô tả đơn vị tối đa 500 ký tự!")]
        public string supplierDescription { get; set; }
        public int? addressId { get; set; }
        public EM_Address addressObj { get; set; }
    }
    public class EM_AccountSignIn
    {
        [Required(ErrorMessage = "Nhập tài khoản")]
        [RegularExpression("^[a-z0-9_-]{3,50}$", ErrorMessage = "Tài khoản không hợp lệ")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [RegularExpression(@"^([^'ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêếìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳýỵỷỹ\-\s]+)$", ErrorMessage = "Mật khẩu không được chứa ký tự dấu và khoảng trắng.")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#?\-\$%\^&\*\[\];:_<>\.,=\+\/\\]).{8,50}$", ErrorMessage = "Mật khẩu không hợp lệ!")]
        public string password { get; set; }
        public bool stayLoggedIn { get; set; }
        public string deviceToken { get; set; }
        public int? intervalTime { get; set; } = 30;
        public string deviceName { get; set; }
        public string deviceType { get; set; }
        public string browserName { get; set; }
        public string userAgent { get; set; }
        public string ipPublic { get; set; }
        public int shopId { get; set; }
        public string tokenReCAPTCHA { get; set; }
    }
    public enum unitTime
    {
        Year,
        Month,
        Day,
        Hour,
        Minute,
        Second
    }
}
