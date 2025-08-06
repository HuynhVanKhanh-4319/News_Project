using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_DistributeAdministrativeUnit
    {
        Task<ResponseData<M_Country>> GetCountryById(int id);
        Task<ResponseData<M_Province>> GetProvinceById(int id);
        Task<ResponseData<M_District>> GetDistrictById(int id);
        Task<ResponseData<M_Ward>> GetWardById(int id);
        Task<ResponseData<M_Bank>> GetBankById(int id);
        Task<ResponseData<M_Folk>> GetFolkById(int id);
        Task<ResponseData<M_Religion>> GetReligionById(int id);
        Task<ResponseData<List<M_Country>>> GetListCountryByStatus(int status = 1);
        Task<ResponseData<List<M_Province>>> GetListProvinceByStatusCountryId(int countryId, int status = 1);
        Task<ResponseData<List<M_District>>> GetListDistrictByStatusProvinceId(int provinceId, int status = 1);
        Task<ResponseData<List<M_Ward>>> GetListWardByStatusDistrictId(int districtId, int status = 1);
        Task<ResponseData<List<M_Bank>>> GetListBankByStatus(int status = 1);
        Task<ResponseData<List<M_Folk>>> GetListFolkByStatus(int status = 1);
        Task<ResponseData<List<M_Religion>>> GetListReligionByStatus(int status = 1);
    }
    public class S_DistributeAdministrativeUnit : IS_DistributeAdministrativeUnit
    {
        private readonly ICallBaseApi _callApi;
        public S_DistributeAdministrativeUnit(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<M_Country>> GetCountryById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Country>("DistributeAdministrativeUnit/GetCountryById", dictPars);
        }
        public async Task<ResponseData<M_Province>> GetProvinceById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Province>("DistributeAdministrativeUnit/GetProvinceById", dictPars);
        }
        public async Task<ResponseData<M_District>> GetDistrictById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_District>("DistributeAdministrativeUnit/GetDistrictById", dictPars);
        }
        public async Task<ResponseData<M_Ward>> GetWardById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Ward>("DistributeAdministrativeUnit/GetWardById", dictPars);
        }
        public async Task<ResponseData<M_Bank>> GetBankById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Bank>("DistributeAdministrativeUnit/GetBankById", dictPars);
        }
        public async Task<ResponseData<M_Folk>> GetFolkById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Folk>("DistributeAdministrativeUnit/GetFolkById", dictPars);
        }
        public async Task<ResponseData<M_Religion>> GetReligionById(int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Religion>("DistributeAdministrativeUnit/GetReligionById", dictPars);
        }
        public async Task<ResponseData<List<M_Country>>> GetListCountryByStatus(int status = 1)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"status", status},
            };
            return await _callApi.GetResponseDataAsync<List<M_Country>>("DistributeAdministrativeUnit/GetListCountryByStatus", dictPars);
        }
        public async Task<ResponseData<List<M_Province>>> GetListProvinceByStatusCountryId(int countryId, int status = 1)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"countryId", countryId},
                {"status", status},
            };
            return await _callApi.GetResponseDataAsync<List<M_Province>>("DistributeAdministrativeUnit/GetListProvinceByStatusCountryId", dictPars);
        }
        public async Task<ResponseData<List<M_District>>> GetListDistrictByStatusProvinceId(int provinceId, int status = 1)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"provinceId", provinceId},
                {"status", status},
            };
            return await _callApi.GetResponseDataAsync<List<M_District>>("DistributeAdministrativeUnit/GetListDistrictByStatusProvinceId", dictPars);
        }
        public async Task<ResponseData<List<M_Ward>>> GetListWardByStatusDistrictId(int districtId, int status = 1)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"districtId", districtId},
                {"status", status},
            };
            return await _callApi.GetResponseDataAsync<List<M_Ward>>("DistributeAdministrativeUnit/GetListWardByStatusDistrictId", dictPars);
        }
        public async Task<ResponseData<List<M_Bank>>> GetListBankByStatus(int status = 1)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"status", status},
            };
            return await _callApi.GetResponseDataAsync<List<M_Bank>>("DistributeAdministrativeUnit/GetListBankByStatus", dictPars);
        }
        public async Task<ResponseData<List<M_Folk>>> GetListFolkByStatus(int status = 1)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"status", status},
            };
            return await _callApi.GetResponseDataAsync<List<M_Folk>>("DistributeAdministrativeUnit/GetListFolkByStatus", dictPars);
        }
        public async Task<ResponseData<List<M_Religion>>> GetListReligionByStatus(int status = 1)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"status", status},
            };
            return await _callApi.GetResponseDataAsync<List<M_Religion>>("DistributeAdministrativeUnit/GetListReligionByStatus", dictPars);
        }
    }
}
