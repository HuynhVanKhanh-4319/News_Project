using Microsoft.AspNetCore.Mvc;
using Web_ProjectName.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Web_ProjectName.Models;

namespace Web_ProjectName.Controllers
{
    public class NewsUserController : BaseController<NewsUserController>
    {
        private readonly IS_News _s_News;
        private readonly IS_NewsCategory _s_NewsCategory;

        public NewsUserController(IS_News s_News, IS_NewsCategory s_NewsCategory)
        {
            _s_News = s_News;
            _s_NewsCategory = s_NewsCategory;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _s_News.GetListByStatus(default, 1); 
            var hotNews = result?.data?.Where(n => n.isHot == false).ToList() ?? new List<M_News>();

            ViewBag.HotNews = hotNews;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetHotNewsPartial()
        {
            var result = await _s_News.GetListByStatus(default, 1);
            var hotNews = result?.data?.Where(n => n.isHot == false).ToList() ?? new List<M_News>();
            return PartialView("_P_News_Hot", hotNews);
        }
        [HttpGet]
        public async Task<IActionResult> GetList(int status = 1)
        {
            var result = await _s_News.GetListByStatus(default, status);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _s_News.GetById(id);
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(string metaUrl)
        {
            var res = await _s_News.GetByMetaUrl(metaUrl); 
            if (res?.data == null)
            {
                return NotFound();
            }

            var news = res.data;
            var relatedNews = (await _s_News.GetListByStatus(default, 1))?.data?
                .Where(n => n.newsCategoryId == news.newsCategoryId && n.id != news.id)
                .Take(3)
                .ToList() ?? new List<M_News>();

            ViewBag.RelatedNews = relatedNews;

            return View(news);
        }



    }
}
