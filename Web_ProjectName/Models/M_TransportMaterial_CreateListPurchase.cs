using static Web_ProjectName.ExtensionMethods.ValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace Web_ProjectName.Models
{
    public class M_TransportMaterial_CreateListPurchase
    {
        public byte Type { get; set; }
        public byte TypeCreate { get; set; }
        public int FactoryId { get; set; }
        public DateOnly StartDate { get; set; }
        public short Status { get; set; }
        public int CreatedBy { get; set; }
        public int? IdentificationCriterionId { get; set; }

        public float? FactoryDrcCup { get; set; }
        public float? FactoryDrcCupInfected { get; set; }
        public float? FactoryDrcWire { get; set; }
        public float? FactoryDrcSolidify { get; set; }

        public string JsonItem { get; set; }
        public class Item
        {
            public string FarmerCode { get; set; }
            public string FarmerName { get; set; }
            public string LicensePlate { get; set; }
            public DateTime? Day { get; set; }
            public string Remark { get; set; }
            public TimeOnly? TimeStart { get; set; }
            public TimeOnly? TimeFarm { get; set; }
            public TimeOnly? TimeFinish { get; set; }

            public float? FactoryWaterTsc { get; set; }
            public float? FactoryWaterDrc { get; set; }
            public float? FactoryWaterDry { get; set; }
            public float? FactoryWaterPh { get; set; }
            public float? FactoryWaterWeight { get; set; }
            public float? FactoryWeightCup { get; set; }
            public float? FactoryWeightCupInfected { get; set; }
            public float? FactoryWeightWire { get; set; }
            public float? FactoryWeightWireInfected { get; set; }
            public float? FactoryWeightSolidify { get; set; }
            public float? FactoryWeightSolidifyInfected { get; set; }
            public float? FactoryWeightSolidifyTank { get; set; }
            public float? FactoryDrcCup { get; set; }
            public float? FactoryDrcCupInfected { get; set; }
            public float? FactoryDrcWire { get; set; }
            public float? FactoryDrcWireInfected { get; set; }
            public float? FactoryDrcSolidify { get; set; }
            public float? FactoryDrcSolidifyInfected { get; set; }
            public float? FactoryDrcSolidifyTank { get; set; }
            public float? FactoryDryCup { get; set; }
            public float? FactoryDryCupInfected { get; set; }
            public float? FactoryDryWire { get; set; }
            public float? FactoryDryWireInfected { get; set; }
            public float? FactoryDrySolidify { get; set; }
            public float? FactoryDrySolidifyInfected { get; set; }
            public float? FactoryDrySolidifyTank { get; set; }
            public bool? IsEudr { get; set; }
        }
    }
    public class EM_TransportMaterial_UploadFile
    {
        [Required(ErrorMessage = "Vui lòng chọn tệp!")]
        [DataType(DataType.Upload)]
        [MaxFileSize(maxFileSize: 10 * 1024 * 1024, errorMessage: "Dung lượng tệp tối đa 10MB!")]
        [AllowedExtensions(extensions: new string[] { ".xlsx", ".xls" }, errorMessage: "Tệp không hợp lệ!")]
        public IFormFile file { get; set; }
    }
}
