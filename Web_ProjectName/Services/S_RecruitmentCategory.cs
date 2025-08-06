using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_RecruitmentCategory
    {
        Task<ResponseData<List<M_RecruitmentCategory>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId);
        Task<ResponseData<List<M_RecruitmentCategory>>> GetListBySupplierId(string supplierId);
        Task<ResponseData<M_RecruitmentCategory>> GetById(int id);
        Task<ResponseData<M_RecruitmentCategory>> Create(string accessToken, EM_RecruitmentCategory model, string createdBy);
        Task<ResponseData<M_RecruitmentCategory>> Update(string accessToken, EM_RecruitmentCategory model, string updatedBy);
        Task<ResponseData<M_RecruitmentCategory>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_RecruitmentCategory>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<List<M_RecruitmentCategory>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
    }
    public class S_RecruitmentCategory : IS_RecruitmentCategory
    {
        private readonly ICallBaseApi _callApi;
        public S_RecruitmentCategory(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_RecruitmentCategory>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_RecruitmentCategory>>("RecruitmentCategory/GetListBySequenceStatusSupplierId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_RecruitmentCategory>>> GetListBySupplierId(string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_RecruitmentCategory>>("RecruitmentCategory/GetListBySupplierId", dictPars);
        }
        public async Task<ResponseData<M_RecruitmentCategory>> GetById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_RecruitmentCategory>("RecruitmentCategory/GetById", dictPars);
        }
        public async Task<ResponseData<M_RecruitmentCategory>> Create(string accessToken, EM_RecruitmentCategory model, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", model.supplierId},
                {"parentId", model.parentId},
                {"name", model.name},
                {"remark", model.remark},
                {"sort", model.sort},
                {"status", model.status},
                {"createdBy", createdBy},
            };
            return await _callApi.PostResponseDataAsync<M_RecruitmentCategory>("RecruitmentCategory/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_RecruitmentCategory>> Update(string accessToken, EM_RecruitmentCategory model, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"supplierId", model.supplierId},
                {"parentId", model.parentId},
                {"name", model.name},
                {"remark", model.remark},
                {"sort", model.sort},
                {"status", model.status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_RecruitmentCategory>("RecruitmentCategory/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_RecruitmentCategory>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_RecruitmentCategory>("RecruitmentCategory/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_RecruitmentCategory>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_RecruitmentCategory>("RecruitmentCategory/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_RecruitmentCategory>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_RecruitmentCategory>>("RecruitmentCategory/UpdateStatusList", dictPars, accessToken);
        }
    }
}
