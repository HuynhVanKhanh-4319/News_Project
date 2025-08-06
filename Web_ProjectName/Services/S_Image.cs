using Microsoft.Extensions.Options;
using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using Web_ProjectName.Models.Common;
using static System.String;

namespace Web_ProjectName.Services
{
    public interface IS_Image
    {
        Task<ResponseData<M_Image>> UploadImage(IFormFile imageFile, string refId, string createdBy);
        Task<ResponseData<M_Image>> UploadImageResize(IFormFile imageFile, string refId, string createdBy);
        Task<ResponseData<M_Image>> UploadImageResizeWebp(IFormFile imageFile, string refId, string createdBy);
        Task<ResponseData<List<M_Image>>> UploadListImage(List<IFormFile> imageListFile, string refId, string createdBy);
        Task<ResponseData<List<M_Image>>> UploadListImageResize(List<IFormFile> imageListFile, string refId, string createdBy);
        Task<ResponseData<List<M_Image>>> UploadListImageResizeWebp(List<IFormFile> listFile, string refId, string createdBy);
        Task<ResponseData<M_Image>> UploadFileCustomPath(IFormFile file, string path, string refId, string createdBy);
        Task<ResponseData<M_Image>> UploadFile(IFormFile file, string refId, string createdBy);
        Task<ResponseData<List<M_Image>>> UploadListFile(List<IFormFile> listFile, string refId, string createdBy);
        Task<ResponseData<object>> GenerateCatalogue(string urlFile);
        Task<ResponseData<int>> CountFileFromFolder(string urlFile, string folderName = "large");

    }
    public class S_Image : IS_Image
    {
        private readonly ICallApi _callApi;
        private readonly IOptions<Config_ApiSettings> _apiSettings;

        public S_Image(ICallApi callApi, IOptions<Config_ApiSettings> apiSettings)
        {
            _callApi = callApi;
            _apiSettings = apiSettings;
        }

