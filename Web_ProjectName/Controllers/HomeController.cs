using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web_ProjectName.Models;
using Web_ProjectName.Services;
using Web_ProjectName.Lib;
using Web_ProjectName.ViewModels;

namespace Web_ProjectName.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private readonly IS_Product _s_Product;
        private readonly IS_Product _s_News;
        private readonly IOptions<Config_MetaSEO> _metaSEO;

        public HomeController(IS_Product product, IS_Product news, IOptions<Config_MetaSEO> metaSEO)
        {
            _s_Product = product;
            _s_News = news;
            _metaSEO = metaSEO;
        }

        public async Task<IActionResult> Index()
        {
           
            return View();
        }
        public async Task<IActionResult> Element()
        {
   
            return View();
        }
        public async Task<IActionResult> Widget()
        {
            return View();
        }
        public async Task<IActionResult> Form()
        {
            return View();
        }
        public async Task<IActionResult> Button()
        {
            return View();
        }
        public async Task<IActionResult> Table()
        {
            return View();
        }
        public async Task<IActionResult> Chart()
        {
            return View();
        }
        [HttpGet]
        public async Task GetListProduct()
        {
            var res = await _s_Product.GetListByPaging("1", _supplierId, "1", default, EN_TypeSearchProduct.Hot, 1, 2);
            if (res.result == 1 && res.data != null)
            {
                ViewBag.ListIntroduce = res.data;
                //Đây là code để mô tả việc mapper, chỉ minh họa
                //ViewBag.ListIntroduce = res.data != null ? _mapper.Map<List<VM_IntroduceHome>>(res.data) : new List<VM_IntroduceHome>();
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetListProductByAjax(string supplierId)
        {
            var res = await _s_Product.GetListByPaging("1", _supplierId, "1", default, EN_TypeSearchProduct.Hot, 1, 2);
            return Json(new M_JResult(res));
        }

   
    }
    }
