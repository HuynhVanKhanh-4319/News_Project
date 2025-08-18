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

        [HttpGet]
        public IActionResult GetDataByTeam(string teamId)
        {
            var data = new List<object>();

            if (teamId == "D1")
            {
                data = new List<object>
                {
                    new { name = "D1-C1", value = 1274.05 },
                    new { name = "D1-C2", value = 1267.02 },
                    new { name = "D1-C3", value = 3068.49 },
                    new { name = "D1-C4", value = 2710.16 },
                    new { name = "D1-C5", value = 1987.89 },
                    new { name = "D1-C6", value = 1879.69 },
                    new { name = "D1-C7", value = 4629.67 },
                    new { name = "D1-C8", value = 3783.74 }
                };
            }
            else if (teamId == "D2")
            {
                data = new List<object>
                {
                    new { name = "D2-C1", value = 1500 },
                    new { name = "D2-C2", value = 2000 },
                    new { name = "D2-C3", value = 1800 }
                };
            }

            return Json(data);
        }


    }
}
