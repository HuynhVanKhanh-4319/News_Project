using Web_ProjectName.Models.Common;

namespace Web_ProjectName.Models
{
    public class M_AccountDetail : M_BaseModel.BaseCustom
    {
        public int? accountId { get; set; }
        public string firstNameSlug { get; set; }
        public string lastNameSlug { get; set; }
        public string qrCode { get; set; }
        public DateTime? birthday { get; set; }
        public int? gender { get; set; }
        public string idCard { get; set; }
        public string taxNumber { get; set; }
        public int? imageId { get; set; }
        public int? folkId { get; set; }
        public string remark { get; set; }
        public M_Image imageObj { get; set; }
        public M_Folk folkObj { get; set; }
    }
}
