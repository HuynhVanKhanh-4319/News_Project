using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_Address
    {
        Task<ResponseData<M_Address>> Create(string accessToken, EM_Address model, string createdBy);
        Task<ResponseData<M_Address>> Update(string accessToken, EM_Address model, string updatedBy);
    }
    public class S_Address : IS_Address
    {
        private readonly ICallBaseApi _callApi;
        public S_Address(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<M_Address>> Create(string accessToken, EM_Address model, string createdBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"title", model.title},
                {"addressNumber", model.addressNumber},
                {"addressText", model.addressText},
                {"countryId", model.countryId ?? 1},
                {"provinceId", model.provinceId ?? 0},
                {"districtId", model.districtId ?? 0},
                {"wardId", model.wardId ?? 0},
                {"latitude", model.latitude ?? 0},
                {"longitude", model.longitude ?? 0},
                {"createdBy", createdBy},
            };
            return await _callApi.PostResponseDataAsync<M_Address>("Address/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Address>> Update(string accessToken, EM_Address model, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"title", model.title},
                {"addressNumber", model.addressNumber},
                {"addressText", model.addressText},
                {"countryId", model.countryId ?? 1},
                {"provinceId", model.provinceId ?? 0},
                {"districtId", model.districtId ?? 0},
                {"wardId", model.wardId ?? 0},
                {"latitude", model.latitude ?? 0},
                {"longitude", model.longitude ?? 0},
                {"status", model.status},
                {"updatedBy", updatedBy},
                {"timer", model.timer?.ToString("O")},
            };
            return await _callApi.PutResponseDataAsync<M_Address>("Address/Update", dictPars, accessToken);
        }
    }
}
