using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Web_ProjectName.ExtensionMethods;
using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using Web_ProjectName.Models.Common;
using Web_ProjectName.Services;
using Web_ProjectName.ViewModels;
using static System.String;

namespace Web_ProjectName.Controllers
{
    public class ProductController : BaseController<HomeController>
    {
        private readonly IS_Product _s_Product;
        private readonly IS_ProductCategory _s_ProductCategory;
        private readonly IS_Image _s_Image;
        private readonly IS_Banner _s_Banner;
        private readonly IS_Contact _s_Contact;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IS_GoogleReCAPTCHA _s_GoogleReCAPTCHA;
        private readonly IOptions<Config_Supplier> _configSupplier;
        private const int MAX_RECORDS = 64;

        public ProductController(IS_Product product, IS_ProductCategory productCategory, IS_Image image, IS_Banner banner, IS_Contact contact, IWebHostEnvironment webHostEnvironment, IS_GoogleReCAPTCHA googleReCAPTCHA, IOptions<Config_Supplier> configSupplier)
        {
            _s_Product = product;
            _s_ProductCategory = productCategory;
            _s_Image = image;
            _s_Banner = banner;
            _s_Contact = contact;
            _webHostEnvironment = webHostEnvironment;
            _s_GoogleReCAPTCHA = googleReCAPTCHA;
            _configSupplier = configSupplier;
        }

        public async Task<IActionResult> Index(string keyword, string c, EN_TypeSearchProduct type = EN_TypeSearchProduct.All, int record = 24, int page = 1)
        {
            keyword = CleanXSSHelper.CleanXSS(keyword);
            ViewBag.keyword = keyword;
            ViewBag.category = c;
            ViewBag.record = record;
            ViewBag.page = page;
            string nameLv1 = default;

            keyword = !IsNullOrEmpty(keyword) && keyword.Length > 50 ? keyword.Substring(0, 50) + "..." : keyword;
            var breadCrumb = new VM_BreadCrumb();
            if (!IsNullOrEmpty(keyword))
            {
                breadCrumb.currentName = keyword;
                breadCrumb.lv1Name = "Sản phẩm";
                breadCrumb.lv1Url = "/san-pham";
            }
            else
            {
                if (!IsNullOrEmpty(nameLv1))
                {
                    breadCrumb.currentName = nameLv1;
                    breadCrumb.lv1Name = "Sản phẩm";
                    breadCrumb.lv1Url = "/san-pham";
                }
                else
                {
                    breadCrumb.currentName = "Sản phẩm";
                }
            }
            SetDropDownTypeProduct(type);
            Task<ResponseData<List<M_Product>>> taskProductHot = _s_Product.GetListBySequenceStatusSupplierIdProductCategoryId("1", "1", _supplierId, default);

            await Task.WhenAll(taskProductHot);
            ViewBag.ProductHot = taskProductHot.Result.data ?? new List<M_Product>();
            ViewBag.BreadCrumb = breadCrumb;


            _memoryCache.TryGetValue(CommonConstants.CACHE_KEY_METASEO, out VM_MetaSeo metaSeo);
            if (metaSeo?.product != null)
            {
                ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEOCustom(this, metaSeo.product);
            }

            return View();
        }

        public IActionResult Search(string keyword)
        {
            keyword = CleanXSSHelper.CleanXSS(keyword);
            return Redirect($"/san-pham?keyword={keyword}");
        }

        public async Task<JsonResult> GetList(string keyword, string c, EN_TypeSearchProduct type = EN_TypeSearchProduct.All, int record = 24, int page = 1)
        {
            if (record > MAX_RECORDS) //maximum records
                record = MAX_RECORDS;

            keyword = CleanXSSHelper.CleanXSS(keyword);
            var res = await _s_Product.GetListByPaging("1", _supplierId, c, keyword, type, page, record);
            return Json(new M_JResult(res));
        }

        public async Task<IActionResult> ViewDetail(string metaUrl)
        {
            metaUrl = CleanXSSHelper.CleanXSS(metaUrl);
            var res = await _s_Product.GetByMetaUrl(metaUrl);
            if (res.result != 1 || res.data == null) return Redirect("/");
            string categoryParentName = "";
            if (res.data.productCategoryObj?.parentId > 0)
            {
                var resCategoryParent = await _s_ProductCategory.GetById(res.data.productCategoryObj?.parentId ?? 0);
                categoryParentName = resCategoryParent.data?.name;
            }
            var breadCrumb = new VM_BreadCrumb
            {
                currentName = res.data.name,
                lv1Name = "Sản phẩm",
                lv1Url = "/san-pham",
                lv2Name = categoryParentName,
                lv2Url = $"/san-pham?c={res.data.productCategoryObj?.parentId}",
                lv3Name = res.data.productCategoryId == 0 ? "" : res.data.productCategoryObj?.name,
                lv3Url = $"/san-pham?c={res.data.productCategoryId}",
            };
            Task<ResponseData<List<M_Product>>> taskProductHot = _s_Product.GetListBySequenceStatusSupplierIdProductCategoryId("1", "1", _supplierId, default);
            Task<ResponseData<List<M_Product>>> taskProductRelated = _s_Product.GetListByPaging("1", _supplierId, res.data.productCategoryId.ToString(), default, EN_TypeSearchProduct.All, 1, 10);

            await Task.WhenAll(taskProductHot, taskProductRelated);

            var productRelated = new List<M_Product>();
            if (taskProductRelated.Result.data != null)
                productRelated = taskProductRelated.Result.data.Where(x => x.id != res.data.id).ToList();

            ViewBag.ProductHot = taskProductHot.Result.data ?? new List<M_Product>();
            ViewBag.ProductRelated = productRelated;
            ViewBag.BreadCrumb = breadCrumb;
            ExtensionMethods.SetViewDataSEOExtensionMethod.SetViewDataSEOCustom(this, new VM_ViewDataSEO
            {
                Title = IsNullOrEmpty(res.data.metaTitle) ? res.data.name : res.data.metaTitle,
                Description = IsNullOrEmpty(res.data.metaDescription) ? res.data.description : res.data.metaDescription,
                Keywords = IsNullOrEmpty(res.data.metaKeywords) ? res.data.name : res.data.metaKeywords,
                Image = IsNullOrEmpty(res.data.metaImagePreview) ? res.data.imageObj?.mediumUrl : res.data.metaImagePreview,
            });

            ViewBag.AllowCatalogue = _configSupplier.Value.catalogueProduct;
            return View(res.data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(EM_Contact model, string tokenReCAPTCHA)
        {
            M_JResult jResult = new M_JResult();
            if (!_webHostEnvironment.IsDevelopment())
            {
                //Google check robot
                var _googeRecp = await _s_GoogleReCAPTCHA.VertifyToken(tokenReCAPTCHA);
                if (!_googeRecp.success && _googeRecp.score <= 0.5)
                {
                    jResult.result = 0;
                    jResult.error.message = "reCAPTCHA warning: You are a robot. Denied access.";
                    return Json(jResult);
                }
            }

            if (!ModelState.IsValid)
            {
                jResult.error = new error(0, DataAnnotationExtensionMethod.GetErrorMessage(ModelState));
                return Json(jResult);
            }
            model.supplierId = Convert.ToInt32(_supplierId);
            model.status = 0;
            var res = await _s_Contact.Create(model, default);
            return Json(jResult.MapData(res));
        }
    }
}
