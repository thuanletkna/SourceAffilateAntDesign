using AffilateSource.Data.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AffilateSource.App.Controllers
{

    public class ContactController : Controller
    {
        public IContactServices contactServices;
        public ContactController(IContactServices contactServices)
        {
            this.contactServices = contactServices;
        }
        public IActionResult Index()
        {
            var contact = contactServices.GetContact();
            return View(contact);
        }
    }
}
