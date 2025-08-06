using Web_ProjectName.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Web_ProjectName.Models
{
    public class M_Supplier : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public string refCode { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string businessNumber { get; set; }
        public string headerFullName { get; set; }
        public string description { get; set; }
        public string logoUrl { get; set; }
        public string faviconUrl { get; set; }
        public int? addressId { get; set; }
        public string hotline { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string websiteUrl { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string instagram { get; set; }
        public string zalo { get; set; }
        public string youtube { get; set; }
        public string tiktokUrl { get; set; }
        public string ministryIndustryTradeUrl { get; set; }
        public string themeColorFirst { get; set; }
        public string themeColorSecond { get; set; }
        public M_Address addressObj { get; set; }
    }
    public class EM_Supplier : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public string refCode { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!")]
        [StringLength(150, ErrorMessage = "Tên tối đa 150 ký tự!")]
        public string name { get; set; }
        [StringLength(20, ErrorMessage = "Tên viết tắt tối đa 20 ký tự!")]
        public string shortName { get; set; }
        [StringLength(20, ErrorMessage = "Mã số thuế tối đa 20 ký tự!")]
        public string businessNumber { get; set; }
        [StringLength(150, ErrorMessage = "Họ tên người đại diện pháp lý tối đa 150 ký tự!")]
        public string headerFullName { get; set; }
        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự!")]
        public string description { get; set; }
        [StringLength(200, ErrorMessage = "Logo URL có độ dài tối đa 200 ký tự")]
        public string logoUrl { get; set; }
        [StringLength(200, ErrorMessage = "Favicon URL có độ dài tối đa 200 ký tự")]
        public string faviconUrl { get; set; }
        public int? addressId { get; set; }
        [StringLength(20, ErrorMessage = "Hotline có độ dài tối đa 20 ký tự")]
        public string hotline { get; set; }
        [StringLength(20, ErrorMessage = "Điện thoại có độ dài tối đa 20 ký tự")]
        public string phone { get; set; }
        [StringLength(50, ErrorMessage = "Email có độ dài tối đa 50 ký tự")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Email không hợp lệ")]
        public string email { get; set; }
        [StringLength(200, ErrorMessage = "Website có độ dài tối đa 200 ký tự")]
        public string websiteUrl { get; set; }
        [StringLength(200, ErrorMessage = "Facebook có độ dài tối đa 200 ký tự")]
        public string facebook { get; set; }
        [StringLength(200, ErrorMessage = "Twitter có độ dài tối đa 200 ký tự")]
        public string twitter { get; set; }
        [StringLength(200, ErrorMessage = "Instagram có độ dài tối đa 200 ký tự")]
        public string instagram { get; set; }
        [StringLength(200, ErrorMessage = "Zalo có độ dài tối đa 200 ký tự")]
        public string zalo { get; set; }
        [StringLength(200, ErrorMessage = "Youtube có độ dài tối đa 200 ký tự")]
        public string youtube { get; set; }
        [StringLength(200, ErrorMessage = "Tiktok có độ dài tối đa 200 ký tự")]
        public string tiktokUrl { get; set; }
        [StringLength(200, ErrorMessage = "Link Bộ Công Thương có độ dài tối đa 200 ký tự")]
        public string ministryIndustryTradeUrl { get; set; }
        [StringLength(30, ErrorMessage = "Màu theme 01 có độ dài tối đa 30 ký tự")]
        public string themeColorFirst { get; set; }
        [StringLength(30, ErrorMessage = "Màu theme 02 có độ dài tối đa 30 ký tự")]
        public string themeColorSecond { get; set; }
        public EM_Address addressObj { get; set; }
    }
}
