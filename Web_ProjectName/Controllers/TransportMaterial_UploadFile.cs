using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Web_ProjectName.Controllers;
using Web_ProjectName.ExtensionMethods;
using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using Web_ProjectName.Services;

public class TransportMaterial_UploadFile : BaseController<TransportMaterial_UploadFile>
{
    private readonly IS_TransportMaterial _s_TransportMaterial;
    public TransportMaterial_UploadFile(IS_TransportMaterial s_TransportMaterial)
    {
        _s_TransportMaterial = s_TransportMaterial;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public JsonResult ReadExcelTransportMaterial(EM_TransportMaterial_UploadFile model, string isEudr)
    {
        var jResult = new M_JResult();
        try
        {
            if (!ModelState.IsValid)
            {
                jResult.error = new error(0, DataAnnotationExtensionMethod.GetErrorMessage(ModelState));
                return Json(jResult);
            }

            bool isEudrValue = isEudr == "true";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var data = new List<M_TransportMaterial_CreateListPurchase.Item>();

            using (var stream = new MemoryStream())
            {
                model.file.CopyTo(stream);
                stream.Position = 0;
                int startRow = 4;

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        if (reader.Depth < startRow || IsRowEmpty(reader))
                            continue;

                        string farmerCode = reader.GetValue(2)?.ToString();
                        if (string.IsNullOrEmpty(farmerCode))
                            break;

                        string licensePlate = reader.GetValue(3)?.ToString();
                        DateTime? excelDate = null;
                        object cellValue = reader.GetValue(4);

                        if (cellValue != null)
                        {
                            if (cellValue is DateTime dt)
                                excelDate = dt;
                            else if (double.TryParse(cellValue.ToString(), out double oaDate))
                                excelDate = DateTime.FromOADate(oaDate);
                            else if (DateTime.TryParse(cellValue.ToString(), out DateTime parsed))
                                excelDate = parsed;
                        }

                        string farmerName = reader.GetValue(5)?.ToString();
                        float factoryWaterWeight = float.TryParse(reader.GetValue(6)?.ToString(), out float fww) ? fww : 0f;
                        float factoryWaterTsc = float.TryParse(reader.GetValue(7)?.ToString(), out float fwt) ? fwt : 0f;

                        data.Add(new M_TransportMaterial_CreateListPurchase.Item
                        {
                            FarmerCode = farmerCode,
                            FarmerName = farmerName,
                            LicensePlate = licensePlate,
                            Day = excelDate,
                            FactoryWaterWeight = factoryWaterWeight,
                            FactoryWaterTsc = factoryWaterTsc,
                            IsEudr = isEudrValue
                        });
                    }
                }
            }

            jResult.result = 1;
            jResult.data = data.Select(x => new
            {
                licensePlate = x.LicensePlate,
                materialPoolAreaId = 1,
                isEudr = x.IsEudr,
                farmerCode = x.FarmerCode,
                day = x.Day?.ToString("yyyy/MM/dd"), 
                factoryWaterTsc = x.FactoryWaterTsc,
                factoryWaterDry = x.FactoryWaterDry,
                factoryWaterPh = x.FactoryWaterPh,
                factoryWaterWeight = x.FactoryWaterWeight,
                factoryWeightCup = x.FactoryWeightCup,
                factoryWeightSolidify = x.FactoryWeightSolidify,
                factoryWeightWire = x.FactoryWeightWire,
                placeMarkIdObjs = new List<object>(),
                farmerChildObjs = new List<object>()
            }).ToList();
        }
        catch (Exception ex)
        {
            jResult.result = -1;
            jResult.error = new error(500, $"Dữ liệu không hợp lệ. Chi tiết lỗi: {ex.Message}");
        }
        return Json(jResult);
    }



    private bool IsRowEmpty(IExcelDataReader reader)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetValue(i) != null && !string.IsNullOrWhiteSpace(reader.GetValue(i).ToString()))
            {
                return false;
            }
        }
        return true;
    }
}
