using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_SupplierOffice
    {
        Task<ResponseData<List<M_SupplierOffice>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId);
        Task<ResponseData<M_SupplierOffice>> GetById(int id);
        Task<ResponseData<M_SupplierOffice>> Create(string accessToken, EM_SupplierOffice model, string createdBy);
        Task<ResponseData<M_SupplierOffice>> Update(string accessToken, EM_SupplierOffice model, string updatedBy);
        Task<ResponseData<M_SupplierOffice>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_SupplierOffice>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<List<M_SupplierOffice>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
    }
    public class S_SupplierOffice : IS_SupplierOffice
    {
        private readonly ICallBaseApi _callApi;
        public S_SupplierOffice(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<List<M_SupplierOffice>>> GetListBySequenceStatusSupplierId(string accessToken, string sequenceStatus, string supplierId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
            };
            return await _callApi.GetResponseDataAsync<List<M_SupplierOffice>>("SupplierOffice/GetListBySequenceStatusSupplierId", dictPars, accessToken);
        }
        public async Task<ResponseData<M_SupplierOffice>> GetById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_SupplierOffice>("SupplierOffice/GetById", dictPars);
        }
        public async Task<ResponseData<M_SupplierOffice>> Create(string accessToken, EM_SupplierOffice model, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"supplierId", model.supplierId},
                {"title", model.title},
                {"phone", model.phone},
                {"email", model.email},
                {"addressNumber", model.addressNumber},
                {"addressText", model.addressText},
                {"countryId", model.countryId ?? 1},
                {"provinceId", model.provinceId},
                {"districtId", model.districtId},
                {"wardId", model.wardId},
                {"latitude", model.latitude ?? 0},
                {"longitude", model.longitude ?? 0},
                {"status", model.status},
                {"createdBy", createdBy},
            };
            return await _callApi.PostResponseDataAsync<M_SupplierOffice>("SupplierOffice/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_SupplierOffice>> Update(string accessToken, EM_SupplierOffice model, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"supplierId", model.supplierId},
                {"title", model.title},
                {"phone", model.phone},
                {"email", model.email},
                {"addressNumber", model.addressNumber},
                {"addressText", model.addressText},
                {"countryId", model.countryId ?? 1},
                {"provinceId", model.provinceId},
                {"districtId", model.districtId},
                {"wardId", model.wardId},
                {"latitude", model.latitude ?? 0},
                {"longitude", model.longitude ?? 0},
                {"status", model.status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_SupplierOffice>("SupplierOffice/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_SupplierOffice>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_SupplierOffice>("SupplierOffice/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_SupplierOffice>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_SupplierOffice>("SupplierOffice/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_SupplierOffice>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_SupplierOffice>>("SupplierOffice/UpdateStatusList", dictPars, accessToken);
        }
    }
}
