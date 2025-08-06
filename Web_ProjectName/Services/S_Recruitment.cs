using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using static System.String;

namespace Web_ProjectName.Services
{
    public interface IS_Recruitment
    {
        Task<ResponseData<List<M_Recruitment>>> GetListBySequenceStatusSupplierIdRecruitmentCategoryId(string accessToken, string sequenceStatus, string supplierId, int? recruitmentCategoryId);
        Task<ResponseData<List<M_Recruitment>>> GetListBySequenceStatusSupplierIdSearchText(string accessToken, string sequenceStatus, string supplierId, string searchText);
        Task<ResponseData<List<M_Recruitment>>> GetListByPaging(string sequenceStatus, string supplierId, int? recruitmentCategoryId, string keyword, int page = 1, int record = 10);
        Task<ResponseData<M_Recruitment>> GetById(string accessToken, int id);
        Task<ResponseData<M_Recruitment>> GetByMetaUrl(string metaUrl);
        Task<ResponseData<M_Recruitment>> Create(string accessToken, EM_Recruitment model, string refCode, string createdBy);
        Task<ResponseData<M_Recruitment>> Update(string accessToken, EM_Recruitment model, string refCode, string updatedBy);
        Task<ResponseData<M_Recruitment>> Delete(string accessToken, int id, string updatedBy);
        Task<ResponseData<M_Recruitment>> UpdateStatus(string accessToken, int id, int status, string updatedBy);
        Task<ResponseData<List<M_Recruitment>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy);
    }
    public class S_Recruitment : IS_Recruitment
    {
        private readonly ICallBaseApi _callApi;
        private readonly IS_Image _s_Image;
        public S_Recruitment(ICallBaseApi callApi, IS_Image image)
        {
            _callApi = callApi;
            _s_Image = image;
        }

        public async Task<ResponseData<List<M_Recruitment>>> GetListBySequenceStatusSupplierIdRecruitmentCategoryId(string accessToken, string sequenceStatus, string supplierId, int? recruitmentCategoryId)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"recruitmentCategoryId", recruitmentCategoryId},
            };
            return await _callApi.GetResponseDataAsync<List<M_Recruitment>>("Recruitment/GetListBySequenceStatusSupplierIdRecruitmentCategoryId", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Recruitment>>> GetListBySequenceStatusSupplierIdSearchText(string accessToken, string sequenceStatus, string supplierId, string searchText)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"searchText", searchText},
            };
            return await _callApi.GetResponseDataAsync<List<M_Recruitment>>("Recruitment/GetListBySequenceStatusSupplierIdSearchText", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Recruitment>>> GetListByPaging(string sequenceStatus, string supplierId, int? recruitmentCategoryId, string keyword, int page = 1, int record = 10)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceStatus", sequenceStatus},
                {"supplierId", supplierId},
                {"recruitmentCategoryId", recruitmentCategoryId ?? 0},
                {"keyword", keyword},
                {"page", page},
                {"record", record},
            };
            return await _callApi.GetResponseDataAsync<List<M_Recruitment>>("Recruitment/GetListByPaging", dictPars);
        }
        public async Task<ResponseData<M_Recruitment>> GetById(string accessToken, int id)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
            };
            return await _callApi.GetResponseDataAsync<M_Recruitment>("Recruitment/GetById", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Recruitment>> GetByMetaUrl(string metaUrl)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"metaUrl", metaUrl},
            };
            return await _callApi.GetResponseDataAsync<M_Recruitment>("Recruitment/GetByMetaUrl", dictPars);
        }
        public async Task<ResponseData<M_Recruitment>> Create(string accessToken, EM_Recruitment model, string refCode, string createdBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            //ResponseData<M_Recruitment> res = new ResponseData<M_Recruitment>();
            //if (model.imageFile != null)
            //{
            //    var imgUpload = await _s_Image.UploadResize(model.imageFile, refCode, Empty, createdBy);
            //    if (imgUpload.result == 1 && imgUpload.data != null)
            //    {
            //        model.imageId = imgUpload.data.id;
            //    }
            //    else
            //    {
            //        res.result = imgUpload.result; res.error = imgUpload.error;
            //        return res;
            //    }
            //}
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"name", model.name},
                {"recruitmentCategoryId", model.recruitmentCategoryId},
                {"supplierId", model.supplierId},
                {"description", model.description},
                {"detail", model.detail},
                {"salary", model.salary},
                {"workingForm", model.workingForm},
                {"quantity", model.quantity},
                {"position", model.position},
                {"experience", model.experience},
                {"gender", model.gender},
                {"location", model.location},
                {"deadlineSubmission", model.deadlineSubmission?.ToString("O")},
                {"isHot", model.isHot},
                {"viewNumber", model.viewNumber},
                {"imageId", model.imageId},
                {"publishedAt", model.publishedAt.ToString("O")},
                {"metaKeywords", model.metaKeywords},
                {"metaDescription", model.metaDescription},
                {"metaTitle", model.metaTitle},
                {"metaUrl", model.metaUrl},
                {"metaImagePreview", model.metaImagePreview},
                {"status", model.status},
                {"createdBy", createdBy},
            };
            return await _callApi.PostResponseDataAsync<M_Recruitment>("Recruitment/Create", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Recruitment>> Update(string accessToken, EM_Recruitment model, string refCode, string updatedBy)
        {
            model = CleanXSSHelper.CleanXSSObject(model); //Clean XSS
            //ResponseData<M_Recruitment> res = new ResponseData<M_Recruitment>();
            //if (model.imageFile != null)
            //{
            //    var imgUpload = await _s_Image.UploadResize(model.imageFile, refCode, Empty, updatedBy);
            //    if (imgUpload.result == 1 && imgUpload.data != null)
            //    {
            //        model.imageId = imgUpload.data.id;
            //    }
            //    else
            //    {
            //        res.result = imgUpload.result; res.error = imgUpload.error;
            //        return res;
            //    }
            //}
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", model.id},
                {"name", model.name},
                {"recruitmentCategoryId", model.recruitmentCategoryId},
                {"supplierId", model.supplierId},
                {"description", model.description},
                {"detail", model.detail},
                {"salary", model.salary},
                {"workingForm", model.workingForm},
                {"quantity", model.quantity},
                {"position", model.position},
                {"experience", model.experience},
                {"gender", model.gender},
                {"location", model.location},
                {"deadlineSubmission", model.deadlineSubmission?.ToString("O")},
                {"isHot", model.isHot},
                {"viewNumber", model.viewNumber},
                {"imageId", model.imageId},
                {"publishedAt", model.publishedAt.ToString("O")},
                {"metaKeywords", model.metaKeywords},
                {"metaDescription", model.metaDescription},
                {"metaTitle", model.metaTitle},
                {"metaUrl", model.metaUrl},
                {"metaImagePreview", model.metaImagePreview},
                {"status", model.status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Recruitment>("Recruitment/Update", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Recruitment>> Delete(string accessToken, int id, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"updatedBy", updatedBy},
            };
            return await _callApi.DeleteResponseDataAsync<M_Recruitment>("Recruitment/Delete", dictPars, accessToken);
        }
        public async Task<ResponseData<M_Recruitment>> UpdateStatus(string accessToken, int id, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"id", id},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<M_Recruitment>("Recruitment/UpdateStatus", dictPars, accessToken);
        }
        public async Task<ResponseData<List<M_Recruitment>>> UpdateStatusList(string accessToken, string sequenceIds, int status, string updatedBy)
        {
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"sequenceIds", sequenceIds},
                {"status", status},
                {"updatedBy", updatedBy},
            };
            return await _callApi.PutResponseDataAsync<List<M_Recruitment>>("Recruitment/UpdateStatusList", dictPars, accessToken);
        }
    }
}
