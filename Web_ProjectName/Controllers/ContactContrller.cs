using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Web_ProjectName.ExtensionMethods;
using Web_ProjectName.Lib;
using Web_ProjectName.Models;
using Web_ProjectName.Services;

namespace Web_ProjectName.Controllers
{
    public class ContactController : BaseController<ContactController>
    {
        private readonly IS_Contact _s_Contact;
        public ContactController(IS_Contact s_Contact)
        {
            _s_Contact = s_Contact;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(EM_Contact model)
        {
            var createdBy = 0;
            model.status = 1;
            var response = await _s_Contact.Create(model, createdBy.ToString());
            return Json(response);
        }

    }
}
