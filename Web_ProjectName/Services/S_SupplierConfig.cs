using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_SupplierConfig
    {
        Task<ResponseData<M_SupplierConfig>> GetById(int id);
        Task<ResponseData<M_SupplierConfig>> GetBySupplierId(string supplierId);
        Task<ResponseData<M_SupplierConfig>> Create(string accessToken, EM_SupplierConfig model, string createdBy);
        Task<ResponseData<M_SupplierConfig>> Update(string accessToken, EM_SupplierConfig model, string updatedBy);
        Task<ResponseData<M_SupplierConfig>> CreateOrUpdate(string accessToken, EM_SupplierConfig model, string updatedBy);
        Task<ResponseData<M_SupplierConfig>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_SupplierConfig>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
    }
    public class S_SupplierConfig : IS_SupplierConfig
    {
        private readonly ICallBaseApi _callApi;
        public S_SupplierConfig(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<M_SupplierConfig>> GetById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_SupplierConfig>("SupplierConfig/GetById", dictPars);
        }
        public async Task<ResponseData<M_SupplierConfig>> GetBySupplierId(string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<M_SupplierConfig>("SupplierConfig/GetBySupplierId", dictPars);
        }
        public async Task<ResponseData<M_SupplierConfig>> Create(string accessToken, EM_SupplierConfig model, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", model.supplierId},
                {"mailDisplayName", model.mailDisplayName},
                {"mailFrom", model.mailFrom},
                {"mailPassword", Encryptor.Encrypt(model.mailPassword)},
                {"mailHost", model.mailHost},
                {"mailPort", model.mailPort},
                {"mailTo", model.mailTo},
                {"status", model.status},
                {"createdBy", createdBy},
            };
            return await _callApi.PostResponseDataAsync<M_SupplierConfig>("SupplierConfig/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_SupplierConfig>> Update(string accessToken, EM_SupplierConfig model, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"supplierId", model.supplierId},
                {"mailDisplayName", model.mailDisplayName},
                {"mailFrom", model.mailFrom},
                {"mailPassword", Encryptor.Encrypt(model.mailPassword)},
                {"mailHost", model.mailHost},
                {"mailPort", model.mailPort},
                {"mailTo", model.mailTo},
                {"status", model.status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_SupplierConfig>("SupplierConfig/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_SupplierConfig>> CreateOrUpdate(string accessToken, EM_SupplierConfig model, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"supplierId", model.supplierId},
                {"mailDisplayName", model.mailDisplayName},
                {"mailFrom", model.mailFrom},
                {"mailPassword", Encryptor.Encrypt(model.mailPassword)},
                {"mailHost", model.mailHost},
                {"mailPort", model.mailPort},
                {"mailTo", model.mailTo},
                {"status", model.status},
                {"createdBy", updatedBy},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_SupplierConfig>("SupplierConfig/CreateOrUpdate", dictPars, accessToken);
        }
        public async Task<ResponseData<M_SupplierConfig>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_SupplierConfig>("SupplierConfig/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_SupplierConfig>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_SupplierConfig>("SupplierConfig/UpdateStatus", dictPars, accessToken);
        }
    }
}
