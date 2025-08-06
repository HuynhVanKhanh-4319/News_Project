using Web_ProjectName.Models.Common;
using System.ComponentModel.DataAnnotations;
using Web_ProjectName.Models.Common;

namespace Web_ProjectName.Models
{
    public class M_Banner : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlTarget { get; set; }
        public EN_BannerUrlType urlType { get; set; }
        public string fileUrl { get; set; }
        public EN_BannerFileType fileType { get; set; }
        public string page { get; set; }
        public string align { get; set; }
        public EN_BannerLocation location { get; set; }
        public DateTime? startAt { get; set; }
        public DateTime? endAt { get; set; }
        public bool isNeverExpired { get; set; }
        public int? sort { get; set; }
    }
    public class M_Banner_CustomFullLocation
    {
        public List<M_Banner> popupObjs { get; set; }
        public List<M_Banner> topObjs { get; set; }
        public List<M_Banner> midObjs { get; set; }
        public List<M_Banner> bottomObjs { get; set; }
    }
    public class EM_Banner : M_BaseModel.BaseCustom
    {
        public int? id { get; set; }
        public int? supplierId { get; set; }
        [StringLength(100, ErrorMessage = "Tiêu đề tối đa 100 ký tự!")]
        public string title { get; set; }
        [StringLength(100, ErrorMessage = "Mô tả tối đa 100 ký tự!")]
        public string description { get; set; }
        [StringLength(200, ErrorMessage = "URL tối đa 200 ký tự!")]
        public string url { get; set; }
        [StringLength(10, ErrorMessage = "URL Target tối đa 10 ký tự!")]
        public string urlTarget { get; set; }
        public EN_BannerUrlType urlType { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập File URL")]
        [StringLength(200, ErrorMessage = "File URL tối đa 200 ký tự!")]
        public string fileUrl { get; set; }
        public EN_BannerFileType fileType { get; set; }
        [StringLength(10, ErrorMessage = "Trang tối đa 10 ký tự!")]
        public string page { get; set; }
        [StringLength(10, ErrorMessage = "Căn lề tối đa 10 ký tự!")]
        public string align { get; set; }
        public EN_BannerLocation location { get; set; }
        public DateTime? startAt { get; set; }
        public DateTime? endAt { get; set; }
        public bool isNeverExpired { get; set; }
        public int? sort { get; set; }
    }
    public enum EN_BannerFileType
    {
        Image,
        Youtube
    }
    public enum EN_BannerUrlType
    {
        Link,
        Youtube
    }
    public enum EN_BannerLocation
    {
        Popup,
        Top,
        Mid,
        Bottom,
    }
    public enum EN_BannerPage
    {
        home,
        product
    }
}
