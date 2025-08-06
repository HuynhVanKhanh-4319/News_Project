using Microsoft.AspNetCore.Mvc;
using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using Web_ProjectName.Services;

public class NewsCategoryController : Controller
{
    private readonly IS_NewsCategory _s_NewsCategory;

    public NewsCategoryController(IS_NewsCategory s_NewsCategory)
    {
        _s_NewsCategory = s_NewsCategory;
    }

    public async Task<IActionResult> Index()
    {

        return View();
    }
    public async Task<IActionResult> GetList(int status)
    {
        var result = await _s_NewsCategory.GetListByStatus(default, status);
        return Json(result);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View(new EM_NewsCategory());
    }
    [HttpPost]
    public async Task<IActionResult> Create(EM_NewsCategory model)
    {
        // không có đăng nhập nên cho nó mặc định là 0
        var createdBy = 0; 
        var res = await _s_NewsCategory.Create(default, model, createdBy.ToString());
        return Json(res);
    }




}
