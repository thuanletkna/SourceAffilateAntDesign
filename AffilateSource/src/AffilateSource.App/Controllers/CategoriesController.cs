using AffilateSource.Data.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AffilateSource.App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly ICategoriesServices _categoryApiClient;
        public CategoriesController(ICategoriesServices categoryApiClient)
        {

            _categoryApiClient = categoryApiClient;
        }
        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryApiClient.GetCategoryByParentId(2);
            return Ok(category);
        }
    }
}
