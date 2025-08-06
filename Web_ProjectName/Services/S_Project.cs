using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_Project
    {
        Task<ResponseData<List<M_Project>>> GetListBySequenceStatusSupplierIdProjectCategoryIdSequenceProcessStatus(string sequenceStatus, string sequenceIsHot, string supplierId, int? projectCategoryId, string sequenceProcessStatus = "0,1,2");
        Task<ResponseData<List<M_Project>>> GetListBySequenceStatusSupplierIdSearchText(string accessToken, string sequenceStatus, string supplierId, string searchText);
        Task<ResponseData<List<M_Project>>> GetListByPaging(string sequenceStatus, string supplierId, string sequenceProjectCategoryId, string searchText, int page = 1, int record = 10);
        Task<ResponseData<M_Project>> GetById(string accessToken, int id);
        Task<ResponseData<M_Project>> GetByMetaUrl(string metaUrl);
        Task<ResponseData<M_Project>> Create(string accessToken, EM_Project model, string refCode, string createdBy);
        Task<ResponseData<M_Project>> Update(string accessToken, EM_Project model, string refCode, string updatedBy);
        Task<ResponseData<M_Project>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_Project>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<M_Project>> UpdateCatalogueId(string accessToken, int id, int catalogueId, string updatedBy);
        Task<ResponseData<List<M_Project>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
    }
    public class S_Project : IS_Project
    {
        private readonly ICallBaseApi _callApi;
        private readonly IS_Image _s_Image;
        public S_Project(ICallBaseApi callApi, IS_Image image)
        {
            _callApi = callApi;
            _s_Image = image;
        }

        public async Task<ResponseData<List<M_Project>>> GetListBySequenceStatusSupplierIdProjectCategoryIdSequenceProcessStatus(string sequenceStatus, string sequenceIsHot, string supplierId, int? projectCategoryId, string sequenceProcessStatus = "0,1,2")
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"sequenceIsHot", sequenceIsHot},
                {"supplierId", supplierId},
                {"projectCategoryId", projectCategoryId},
                {"sequenceProcessStatus", sequenceProcessStatus},
            };
            return await _callApi.GetResponseDataAsync<List<M_Project>>("Project/GetListBySequenceStatusSupplierIdProjectCategoryIdSequenceProcessStatus", dictPars);
        }
        public async Task<ResponseData<List<M_Project>>> GetListBySequenceStatusSupplierIdSearchText(string accessToken, string sequenceStatus, string supplierId, string searchText)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"searchText", searchText},
            };
            return await _callApi.GetResponseDataAsync<List<M_Project>>("Project/GetListBySequenceStatusSupplierIdSearchText", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Project>>> GetListByPaging(string sequenceStatus, string supplierId, string sequenceProjectCategoryId, string searchText, int page = 1, int record = 10)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"sequenceProjectCategoryId", sequenceProjectCategoryId},
                {"searchText", searchText},
                {"page", page},
                {"record", record},
            };
            return await _callApi.GetResponseDataAsync<List<M_Project>>("Project/GetListByPaging", dictPars);
        }
        public async Task<ResponseData<M_Project>> GetById(string accessToken, int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Project>("Project/GetById", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Project>> GetByMetaUrl(string metaUrl)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"metaUrl", metaUrl},
            };
            return await _callApi.GetResponseDataAsync<M_Project>("Project/GetByMetaUrl", dictPars);
        }
        public async Task<ResponseData<M_Project>> Create(string accessToken, EM_Project model, string refCode, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            //ResponseData<M_Project> res = new ResponseData<M_Project>();
            //if (model.imageFile != null)
            //{
            //    var imgUpload = await _s_Image.UploadResize(model.imageFile, refCode, Empty, createdBy);
            //    if (imgUpload.result == 1 && imgUpload.data != null)
            //    {
            //        model.imageId = imgUpload.data.id;
            //        model.imageSerialId = imgUpload.data.serialId;
            //    }
            //    else
            //    {
            //        res.result = imgUpload.result; res.error = imgUpload.error;
            //        return res;
            //    }
            //}
            //if (model.listImageFile != null && model.listImageFile.Count > 0)
            //{
            //    var imgUpload = await _s_Image.UploadListImageIFormFile(model.listImageFile, refCode, Empty, Empty, createdBy);
            //    if (imgUpload.result == 1 && imgUpload.data != null)
            //    {
            //        model.imageList = JsonConvert.SerializeObject(imgUpload.data.Select(x => x.id));
            //        model.imageSerialList = JsonConvert.SerializeObject(imgUpload.data.Select(x => x.serialId));
            //    }
            //    else
            //    {
            //        res.result = imgUpload.result; res.error = imgUpload.error;
            //        return res;
            //    }
            //}
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"name", model.name},
                {"projectCode", model.projectCode},
                {"projectCategoryId", model.projectCategoryId},
                {"qrCode", model.qrCode},
                {"supplierId", model.supplierId},
                {"description", model.description},
                {"detail", model.detail},
                {"isHot", model.isHot},
                {"viewNumber", model.viewNumber},
                {"investor", model.investor},
                {"addressId", model.addressId},
                {"startDate", model.startDate?.ToString("O")},
                {"endDate", model.endDate?.ToString("O")},
                {"year", model.year},
                {"processStatus", model.processStatus},
                {"catalogueId", model.catalogueId},
                {"imageId", model.imageId},
                {"imageList", model.imageList},
                {"videoUrl", model.videoUrl},
                {"metaKeywords", model.metaKeywords},
                {"metaDescription", model.metaDescription},
                {"metaTitle", model.metaTitle},
                {"metaUrl", model.metaUrl},
                {"metaImagePreview", model.metaImagePreview},
                {"status", model.status},
                {"createdBy", createdBy},
                {"address.addressText", model.addressObj.addressText},
                {"address.countryId", model.addressObj.countryId},
                {"address.provinceId", model.addressObj.provinceId},
                {"address.districtId", model.addressObj.districtId},
                {"address.wardId", model.addressObj.wardId},
                {"address.latitude", model.addressObj.latitude},
                {"address.longitude", model.addressObj.longitude},
            };
            return await _callApi.PostResponseDataAsync<M_Project>("Project/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Project>> Update(string accessToken, EM_Project model, string refCode, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            //ResponseData<M_Project> res = new ResponseData<M_Project>();
            //if (model.imageFile != null)
            //{
            //    var imgUpload = await _s_Image.UploadResize(model.imageFile, refCode, Empty, updatedBy);
            //    if (imgUpload.result == 1 && imgUpload.data != null)
            //    {
            //        model.imageId = imgUpload.data.id;
            //        model.imageSerialId = imgUpload.data.serialId;
            //    }
            //    else
            //    {
            //        res.result = imgUpload.result; res.error = imgUpload.error;
            //        return res;
            //    }
            //}
            //if (model.listImageFile != null && model.listImageFile.Count > 0)
            //{
            //    var imgUpload = await _s_Image.UploadListImageIFormFile(model.listImageFile, refCode, Empty, Empty, updatedBy);
            //    if (imgUpload.result == 1 && imgUpload.data != null)
            //    {
            //        model.imageList = JsonConvert.SerializeObject(imgUpload.data.Select(x => x.id));
            //        model.imageSerialList = JsonConvert.SerializeObject(imgUpload.data.Select(x => x.serialId));
            //    }
            //    else
            //    {
            //        res.result = imgUpload.result; res.error = imgUpload.error;
            //        return res;
            //    }
            //}
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"name", model.name},
                {"projectCode", model.projectCode},
                {"projectCategoryId", model.projectCategoryId},
                {"qrCode", model.qrCode},
                {"supplierId", model.supplierId},
                {"description", model.description},
                {"detail", model.detail},
                {"isHot", model.isHot},
                {"viewNumber", model.viewNumber},
                {"investor", model.investor},
                {"addressId", model.addressId},
                {"startDate", model.startDate?.ToString("O")},
                {"endDate", model.endDate?.ToString("O")},
                {"year", model.year},
                {"processStatus", model.processStatus},
                {"catalogueId", model.catalogueId},
                {"imageId", model.imageId},
                {"imageList", model.imageList},
                {"videoUrl", model.videoUrl},
                {"metaKeywords", model.metaKeywords},
                {"metaDescription", model.metaDescription},
                {"metaTitle", model.metaTitle},
                {"metaUrl", model.metaUrl},
                {"metaImagePreview", model.metaImagePreview},
                {"status", model.status},
                {"updatedBy", updatedBy},
                {"address.addressText", model.addressObj.addressText},
                {"address.countryId", model.addressObj.countryId},
                {"address.provinceId", model.addressObj.provinceId},
                {"address.districtId", model.addressObj.districtId},
                {"address.wardId", model.addressObj.wardId},
                {"address.latitude", model.addressObj.latitude},
                {"address.longitude", model.addressObj.longitude},
            };
            return await _callApi.PutResponseDataAsync<M_Project>("Project/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Project>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_Project>("Project/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Project>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Project>("Project/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Project>> UpdateCatalogueId(string accessToken, int id, int catalogueId, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"catalogueId", catalogueId},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Project>("Project/UpdateCatalogueId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Project>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_Project>>("Project/UpdateStatusList", dictPars, accessToken);
        }
    }
}
