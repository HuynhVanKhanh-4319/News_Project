using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_PartnerList
    {
        Task<ResponseData<List<M_PartnerList>>> GetListBySequenceStatusSupplierId(string sequenceStatus, string supplierId);
        Task<ResponseData<M_PartnerList>> GetById(string accessToken, int id);
        Task<ResponseData<M_PartnerList>> Create(string accessToken, EM_PartnerList model, string createdBy);
        Task<ResponseData<M_PartnerList>> Update(string accessToken, EM_PartnerList model, string updatedBy);
        Task<ResponseData<M_PartnerList>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_PartnerList>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<List<M_PartnerList>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
        Task<ResponseData<List<M_PartnerList>>> UpdateSortList(string accessToken, string sequenceIds, string updatedBy);
    }
    public class S_PartnerList : IS_PartnerList
    {
        private readonly ICallBaseApi _callApi;
        public S_PartnerList(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_PartnerList>>> GetListBySequenceStatusSupplierId(string sequenceStatus, string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_PartnerList>>("PartnerList/GetListBySequenceStatusSupplierId", dictPars);
        }
        public async Task<ResponseData<M_PartnerList>> GetById(string accessToken, int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_PartnerList>("PartnerList/GetById", dictPars, accessToken);
        }
        public async Task<ResponseData<M_PartnerList>> Create(string accessToken, EM_PartnerList model, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", model.supplierId},
                {"title", model.title},
                {"remark", model.remark},
                {"url", model.url},
                {"imageUrl", model.imageUrl},
                {"sort", model.sort},
                {"status", model.status},
                {"createdBy", createdBy},
            };
            return await _callApi.PostResponseDataAsync<M_PartnerList>("PartnerList/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_PartnerList>> Update(string accessToken, EM_PartnerList model, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"supplierId", model.supplierId},
                {"title", model.title},
                {"remark", model.remark},
                {"url", model.url},
                {"imageUrl", model.imageUrl},
                {"sort", model.sort},
                {"status", model.status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_PartnerList>("PartnerList/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_PartnerList>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_PartnerList>("PartnerList/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_PartnerList>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_PartnerList>("PartnerList/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_PartnerList>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_PartnerList>>("PartnerList/UpdateStatusList", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_PartnerList>>> UpdateSortList(string accessToken, string sequenceIds, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_PartnerList>>("PartnerList/UpdateSortList", dictPars, accessToken);
        }
    }
}
