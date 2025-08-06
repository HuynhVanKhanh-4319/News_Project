using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_NewsCategory
    {
        Task<ResponseData<List<M_NewsCategory>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId);
        Task<ResponseData<List<M_NewsCategory>>> GetListBySupplierId(string supplierId);
        Task<ResponseData<M_NewsCategory>> GetById(int id);
        Task<ResponseData<M_NewsCategory>> Create(string accessToken, EM_NewsCategory model, string createdBy);
        Task<ResponseData<M_NewsCategory>> Update(string accessToken, EM_NewsCategory model, string updatedBy);
        Task<ResponseData<M_NewsCategory>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_NewsCategory>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<List<M_NewsCategory>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
     
        Task<ResponseData<List<M_NewsCategory>>> GetListByStatus(string accessToken, int status);



    }
    public class S_NewsCategory : IS_NewsCategory
    {
        private readonly ICallBaseApi _callApi;
    
        public S_NewsCategory(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_NewsCategory>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_NewsCategory>>("NewsCategory/GetListBySequenceStatusSupplierId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_NewsCategory>>> GetListBySupplierId(string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_NewsCategory>>("NewsCategory/GetListBySupplierId", dictPars);
        }
        public async Task<ResponseData<M_NewsCategory>> GetById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_NewsCategory>("NewsCategory/GetById", dictPars);
        }
        public async Task<ResponseData<M_NewsCategory>> Create(string accessToken, EM_NewsCategory model, string createdBy)
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
            return await _callApi.PostResponseDataAsync<M_NewsCategory>("NewsCategory/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_NewsCategory>> Update(string accessToken, EM_NewsCategory model, string updatedBy)
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
            return await _callApi.PutResponseDataAsync<M_NewsCategory>("NewsCategory/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_NewsCategory>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_NewsCategory>("NewsCategory/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_NewsCategory>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_NewsCategory>("NewsCategory/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_NewsCategory>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_NewsCategory>>("NewsCategory/UpdateStatusList", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_NewsCategory>>> GetListByStatus(string accessToken, int status)
        {
            var pars = new Dictionary<string, dynamic>
            {
                        { "status", status }
            };

            return await _callApi.GetResponseDataAsync<List<M_NewsCategory>>("NewsCategory/GetListByStatus", pars, accessToken);
        }

    }
}
