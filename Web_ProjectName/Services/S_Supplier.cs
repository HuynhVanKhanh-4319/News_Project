using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_Supplier
    {
        Task<ResponseData<List<M_Supplier>>> getListSupplierBySequenceStatusProvinceIdDistrictId(string accessToken, string sequenceStatus, int? provinceId, int? districtId);
        Task<ResponseData<M_Supplier>> GetById(string id);
        Task<ResponseData<M_Supplier>> Update(string accessToken, EM_Supplier model, string refCode, string updatedBy);
        Task<ResponseData<M_Supplier>> Delete(string accessToken, string id, string updatedBy);
        Task<ResponseData<M_Supplier>> UpdateStatus(string accessToken, string id, int status, DateTime timer, string updatedBy);
        Task<ResponseData<List<M_Supplier>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
    }
    public class S_Supplier : IS_Supplier
    {
        private readonly ICallBaseApi _callApi;
        private readonly IS_Address _s_Address;

        public S_Supplier(ICallBaseApi callApi, IS_Address address)
        {
            _callApi = callApi;
            _s_Address = address;
        }

        public async Task<ResponseData<List<M_Supplier>>> getListSupplierBySequenceStatusProvinceIdDistrictId(string accessToken, string sequenceStatus, int? provinceId, int? districtId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"provinceId", provinceId ?? 0},
                {"districtId", districtId ?? 0},
            };
            return await _callApi.GetResponseDataAsync<List<M_Supplier>>("Supplier/getListSupplierBySequenceStatusProvinceIdDistrictId", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Supplier>> GetById(string id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Supplier>("Supplier/GetById", dictPars);
        }
        public async Task<ResponseData<M_Supplier>> Update(string accessToken, EM_Supplier model, string refCode, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            ResponseData<M_Supplier> res = new ResponseData<M_Supplier>();
            if (model.addressId != null && model.addressId != 0)
            {
                var resAddress = await _s_Address.Update(accessToken, model.addressObj, updatedBy);
                if (resAddress.result != 1 || resAddress.data == null)
                {
                    res.result = resAddress.result; res.error = resAddress.error;
                    return res;
                }
            }
            else
            {
                var resAddress = await _s_Address.Create(accessToken, model.addressObj, updatedBy);
                if (resAddress.result == 1 && resAddress.data != null)
                    model.addressId = resAddress.data.id;
                else
                {
                    res.result = resAddress.result; res.error = resAddress.error;
                    return res;
                }
            }
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"name", model.name},
                {"shortName", model.shortName},
                {"businessNumber", model.businessNumber},
                {"headerFullName", model.headerFullName},
                {"description", model.description},
                {"logoUrl", model.logoUrl},
                {"faviconUrl", model.faviconUrl},
                {"addressId", model.addressId},
                {"hotline", model.hotline},
                {"phone", model.phone},
                {"email", model.email},
                {"websiteUrl", model.websiteUrl},
                {"facebook", model.facebook},
                {"twitter", model.twitter},
                {"instagram", model.instagram},
                {"zalo", model.zalo},
                {"youtube", model.youtube},
                {"tiktokUrl", model.tiktokUrl},
                {"ministryIndustryTradeUrl", model.ministryIndustryTradeUrl},
                {"themeColorFirst", model.themeColorFirst},
                {"themeColorSecond", model.themeColorSecond},
                {"status", model.status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Supplier>("Supplier/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Supplier>> Delete(string accessToken, string id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_Supplier>("Supplier/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Supplier>> UpdateStatus(string accessToken, string id, int status, DateTime timer, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
                {"timer", timer.ToString("O")},
            };
            return await _callApi.PutResponseDataAsync<M_Supplier>("Supplier/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Supplier>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_Supplier>>("Supplier/UpdateStatusList", dictPars, accessToken);
        }
    }
}
