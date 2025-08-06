using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_Product
    {
        Task<ResponseData<List<M_Product>>> GetListBySequenceStatusSupplierIdProductCategoryId(string sequenceStatus, string sequenceIsHot, string supplierId, int? productCategoryId);
        Task<ResponseData<List<M_Product>>> GetListBySequenceStatusSupplierIdSearchText(string accessToken, string sequenceStatus, string supplierId, string searchText);
        Task<ResponseData<List<M_Product>>> GetListByPaging(string sequenceStatus, string supplierId, string sequenceProductCategoryId, string searchText, EN_TypeSearchProduct typeSearch, int page = 1, int record = 10);
        Task<ResponseData<M_Product>> GetById(string accessToken, int id);
        Task<ResponseData<M_Product>> GetByMetaUrl(string metaUrl);
        Task<ResponseData<M_Product>> Create(string accessToken, EM_Product model, string refCode, string createdBy);
        Task<ResponseData<M_Product>> Update(string accessToken, EM_Product model, string refCode, string updatedBy);
        Task<ResponseData<M_Product>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_Product>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<M_Product>> UpdateCatalogueId(string accessToken, int id, int catalogueId, string updatedBy);
        Task<ResponseData<List<M_Product>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
    }
    public class S_Product : IS_Product
    {
        private readonly ICallBaseApi _callApi;
        private readonly IS_Image _s_Image;
        public S_Product(ICallBaseApi callApi, IS_Image image)
        {
            _callApi = callApi;
            _s_Image = image;
        }

        public async Task<ResponseData<List<M_Product>>> GetListBySequenceStatusSupplierIdProductCategoryId(string sequenceStatus, string sequenceIsHot, string supplierId, int? productCategoryId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"sequenceIsHot", sequenceIsHot},
                {"supplierId", supplierId},
                {"productCategoryId", productCategoryId},
            };
            return await _callApi.GetResponseDataAsync<List<M_Product>>("Product/GetListBySequenceStatusSupplierIdProductCategoryId", dictPars);
        }
        public async Task<ResponseData<List<M_Product>>> GetListBySequenceStatusSupplierIdSearchText(string accessToken, string sequenceStatus, string supplierId, string searchText)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"searchText", searchText},
            };
            return await _callApi.GetResponseDataAsync<List<M_Product>>("Product/GetListBySequenceStatusSupplierIdSearchText", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Product>>> GetListByPaging(string sequenceStatus, string supplierId, string sequenceProductCategoryId, string searchText, EN_TypeSearchProduct typeSearch, int page = 1, int record = 10)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"sequenceProductCategoryId", sequenceProductCategoryId},
                {"searchText", searchText},
                {"typeSearch", (int)typeSearch},
                {"page", page},
                {"record", record},
            };
            return await _callApi.GetResponseDataAsync<List<M_Product>>("Product/GetListByPaging", dictPars);
        }
        public async Task<ResponseData<M_Product>> GetById(string accessToken, int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Product>("Product/GetById", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Product>> GetByMetaUrl(string metaUrl)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"metaUrl", metaUrl},
            };
            return await _callApi.GetResponseDataAsync<M_Product>("Product/GetByMetaUrl", dictPars);
        }
        public async Task<ResponseData<M_Product>> Create(string accessToken, EM_Product model, string refCode, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
     
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"name", model.name},
                {"productCode", model.productCode},
                {"productCategoryId", model.productCategoryId},
                {"qrCode", model.qrCode},
                {"supplierId", model.supplierId},
                {"description", model.description},
                {"detail", model.detail},
                {"isHot", model.isHot},
                {"viewNumber", model.viewNumber},
                {"price", model.price},
                {"discount", model.discount},
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
            };
            return await _callApi.PostResponseDataAsync<M_Product>("Product/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Product>> Update(string accessToken, EM_Product model, string refCode, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS

            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"name", model.name},
                {"productCode", model.productCode},
                {"productCategoryId", model.productCategoryId},
                {"qrCode", model.qrCode},
                {"supplierId", model.supplierId},
                {"description", model.description},
                {"detail", model.detail},
                {"isHot", model.isHot},
                {"viewNumber", model.viewNumber},
                {"price", model.price},
                {"discount", model.discount},
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
            };
            return await _callApi.PutResponseDataAsync<M_Product>("Product/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Product>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_Product>("Product/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Product>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Product>("Product/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Product>> UpdateCatalogueId(string accessToken, int id, int catalogueId, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"catalogueId", catalogueId},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Product>("Product/UpdateCatalogueId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Product>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_Product>>("Product/UpdateStatusList", dictPars, accessToken);
        }
    }
}
