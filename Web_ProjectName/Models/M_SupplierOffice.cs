using System.ComponentModel.DataAnnotations;
using Web_ProjectName.Models.Common;
using static Web_ProjectName.Models.Common.M_BaseModel;

namespace Web_ProjectName.Models
{
    public class M_SupplierOffice : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        public string title { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string addressNumber { get; set; }
        public string addressText { get; set; }
        public int? countryId { get; set; }
        public int? provinceId { get; set; }
        public int? districtId { get; set; }
        public int? wardId { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public CountryCustom countryObj { get; set; }
        public ProvinceCustom provinceObj { get; set; }
        public DistrictCustom districtObj { get; set; }
        public WardCustom wardObj { get; set; }
    }
    public class EM_SupplierOffice : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        [StringLength(150, ErrorMessage = "Tiêu đề có độ dài tối đa 100 ký tự")]
        public string title { get; set; }
        [StringLength(50, ErrorMessage = "Điện thoại có độ dài tối đa 50 ký tự")]
        public string phone { get; set; }
        [StringLength(100, ErrorMessage = "Email có độ dài tối đa 100 ký tự")]
        public string email { get; set; }
        [StringLength(100, ErrorMessage = "Số nhà có độ dài tối đa 100 ký tự")]
        public string addressNumber { get; set; }
        [StringLength(100, ErrorMessage = "Địa chỉ có độ dài tối đa 100 ký tự")]
        public string addressText { get; set; }
        public int? countryId { get; set; }
        public int? provinceId { get; set; }
        public int? districtId { get; set; }
        public int? wardId { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public CountryCustom countryObj { get; set; }
        public ProvinceCustom provinceObj { get; set; }
        public DistrictCustom districtObj { get; set; }
        public WardCustom wardObj { get; set; }
    }
}