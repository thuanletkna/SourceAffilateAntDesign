using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.BannerImages;
using AffilateSource.Shared.ViewModel.Contact;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AffilateSource.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactServices _contactServices;
        public ContactController(IContactServices contactServices)
        {
            _contactServices = contactServices;
        }
        [HttpPost("CreateContacts")]
        public async Task<IActionResult> CreateContacts(ContactVm contactVm)
        {
            var slide = await _contactServices.CreateContacts(contactVm);
            return Ok(slide);
        }
        [HttpPost("UpdateContacts")]
        public async Task<IActionResult> UpdateContacts(ContactVm contactVm)
        {
            var slide = await _contactServices.UpdateContacts(contactVm);
            return Ok(slide);
        }
        [HttpPost("GetContactById")]
        public async Task<IActionResult> GetContactById([FromBody] int id)
        {
            var slide = await _contactServices.GetContactById(id);
            return Ok(slide);
        }
        [HttpGet("GetListContact")]
        public async Task<IActionResult> GetListContact()
        {
            var slide = await _contactServices.GetListContact();
            return Ok(slide);
        }
    }
}
