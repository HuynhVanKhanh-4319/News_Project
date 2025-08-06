using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_Introduce
    {
        Task<ResponseData<List<M_Introduce>>> GetListBySequenceStatusSupplierIdIntroduceType(string accessToken, string sequenceStatus, string supplierId, EN_IntroduceType type);
        Task<ResponseData<List<M_Introduce>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId);
        Task<ResponseData<List<M_Introduce>>> GetListBySequenceStatusSupplierIdSearchText(string accessToken, string sequenceStatus, string supplierId, string searchText);
        Task<ResponseData<List<M_Introduce>>> GetListByPaging(string accessToken, string sequenceStatus, string supplierId, EN_IntroduceType type, int page = 1, int record = 10);
        Task<ResponseData<M_Introduce>> GetById(string accessToken, int id);
        Task<ResponseData<M_Introduce>> GetByMetaUrl(string metaUrl);
        Task<ResponseData<M_Introduce>> Create(string accessToken, EM_Introduce model, string refCode, string createdBy);
        Task<ResponseData<M_Introduce>> Update(string accessToken, EM_Introduce model, string refCode, string updatedBy);
        Task<ResponseData<M_Introduce>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_Introduce>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<M_Introduce>> UpdateCatalogueId(string accessToken, int id, int catalogueId, string updatedBy);
        Task<ResponseData<List<M_Introduce>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
    }
    public class S_Introduce : IS_Introduce
    {
        private readonly ICallBaseApi _callApi;
        private readonly IS_Image _s_Image;
        public S_Introduce(ICallBaseApi callApi, IS_Image image)
        {
            _callApi = callApi;
            _s_Image = image;
        }

        public async Task<ResponseData<List<M_Introduce>>> GetListBySequenceStatusSupplierIdIntroduceType(string accessToken, string sequenceStatus, string supplierId, EN_IntroduceType type)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"type", (int)type},
            };
            return await _callApi.GetResponseDataAsync<List<M_Introduce>>("Introduce/GetListBySequenceStatusSupplierIdIntroduceType", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Introduce>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_Introduce>>("Introduce/GetListBySequenceStatusSupplierId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Introduce>>> GetListBySequenceStatusSupplierIdSearchText(string accessToken, string sequenceStatus, string supplierId, string searchText)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"searchText", searchText},
            };
            return await _callApi.GetResponseDataAsync<List<M_Introduce>>("Introduce/GetListBySequenceStatusSupplierIdSearchText", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Introduce>>> GetListByPaging(string accessToken, string sequenceStatus, string supplierId, EN_IntroduceType type, int page = 1, int record = 10)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"type", (int)type},
                {"page", page},
                {"record", record},
            };
            return await _callApi.GetResponseDataAsync<List<M_Introduce>>("Introduce/GetListByPaging", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Introduce>> GetById(string accessToken, int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Introduce>("Introduce/GetById", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Introduce>> GetByMetaUrl(string metaUrl)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"metaUrl", metaUrl},
            };
            return await _callApi.GetResponseDataAsync<M_Introduce>("Introduce/GetByMetaUrl", dictPars);
        }
        public async Task<ResponseData<M_Introduce>> Create(string accessToken, EM_Introduce model, string refCode, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            //ResponseData<M_Introduce> res = new ResponseData<M_Introduce>();
            //if (model.imageFile != null)
            //{
            //    var imgUpload = await _s_Image.UploadResize(model.imageFile, refCode, Empty, createdBy);
            //    if (imgUpload.result == 1 && imgUpload.data != null)
            //    {
            //        model.imageId = imgUpload.data.id;
            //    }
            //    else
            //    {
            //        res.result = imgUpload.result; res.error = imgUpload.error;
            //        return res;
            //    }
            //}
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", model.supplierId},
                {"type", (int)model.type},
                {"name", model.name},
                {"description", model.description},
                {"detail", model.detail},
                {"sort", model.sort},
                {"isHot", model.isHot},
                {"viewNumber", model.viewNumber},
                {"catalogueId", model.catalogueId},
                {"imageId", model.imageId},
                {"publishedAt", model.publishedAt.ToString("O")},
                {"metaKeywords", model.metaKeywords},
                {"metaDescription", model.metaDescription},
                {"metaTitle", model.metaTitle},
                {"metaUrl", model.metaUrl},
                {"metaImagePreview", model.metaImagePreview},
                {"status", model.status},
                {"createdBy", createdBy},
            };
            return await _callApi.PostResponseDataAsync<M_Introduce>("Introduce/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Introduce>> Update(string accessToken, EM_Introduce model, string refCode, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            //ResponseData<M_Introduce> res = new ResponseData<M_Introduce>();
            //if (model.imageFile != null)
            //{
            //    var imgUpload = await _s_Image.UploadResize(model.imageFile, refCode, Empty, updatedBy);
            //    if (imgUpload.result == 1 && imgUpload.data != null)
            //    {
            //        model.imageId = imgUpload.data.id;
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
                {"supplierId", model.supplierId},
                {"type", (int)model.type},
                {"name", model.name},
                {"description", model.description},
                {"detail", model.detail},
                {"sort", model.sort},
                {"isHot", model.isHot},
                {"viewNumber", model.viewNumber},
                {"catalogueId", model.catalogueId},
                {"imageId", model.imageId},
                {"publishedAt", model.publishedAt.ToString("O")},
                {"metaKeywords", model.metaKeywords},
                {"metaDescription", model.metaDescription},
                {"metaTitle", model.metaTitle},
                {"metaUrl", model.metaUrl},
                {"metaImagePreview", model.metaImagePreview},
                {"status", model.status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Introduce>("Introduce/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Introduce>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_Introduce>("Introduce/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Introduce>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Introduce>("Introduce/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Introduce>> UpdateCatalogueId(string accessToken, int id, int catalogueId, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"catalogueId", catalogueId},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Introduce>("Introduce/UpdateCatalogueId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Introduce>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_Introduce>>("Introduce/UpdateStatusList", dictPars, accessToken);
        }
    }
}
