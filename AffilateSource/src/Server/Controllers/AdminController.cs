using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Filter;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AffilateSource.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICategoriesServices _categoriesServices;
        public AdminController(ICategoriesServices categoriesServices)
        {
            _categoriesServices = categoriesServices;
        }

        [HttpGet("GetSelectListFilter")]
        public async Task<IActionResult> GetSelectListFilter()
        {
            ListColumnSelectModel listColumnSelectModel = new ListColumnSelectModel();
            var category = await _categoriesServices.GetAllCategoryFilter();
            listColumnSelectModel.ListCategoryFilter = category.ListCategoryFilter.ToList();
            return Ok(listColumnSelectModel);
        }
    }
}
