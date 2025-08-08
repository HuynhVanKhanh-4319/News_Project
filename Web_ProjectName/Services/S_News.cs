using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using static System.String;

namespace Web_ProjectName.Services
{
    public interface IS_News
    {
        Task<ResponseData<List<M_News>>> GetListBySequenceStatusSupplierIdNewsCategoryId(string accessToken, string sequenceStatus, string supplierId, int? newsCategoryId, DateTime? fdate, DateTime? tdate);
        Task<ResponseData<List<M_News>>> GetListBySequenceStatusSupplierIdSearchText(string accessToken, string sequenceStatus, string supplierId, string searchText);
        Task<ResponseData<List<M_News>>> GetListByPaging(string sequenceStatus, string supplierId, int? newsCategoryId, string keyword, int page = 1, int record = 10);
        Task<ResponseData<M_News>> GetById( int id);
        Task<ResponseData<M_News>> GetByMetaUrl(string metaUrl);
        Task<ResponseData<M_News>> Create(string accessToken, EM_News model, string createdBy);
        Task<ResponseData<M_News>> Update(string accessToken, EM_News model, string updatedBy);
        Task<ResponseData<M_News>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_News>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<List<M_News>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
        Task<ResponseData<List<M_News>>> GetListByStatus(string accessToken, int status);
    }
    public class S_News : IS_News
    {
        private readonly ICallBaseApi _callApi;
        private readonly IS_Image _s_Image;
        public S_News(ICallBaseApi callApi, IS_Image image)
        {
            _callApi = callApi;
            _s_Image = image;
        }

        public async Task<ResponseData<List<M_News>>> GetListBySequenceStatusSupplierIdNewsCategoryId(string accessToken, string sequenceStatus, string supplierId, int? newsCategoryId, DateTime? fdate, DateTime? tdate)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"newsCategoryId", newsCategoryId},
                {"fdate", fdate?.ToString("O")},
                {"tdate", tdate?.ToString("O")},
            };
            return await _callApi.GetResponseDataAsync<List<M_News>>("News/GetListBySequenceStatusSupplierIdNewsCategoryId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_News>>> GetListBySequenceStatusSupplierIdSearchText(string accessToken, string sequenceStatus, string supplierId, string searchText)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"searchText", searchText},
            };
            return await _callApi.GetResponseDataAsync<List<M_News>>("News/GetListBySequenceStatusSupplierIdSearchText", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_News>>> GetListByPaging(string sequenceStatus, string supplierId, int? newsCategoryId, string keyword, int page = 1, int record = 10)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"newsCategoryId", newsCategoryId ?? 0},
                {"keyword", keyword},
                {"page", page},
                {"record", record},
            };
            return await _callApi.GetResponseDataAsync<List<M_News>>("News/GetListByPaging", dictPars);
        }
        public async Task<ResponseData<M_News>> GetById( int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_News>("News/GetById", dictPars);
        }
        public async Task<ResponseData<M_News>> GetByMetaUrl(string metaUrl)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"metaUrl", metaUrl},
            };
            return await _callApi.GetResponseDataAsync<M_News>("News/GetByMetaUrl", dictPars);
        }
        public async Task<ResponseData<M_News>> Create(string accessToken, EM_News model, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS

            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"name", model.name},
                {"newsCategoryId", model.newsCategoryId},
                {"supplierId", model.supplierId},
                {"description", model.description},
                {"detail", model.detail},
                {"isHot", model.isHot},
                {"viewNumber", model.viewNumber},
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
            return await _callApi.PostResponseDataAsync<M_News>("News/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_News>> Update(string accessToken, EM_News model, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
         
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"name", model.name},
                {"newsCategoryId", model.newsCategoryId},
                {"supplierId", model.supplierId},
                {"description", model.description},
                {"detail", model.detail},
                {"isHot", model.isHot},
                {"viewNumber", model.viewNumber},
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
            return await _callApi.PutResponseDataAsync<M_News>("News/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_News>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_News>("News/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_News>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_News>("News/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_News>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_News>>("News/UpdateStatusList", dictPars, accessToken);
        }

        public Task<ResponseData<List<M_News>>> GetListByStatus(string accessToken, int status)
        {
            var pars = new Dictionary<string, dynamic>
            {
                        { "status", status }
            };
            return _callApi.GetResponseDataAsync<List<M_News>>("News/GetListByStatus", pars, accessToken);
        }
    }
}
