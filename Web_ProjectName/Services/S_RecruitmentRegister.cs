using Newtonsoft.Json;
using Web_ProjectName.Lib;
using Web_ProjectName.Models;

namespace Web_ProjectName.Services
{
    public interface IS_RecruitmentRegister
    {
        Task<ResponseData<List<M_RecruitmentRegister>>> GetListBySequenceStatusSupplierIdRecruitmentIdFdateTdate(string accessToken, string sequenceStatus, string supplierId, int? recruitmentId, DateTime? fdate, DateTime? tdate);
        Task<ResponseData<M_RecruitmentRegister>> GetById(string accessToken, int id);
        //Task<ResponseData<M_RecruitmentRegister>> Create(EM_RecruitmentRegister model,string refCode, string createdBy);
        Task<ResponseData<M_RecruitmentRegister>> Update(string accessToken, EM_RecruitmentRegister model, string refCode, string updatedBy);
        Task<ResponseData<M_RecruitmentRegister>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_RecruitmentRegister>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<List<M_RecruitmentRegister>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
    }
    public class S_RecruitmentRegister : IS_RecruitmentRegister
    {
        private readonly ICallBaseApi _callApi;
        private readonly IS_Image _s_Image;
        public S_RecruitmentRegister(ICallBaseApi callApi, IS_Image image)
        {
            _callApi = callApi;
            _s_Image = image;
        }

        public async Task<ResponseData<List<M_RecruitmentRegister>>> GetListBySequenceStatusSupplierIdRecruitmentIdFdateTdate(string accessToken, string sequenceStatus, string supplierId, int? recruitmentId, DateTime? fdate, DateTime? tdate)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"recruitmentId", recruitmentId},
                {"fdate", fdate?.ToString("O")},
                {"tdate", tdate?.ToString("O")},
            };
            return await _callApi.GetResponseDataAsync<List<M_RecruitmentRegister>>("RecruitmentRegister/GetListBySequenceStatusSupplierIdRecruitmentIdFdateTdate", dictPars, accessToken);
        }
        public async Task<ResponseData<M_RecruitmentRegister>> GetById(string accessToken, int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_RecruitmentRegister>("RecruitmentRegister/GetById", dictPars, accessToken);
        }
        //public async Task<ResponseData<M_RecruitmentRegister>> Create(EM_RecruitmentRegister model, string refCode, string createdBy)
        //{
        //    model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
        //    ResponseData<M_RecruitmentRegister> res = new ResponseData<M_RecruitmentRegister>();
        //    if (model.listFile != null && model.listFile.Count > 0)
        //    {
        //        var imgUpload = await _s_Image.UploadListFile(model.listFile, refCode, createdBy);
        //        if (imgUpload.result == 1 && imgUpload.data != null)
        //        {
        //            model.imageList = JsonConvert.SerializeObject(imgUpload.data.Select(x => x.id));
        //        }
        //        else
        //        {
        //            res.result = imgUpload.result; res.error = imgUpload.error;
        //            return res;
        //        }
        //    }
        //    else
        //    {
        //        model.imageList = "[]";
        //    }
        //    Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
        //    {
        //        {"recruitmentId", model.recruitmentId},
        //        {"supplierId", model.supplierId},
        //        {"name", model.name},
        //        {"email", model.email},
        //        {"phone", model.phone},
        //        {"imageList", model.imageList},
        //        {"remark", model.remark},
        //        {"status", model.status},
        //        {"createdBy", string.IsNullOrEmpty(createdBy) ? 0 : createdBy},
        //    };
        //    return await _callApi.PostResponseDataAsync<M_RecruitmentRegister>("RecruitmentRegister/Create", dictPars);
        //}
        public async Task<ResponseData<M_RecruitmentRegister>> Update(string accessToken, EM_RecruitmentRegister model, string refCode, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"recruitmentId", model.recruitmentId},
                {"supplierId", model.supplierId},
                {"name", model.name},
                {"email", model.email},
                {"phone", model.phone},
                {"imageList", string.IsNullOrEmpty(model.imageList) ? "[]" : model.imageList},
                {"remark", model.remark},
                {"status", model.status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_RecruitmentRegister>("RecruitmentRegister/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_RecruitmentRegister>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_RecruitmentRegister>("RecruitmentRegister/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_RecruitmentRegister>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_RecruitmentRegister>("RecruitmentRegister/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_RecruitmentRegister>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_RecruitmentRegister>>("RecruitmentRegister/UpdateStatusList", dictPars, accessToken);
        }
    }
}
