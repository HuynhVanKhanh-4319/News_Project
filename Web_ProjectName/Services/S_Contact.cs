using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_Contact
    {
        Task<ResponseData<List<M_Contact>>> GetListBySequenceStatusSupplierIdProductIdFdateTdate(string accessToken, string sequenceStatus, string supplierId, int? productId, DateTime? fdate, DateTime? tdate);
        Task<ResponseData<M_Contact>> GetById(string accessToken, int id);
        Task<ResponseData<M_Contact>> Create(EM_Contact model, string createdBy);
        Task<ResponseData<M_Contact>> Update(string accessToken, EM_Contact model, string updatedBy);
        Task<ResponseData<M_Contact>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_Contact>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<List<M_Contact>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
    }
    public class S_Contact : IS_Contact
    {
        private readonly ICallBaseApi _callApi;
        public S_Contact(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_Contact>>> GetListBySequenceStatusSupplierIdProductIdFdateTdate(string accessToken, string sequenceStatus, string supplierId, int? productId, DateTime? fdate, DateTime? tdate)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"productId", productId}, //-2: load all contact product; 0: load contact page; null: all
                {"fdate", fdate?.ToString("O")},
                {"tdate", tdate?.ToString("O")},
            };
            return await _callApi.GetResponseDataAsync<List<M_Contact>>("Contact/GetListBySequenceStatusSupplierIdProductIdFdateTdate", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Contact>> GetById(string accessToken, int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Contact>("Contact/GetById", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Contact>> Create(EM_Contact model, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", model.supplierId},
                {"productId", model.productId ?? 0},
                {"name", model.name},
                {"email", model.email},
                {"phone", model.phone},
                {"title", model.title},
                {"detail", model.detail},
                {"remark", model.remark},
                {"status", model.status},
                {"createdBy", string.IsNullOrEmpty(createdBy) ? 0 : createdBy},
            };
            return await _callApi.PostResponseDataAsync<M_Contact>("Contact/Create", dictPars);
        }
        public async Task<ResponseData<M_Contact>> Update(string accessToken, EM_Contact model, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"supplierId", model.supplierId},
                {"productId", model.productId ?? 0},
                {"name", model.name},
                {"email", model.email},
                {"phone", model.phone},
                {"title", model.title},
                {"detail", model.detail},
                {"remark", model.remark},
                {"status", model.status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Contact>("Contact/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Contact>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_Contact>("Contact/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Contact>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Contact>("Contact/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Contact>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_Contact>>("Contact/UpdateStatusList", dictPars, accessToken);
        }
    }
}
