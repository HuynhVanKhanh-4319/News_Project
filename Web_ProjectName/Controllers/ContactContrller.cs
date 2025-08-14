using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Web_ProjectName.ExtensionMethods;
using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using Web_ProjectName.Services;

namespace Web_ProjectName.Controllers
{
    public class ContactContrller : Controller
    {
        private readonly IS_Contact _s_Contact;
        public ContactContrller(IS_Contact s_Contact)
        {
            _s_Contact = s_Contact;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Send(EM_Contact model, string tokenReCAPTCHA)
        {
            M_JResult jResult = new M_JResult();


            if (!ModelState.IsValid)
            {
                jResult.error = new error(0, DataAnnotationExtensionMethod.GetErrorMessage(ModelState));
                return Json(jResult);
            }
            model.supplierId = 0;
            model.status = 0;
            var res = await _s_Contact.Create(model, default);
            return Json(jResult.MapData(res));
        }
    }
}
