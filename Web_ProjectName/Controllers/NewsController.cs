using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_ProjectName.Models;
using Web_ProjectName.Services;

namespace Web_ProjectName.Controllers
{

    public class NewsController : Controller
    {
        private readonly IS_News _s_News;
        private readonly IS_NewsCategory _newsCategoryService;

        public NewsController(IS_News s_News ,IS_NewsCategory newsCategoryService)
        {
            _s_News = s_News;
            _newsCategoryService = newsCategoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] List<int> status, [FromQuery] int? categoryId)
        {
            var allData = new List<M_News>();

        
            foreach (var s in status)
            {
                var result = await _s_News.GetListByStatus(default, s);
                if (result?.data != null)
                    allData.AddRange(result.data);
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                allData = allData
                    .Where(x => x.newsCategoryId == categoryId.Value)
                    .ToList();
            }

            return Json(new { data = allData });
        }


        public async Task<IActionResult> Create()
        {

            return View(new EM_News());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EM_News model)
        {
            var createdBy = 0; 

            var result = await _s_News.Create(default, model, createdBy.ToString());
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EM_News model)
        {
            var updatedBy = 0;
            var res = await _s_News.Update(default, model, updatedBy.ToString());
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _s_News.GetById(id);
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, int status)
        {
            var updatedBy = 0; 
            var result = await _s_News.UpdateStatus(default, id, status, updatedBy.ToString());
            return Json(new { success = result != null });
        }

        [HttpPost]
        public IActionResult RenderDetailView([FromBody] EM_News model)
        {           
            return PartialView("P_News_Detail", model);
        }
        [HttpPost]
        public IActionResult RenderUpdateView([FromBody] EM_News model)
        {
            return PartialView("P_News_Update", model);
        }


    }

}
