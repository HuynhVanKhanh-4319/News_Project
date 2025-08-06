using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_ProductCategory
    {
        Task<ResponseData<List<M_ProductCategory>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId);
        Task<ResponseData<List<M_ProductCategory>>> GetListBySequenceStatusSupplierIdParentId(string accessToken, string sequenceStatus, string supplierId, int parentId);
        Task<ResponseData<List<M_ProductCategory>>> GetListBySupplierId(string supplierId);
        Task<ResponseData<List<M_ProductCategory>>> GetListMenuBySupplierId(string supplierId);
        Task<ResponseData<M_ProductCategory>> GetById(int id);
        Task<ResponseData<M_ProductCategory>> Create(string accessToken, EM_ProductCategory model, string createdBy);
        Task<ResponseData<M_ProductCategory>> Update(string accessToken, EM_ProductCategory model, string updatedBy);
        Task<ResponseData<M_ProductCategory>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_ProductCategory>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<List<M_ProductCategory>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
    }
    public class S_ProductCategory : IS_ProductCategory
    {
        private readonly ICallBaseApi _callApi;
        public S_ProductCategory(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_ProductCategory>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_ProductCategory>>("ProductCategory/GetListBySequenceStatusSupplierId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_ProductCategory>>> GetListBySequenceStatusSupplierIdParentId(string accessToken, string sequenceStatus, string supplierId, int parentId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"parentId", parentId},
            };
            return await _callApi.GetResponseDataAsync<List<M_ProductCategory>>("ProductCategory/GetListBySequenceStatusSupplierIdParentId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_ProductCategory>>> GetListBySupplierId(string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_ProductCategory>>("ProductCategory/GetListBySupplierId", dictPars);
        }
        public async Task<ResponseData<List<M_ProductCategory>>> GetListMenuBySupplierId(string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_ProductCategory>>("ProductCategory/GetListMenuBySupplierId", dictPars);
        }
        public async Task<ResponseData<M_ProductCategory>> GetById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_ProductCategory>("ProductCategory/GetById", dictPars);
        }
        public async Task<ResponseData<M_ProductCategory>> Create(string accessToken, EM_ProductCategory model, string createdBy)
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
            return await _callApi.PostResponseDataAsync<M_ProductCategory>("ProductCategory/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_ProductCategory>> Update(string accessToken, EM_ProductCategory model, string updatedBy)
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
            return await _callApi.PutResponseDataAsync<M_ProductCategory>("ProductCategory/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_ProductCategory>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_ProductCategory>("ProductCategory/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_ProductCategory>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_ProductCategory>("ProductCategory/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_ProductCategory>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_ProductCategory>>("ProductCategory/UpdateStatusList", dictPars, accessToken);
        }
    }
}
