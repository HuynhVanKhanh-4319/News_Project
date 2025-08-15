using Microsoft.AspNetCore.Mvc;
using Web_ProjectName.Models;
using Web_ProjectName.Services;

namespace Web_ProjectName.Controllers
{
    public class IntroduceController : BaseController<IntroduceController>
    {
        private readonly IS_Introduce _s_Introduce;
        public IntroduceController(IS_Introduce s_Introduce)
        {
            _s_Introduce = s_Introduce ;
        }

        public async Task<IActionResult> Index()
        {
         
            var response = await _s_Introduce.GetById(default, 1); 
                return View(response.data);
 
        }
    }

}