        //Image upload
        public async Task<ResponseData<M_Image>> UploadImage(IFormFile imageFile, string refId, string createdBy)
        {
            Dictionary<string, dynamic> dictHead = new Dictionary<string, dynamic>
            {
                {"Authorization", $"Bearer {_apiSettings.Value.TokenImageUpload}"},
            };
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "RefId");
            formData.Add(ImageHelper.EncodeToStreamContent(imageFile), "ImageFile", imageFile.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "CreatedBy");
            return await _callApi.PostResponseDataAsync<M_Image>(_apiSettings.Value.UrlApiImage + "Image/UploadImage", formData, dictHead);
        }
        public async Task<ResponseData<M_Image>> UploadImageResize(IFormFile imageFile, string refId, string createdBy)
        {
            Dictionary<string, dynamic> dictHead = new Dictionary<string, dynamic>
            {
                {"Authorization", $"Bearer {_apiSettings.Value.TokenImageUpload}"},
            };
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "RefId");
            formData.Add(ImageHelper.EncodeToStreamContent(imageFile), "ImageFile", imageFile.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "CreatedBy");
            return await _callApi.PostResponseDataAsync<M_Image>(_apiSettings.Value.UrlApiImage + "Image/UploadImageResize", formData, dictHead);
        }
        public async Task<ResponseData<M_Image>> UploadImageResizeWebp(IFormFile imageFile, string refId, string createdBy)
        {
            Dictionary<string, dynamic> dictHead = new Dictionary<string, dynamic>
            {
                {"Authorization", $"Bearer {_apiSettings.Value.TokenImageUpload}"},
            };
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "RefId");
            formData.Add(ImageHelper.EncodeToStreamContent(imageFile), "ImageFile", imageFile.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "CreatedBy");
            return await _callApi.PostResponseDataAsync<M_Image>(_apiSettings.Value.UrlApiImage + "Image/UploadImageResizeWebp", formData, dictHead);
        }
        public async Task<ResponseData<List<M_Image>>> UploadListImage(List<IFormFile> imageListFile, string refId, string createdBy)
        {
            Dictionary<string, dynamic> dictHead = new Dictionary<string, dynamic>
            {
                {"Authorization", $"Bearer {_apiSettings.Value.TokenImageUpload}"},
            };
            MultipartFormDataContent formData = new MultipartFormDataContent();
            foreach (var item in imageListFile)
                formData.Add(ImageHelper.EncodeToStreamContent(item), "ImageListFile", item.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "RefId");
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "CreatedBy");
            return await _callApi.PostResponseDataAsync<List<M_Image>>(_apiSettings.Value.UrlApiImage + "Image/UploadListImage", formData, dictHead);
        }
        public async Task<ResponseData<List<M_Image>>> UploadListImageResize(List<IFormFile> imageListFile, string refId, string createdBy)
        {
            Dictionary<string, dynamic> dictHead = new Dictionary<string, dynamic>
            {
                {"Authorization", $"Bearer {_apiSettings.Value.TokenImageUpload}"},
            };
            MultipartFormDataContent formData = new MultipartFormDataContent();
            foreach (var item in imageListFile)
                formData.Add(ImageHelper.EncodeToStreamContent(item), "ImageListFile", item.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "RefId");
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "CreatedBy");
            return await _callApi.PostResponseDataAsync<List<M_Image>>(_apiSettings.Value.UrlApiImage + "Image/UploadListImageResize", formData, dictHead);
        }
        public async Task<ResponseData<List<M_Image>>> UploadListImageResizeWebp(List<IFormFile> listFile, string refId, string createdBy)
        {
            Dictionary<string, dynamic> dictHead = new Dictionary<string, dynamic>
            {
                {"Authorization", $"Bearer {_apiSettings.Value.TokenImageUpload}"},
            };
            MultipartFormDataContent formData = new MultipartFormDataContent();
            foreach (var item in listFile)
                formData.Add(ImageHelper.EncodeToStreamContent(item), "ImageListFile", item.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "RefId");
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "CreatedBy");
            return await _callApi.PostResponseDataAsync<List<M_Image>>(_apiSettings.Value.UrlApiImage + "Image/UploadListImageResizeWebp", formData, dictHead);
        }

        //File upload
        public async Task<ResponseData<M_Image>> UploadFileCustomPath(IFormFile file, string path, string refId, string createdBy)
        {
            Dictionary<string, dynamic> dictHead = new Dictionary<string, dynamic>
            {
                {"Authorization", $"Bearer {_apiSettings.Value.TokenImageUpload}"},
            };
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(new StringContent(IsNullOrEmpty(path) ? "" : path), "Path"); //VD: data/save
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "RefId");
            formData.Add(ImageHelper.EncodeToStreamContent(file), "File", file.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "CreatedBy");
            return await _callApi.PostResponseDataAsync<M_Image>(_apiSettings.Value.UrlApiImage + "Image/UploadFileCustomPath", formData, dictHead);
        }
        public async Task<ResponseData<M_Image>> UploadFile(IFormFile file, string refId, string createdBy)
        {
            Dictionary<string, dynamic> dictHead = new Dictionary<string, dynamic>
            {
                {"Authorization", $"Bearer {_apiSettings.Value.TokenImageUpload}"},
            };
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "RefId");
            formData.Add(ImageHelper.EncodeToStreamContent(file), "File", file.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "CreatedBy");
            return await _callApi.PostResponseDataAsync<M_Image>(_apiSettings.Value.UrlApiImage + "Image/UploadFile", formData, dictHead);
        }
        public async Task<ResponseData<List<M_Image>>> UploadListFile(List<IFormFile> listFile, string refId, string createdBy)
        {
            Dictionary<string, dynamic> dictHead = new Dictionary<string, dynamic>
            {
                {"Authorization", $"Bearer {_apiSettings.Value.TokenImageUpload}"},
            };
            MultipartFormDataContent formData = new MultipartFormDataContent();
            foreach (var item in listFile)
                formData.Add(ImageHelper.EncodeToStreamContent(item), "ListFile", item.FileName);
            formData.Add(new StringContent(IsNullOrEmpty(refId) ? "" : refId), "RefId");
            formData.Add(new StringContent(IsNullOrEmpty(createdBy) ? "0" : createdBy), "CreatedBy");
            return await _callApi.PostResponseDataAsync<List<M_Image>>(_apiSettings.Value.UrlApiImage + "Image/UploadListFile", formData, dictHead);
        }
        public async Task<ResponseData<object>> GenerateCatalogue(string urlFile)
        {
            Dictionary<string, dynamic> dictHead = new Dictionary<string, dynamic>
            {
                {"Authorization", $"Bearer {_apiSettings.Value.TokenImageUpload}"},
            };
            MultipartFormDataContent formData = new MultipartFormDataContent();
            formData.Add(new StringContent(IsNullOrEmpty(urlFile) ? "" : urlFile), "urlFile");
            return await _callApi.PostResponseDataAsync<object>(_apiSettings.Value.UrlApiImage + "Image/GenerateCatalogue", formData, dictHead);
        }
        public async Task<ResponseData<int>> CountFileFromFolder(string urlFile, string folderName = "large")
        {
            Dictionary<string, dynamic> dictHead = new Dictionary<string, dynamic>
            {
                {"Authorization", $"Bearer {_apiSettings.Value.TokenImageUpload}"},
            }; 
            Dictionary<string, dynamic> dictPars = new Dictionary<string, dynamic>
            {
                {"urlFile", urlFile},
                {"folderName", folderName},
            };
            return await _callApi.GetDictHeaderResponseDataAsync<int>(_apiSettings.Value.UrlApiImage + "Image/CountFileFromFolder", dictPars, dictHead);
        }
    }
}