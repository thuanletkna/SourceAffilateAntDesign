using AffilateSource.Data.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AffilateSource.App.Controllers
{

    public class ContactController : Controller
    {
        public IContactServices contactServices;
        public ContactController(IContactServices contactServices)
        {
            this.contactServices = contactServices;
        }
        public async Task<IActionResult> Index()
        {
            var contact =await contactServices.GetContact();
            return View(contact);
        }
    }
}
