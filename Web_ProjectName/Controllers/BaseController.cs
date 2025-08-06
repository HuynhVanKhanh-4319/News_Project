using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using Web_ProjectName.Models.Common;
using Web_ProjectName.Services;
using Web_ProjectName.ViewModels;

namespace Web_ProjectName.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseController<T>
    {
        private IMemoryCache memoryCache;
        private IMapper mapper;
        private IHttpContextAccessor httpContextAccessor;

        protected IMapper _mapper => mapper ?? (mapper = HttpContext?.RequestServices.GetService<IMapper>());

        protected IHttpContextAccessor _httpContextAccessor => httpContextAccessor ?? (httpContextAccessor = HttpContext?.RequestServices.GetService<IHttpContextAccessor>());

        protected string _supplierId => HttpContext.RequestServices.GetService<IOptions<Config_Supplier>>().Value.id.ToString();

        protected string _refCode => HttpContext.RequestServices.GetService<IOptions<Config_Supplier>>().Value.refCode;
        
        protected string _package => HttpContext.RequestServices.GetService<IOptions<Config_Supplier>>().Value.package;

        protected IMemoryCache _memoryCache => memoryCache ?? (memoryCache = HttpContext?.RequestServices.GetService<IMemoryCache>());

        protected async Task SetDropDownCountry(int? selectedId = 0)
        {
            List<VM_SelectDropDown> result = new List<VM_SelectDropDown>();
            var res = await HttpContext?.RequestServices.GetService<IS_DistributeAdministrativeUnit>().GetListCountryByStatus();
            if (res.result == 1 && res.data != null)
                result = _mapper.Map<List<VM_SelectDropDown>>(res.data);
            ViewBag.CountryId = new SelectList(result, "Id", "Name", selectedId ?? 0);
        }

        protected async Task SetDropDownProvince(int? selectedId, int? countryId = 1)
        {
            List<VM_SelectDropDown> result = new List<VM_SelectDropDown>();
            if (countryId != 0)
            {
                var res = await HttpContext?.RequestServices.GetService<IS_DistributeAdministrativeUnit>().GetListProvinceByStatusCountryId(countryId ?? 1);
                if (res.result == 1 && res.data != null)
                    result = _mapper.Map<List<VM_SelectDropDown>>(res.data);
            }
            else
            {
                var res = await HttpContext?.RequestServices.GetService<IS_DistributeAdministrativeUnit>().GetListProvinceByStatusCountryId(1);
                if (res.result == 1 && res.data != null)
                    result = _mapper.Map<List<VM_SelectDropDown>>(res.data);
            }
            ViewBag.ProvinceId = new SelectList(result, "Id", "Name", selectedId ?? 0);
        }

        protected async Task SetDropDownDistrict(int? selectedId, int? provinceId = 0)
        {
            List<VM_SelectDropDown> result = new List<VM_SelectDropDown>();
            if (provinceId != 0)
            {
                var res = await HttpContext?.RequestServices.GetService<IS_DistributeAdministrativeUnit>().GetListDistrictByStatusProvinceId(provinceId ?? 0);
                if (res.result == 1 && res.data != null)
                    result = _mapper.Map<List<VM_SelectDropDown>>(res.data);
            }
            ViewBag.DistrictId = new SelectList(result, "Id", "Name", selectedId ?? 0);
        }

        protected async Task SetDropDownWard(int? selectedId, int? districtId = 0)
        {
            List<VM_SelectDropDown> result = new List<VM_SelectDropDown>();
            if (districtId != 0)
            {
                var res = await HttpContext?.RequestServices.GetService<IS_DistributeAdministrativeUnit>().GetListWardByStatusDistrictId(districtId ?? 0);
                if (res.result == 1 && res.data != null)
                    result = _mapper.Map<List<VM_SelectDropDown>>(res.data);
            }
            ViewBag.WardId = new SelectList(result, "Id", "Name", selectedId ?? 0);
        }

        protected void SetDropDownTypeProduct(EN_TypeSearchProduct selectedId = 0)
        {
            List<VM_SelectDropDown> result = new List<VM_SelectDropDown>()
            {
                new VM_SelectDropDown
                {
                    Id = $"{(int)EN_TypeSearchProduct.All}",
                    Name = "Tất cả"
                },
                new VM_SelectDropDown
                {
                    Id = $"{(int)EN_TypeSearchProduct.New}",
                    Name = "Mới nhất"
                },
                new VM_SelectDropDown
                {
                    Id = $"{(int)EN_TypeSearchProduct.Hot}",
                    Name = "Nổi bật"
                },
                new VM_SelectDropDown
                {
                    Id = $"{(int)EN_TypeSearchProduct.PriceDesc}",
                    Name = "Giá cao"
                },
                new VM_SelectDropDown
                {
                    Id = $"{(int)EN_TypeSearchProduct.PriceAsc}",
                    Name = "Giá thấp"
                },
            };
            ViewBag.TypeProduct = new SelectList(result, "Id", "Name", selectedId);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            MemoryCacheEntryOptions cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                Priority = CacheItemPriority.Normal,
                //SlidingExpiration = TimeSpan.FromMinutes(5),
                Size = 1024
            };

            if (!_memoryCache.TryGetValue(CommonConstants.CACHE_KEY_PORTAL, out ResponseData<M_Supplier> supplierInfo))
            {
                supplierInfo = HttpContext?.RequestServices.GetService<IS_Supplier>().GetById(_supplierId).Result;
                if (supplierInfo.result == 1 && supplierInfo.data != null)
                    _memoryCache.Set(CommonConstants.CACHE_KEY_PORTAL, supplierInfo, cacheExpiryOptions);
            }

            if (!_memoryCache.TryGetValue(CommonConstants.CACHE_KEY_PRODUCTCATEGORY, out ResponseData<List<M_ProductCategory>> productCategory))
            {
                productCategory = HttpContext?.RequestServices.GetService<IS_ProductCategory>().GetListMenuBySupplierId(_supplierId).Result;
                if (productCategory.result == 1 && productCategory.data != null)
                    _memoryCache.Set(CommonConstants.CACHE_KEY_PRODUCTCATEGORY, productCategory, cacheExpiryOptions);
            }

            if (!_memoryCache.TryGetValue(CommonConstants.CACHE_KEY_NEWSCATEGORY, out ResponseData<List<M_NewsCategory>> newsCategory))
            {
                newsCategory = HttpContext?.RequestServices.GetService<IS_NewsCategory>().GetListBySupplierId(_supplierId).Result;
                if (newsCategory.result == 1 && newsCategory.data != null)
                    _memoryCache.Set(CommonConstants.CACHE_KEY_NEWSCATEGORY, newsCategory, cacheExpiryOptions);
            }
            
            if (!_memoryCache.TryGetValue(CommonConstants.CACHE_KEY_PACKAGE, out string package))
            {
                package = _package;
                if (package != null)
                    _memoryCache.Set(CommonConstants.CACHE_KEY_PACKAGE, package, cacheExpiryOptions);
            }

            ViewBag.SupplierInfo = supplierInfo.data ?? new M_Supplier();
            ViewBag.MenuProductCategory = productCategory.data ?? new List<M_ProductCategory>();
            ViewBag.MenuNewsCategory = newsCategory.data ?? new List<M_NewsCategory>();
            ViewBag.Package = package;
            base.OnActionExecuting(context);
        }
    }
}
