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
      
        var createdBy = 0; 
        var res = await _s_NewsCategory.Create(default, model, createdBy.ToString());
        return Json(res);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id)
    {
        var updatedBy = 0;
        var result = await _s_NewsCategory.UpdateStatus(default, id, 0, updatedBy.ToString());
        return Json(new { success = result != null});
    }








}
