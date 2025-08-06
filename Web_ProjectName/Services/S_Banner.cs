using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_Banner
    {
        Task<ResponseData<List<M_Banner>>> GetListBySequenceStatusSupplierIdLocationPage(string accessToken, string sequenceStatus, string supplierId, EN_BannerLocation location, string page);
        Task<ResponseData<M_Banner_CustomFullLocation>> GetListBySupplierIdPage(string supplierId, string page);
        Task<ResponseData<M_Banner>> GetById(string accessToken, int id);
        Task<ResponseData<M_Banner>> Create(string accessToken, EM_Banner model, string createdBy);
        Task<ResponseData<M_Banner>> Update(string accessToken, EM_Banner model, string updatedBy);
        Task<ResponseData<M_Banner>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_Banner>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<List<M_Banner>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
        Task<ResponseData<List<M_Banner>>> UpdateSortList(string accessToken, string sequenceIds, string updatedBy);
    }
    public class S_Banner : IS_Banner
    {
        private readonly ICallBaseApi _callApi;
        public S_Banner(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_Banner>>> GetListBySequenceStatusSupplierIdLocationPage(string accessToken, string sequenceStatus, string supplierId, EN_BannerLocation location, string page)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"location", (int)location},
                {"page", page},
            };
            return await _callApi.GetResponseDataAsync<List<M_Banner>>("Banner/GetListBySequenceStatusSupplierIdLocationPage", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Banner_CustomFullLocation>> GetListBySupplierIdPage(string supplierId, string page)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", supplierId},
                {"page", page},
            };
            return await _callApi.GetResponseDataAsync<M_Banner_CustomFullLocation>("Banner/GetListBySupplierIdPage", dictPars);
        }
        public async Task<ResponseData<M_Banner>> GetById(string accessToken, int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Banner>("Banner/GetById", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Banner>> Create(string accessToken, EM_Banner model, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", model.supplierId},
                {"title", model.title},
                {"description", model.description},
                {"url", model.url},
                {"urlTarget", model.urlTarget},
                {"urlType", (int)model.urlType},
                {"fileUrl", model.fileUrl},
                {"fileType", (int)model.fileType},
                {"page", model.page},
                {"align", model.align},
                {"location", (int)model.location},
                {"startAt", model.startAt?.ToString("O")},
                {"endAt", model.endAt?.ToString("O")},
                {"isNeverExpired", model.isNeverExpired},
                {"sort", model.sort},
                {"status", model.status},
                {"createdBy", createdBy},
            };
            return await _callApi.PostResponseDataAsync<M_Banner>("Banner/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Banner>> Update(string accessToken, EM_Banner model, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"supplierId", model.supplierId},
                {"title", model.title},
                {"description", model.description},
                {"url", model.url},
                {"urlTarget", model.urlTarget},
                {"urlType", (int)model.urlType},
                {"fileUrl", model.fileUrl},
                {"fileType", (int)model.fileType},
                {"page", model.page},
                {"align", model.align},
                {"location", (int)model.location},
                {"startAt", model.startAt?.ToString("O")},
                {"endAt", model.endAt?.ToString("O")},
                {"isNeverExpired", model.isNeverExpired},
                {"sort", model.sort},
                {"status", model.status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Banner>("Banner/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Banner>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_Banner>("Banner/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Banner>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Banner>("Banner/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Banner>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_Banner>>("Banner/UpdateStatusList", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Banner>>> UpdateSortList(string accessToken, string sequenceIds, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_Banner>>("Banner/UpdateSortList", dictPars, accessToken);
        }
    }
}
