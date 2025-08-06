using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_ProjectCategory
    {
        Task<ResponseData<List<M_ProjectCategory>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId);
        Task<ResponseData<List<M_ProjectCategory>>> GetListBySequenceStatusSupplierIdParentId(string accessToken, string sequenceStatus, string supplierId, int parentId);
        Task<ResponseData<List<M_ProjectCategory>>> GetListBySupplierId(string supplierId);
        Task<ResponseData<List<M_ProjectCategory>>> GetListMenuBySupplierId(string supplierId);
        Task<ResponseData<M_ProjectCategory>> GetById(int id);
        Task<ResponseData<M_ProjectCategory>> Create(string accessToken, EM_ProjectCategory model, string createdBy);
        Task<ResponseData<M_ProjectCategory>> Update(string accessToken, EM_ProjectCategory model, string updatedBy);
        Task<ResponseData<M_ProjectCategory>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_ProjectCategory>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<List<M_ProjectCategory>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
    }
    public class S_ProjectCategory : IS_ProjectCategory
    {
        private readonly ICallBaseApi _callApi;
        public S_ProjectCategory(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_ProjectCategory>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_ProjectCategory>>("ProjectCategory/GetListBySequenceStatusSupplierId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_ProjectCategory>>> GetListBySequenceStatusSupplierIdParentId(string accessToken, string sequenceStatus, string supplierId, int parentId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"parentId", parentId},
            };
            return await _callApi.GetResponseDataAsync<List<M_ProjectCategory>>("ProjectCategory/GetListBySequenceStatusSupplierIdParentId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_ProjectCategory>>> GetListBySupplierId(string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_ProjectCategory>>("ProjectCategory/GetListBySupplierId", dictPars);
        }
        public async Task<ResponseData<List<M_ProjectCategory>>> GetListMenuBySupplierId(string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_ProjectCategory>>("ProjectCategory/GetListMenuBySupplierId", dictPars);
        }
        public async Task<ResponseData<M_ProjectCategory>> GetById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_ProjectCategory>("ProjectCategory/GetById", dictPars);
        }
        public async Task<ResponseData<M_ProjectCategory>> Create(string accessToken, EM_ProjectCategory model, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", model.supplierId},
                {"parentId", model.parentId ?? 0},
                {"name", model.name},
                {"remark", model.remark},
                {"sort", model.sort},
                {"status", model.status},
                {"createdBy", createdBy},
            };
            return await _callApi.PostResponseDataAsync<M_ProjectCategory>("ProjectCategory/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_ProjectCategory>> Update(string accessToken, EM_ProjectCategory model, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"supplierId", model.supplierId},
                {"parentId", model.parentId ?? 0},
                {"name", model.name},
                {"remark", model.remark},
                {"sort", model.sort},
                {"status", model.status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_ProjectCategory>("ProjectCategory/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_ProjectCategory>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_ProjectCategory>("ProjectCategory/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_ProjectCategory>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_ProjectCategory>("ProjectCategory/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_ProjectCategory>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_ProjectCategory>>("ProjectCategory/UpdateStatusList", dictPars, accessToken);
        }
    }
}
