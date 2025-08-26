using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using Web_ProjectName.Services;

namespace Web_ProjectName.Services
{
    public interface IS_TransportMaterial
    {
        Task<ResponseData<object>> CreateListPurchase(string accessToken, M_TransportMaterial_CreateListPurchase model, bool IsGetFull, bool IsFarmer, string jsonItem, string createdBy);

    }
    public class S_TransportMaterial : IS_TransportMaterial
{
        private readonly ICallBaseApi _callApi;
        public S_TransportMaterial(ICallBaseApi callApi)
        {
            _callApi = callApi;
        }

        public async Task<ResponseData<object>> CreateListPurchase(string accessToken, M_TransportMaterial_CreateListPurchase model, bool IsGetFull, bool IsFarmer, string jsonItem, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
    {
        {"FactoryId", model.FactoryId},
        {"Type", model.Type},
        {"FactoryDrcCup", model.FactoryDrcCup},
        {"FactoryDrcCupInfected", model.FactoryDrcCupInfected},
        {"FactoryDrcWire", model.FactoryDrcWire},
        {"FactoryDrcSolidify", model.FactoryDrcSolidify},
        {"TypeCreate", model.TypeCreate},
        {"IdentificationCriterionId", model.IdentificationCriterionId},
        {"StartDate", model.StartDate.ToString("O")},
        {"IsGetFull", IsGetFull},
        {"IsFarmer", IsFarmer},
        {"JsonItem", jsonItem},
        {"status", model.Status},
        {"createdBy", createdBy},
    };
            return await _callApi.PostResponseDataAsync<object>("TransportMaterial/CreateListPurchase", dictPars, accessToken);
        }
    }
}
