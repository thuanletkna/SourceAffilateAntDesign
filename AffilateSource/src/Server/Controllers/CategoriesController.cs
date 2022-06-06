using AffilateSource.Data.DataEntity;
using AffilateSource.Data.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesServices _categoriesServices;
        //private readonly ISequenceService _sequenceService;
        private readonly ApplicationDbContext _context;
        //private readonly IStorageService _storageService;
        public CategoriesController(ICategoriesServices categoriesServices, ApplicationDbContext context)
        {
            _categoriesServices = categoriesServices;
            _context = context;
        }
        
        [HttpPost("GetCategoriesByParentId")]
        public async Task<IActionResult> GetCategoriesByParentId([FromBody] int parentId)
        {
            var categoryParents = await _categoriesServices.GetCategoriesByParentId(parentId);
            if (categoryParents == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(categoryParents);
            }
        }
        [HttpPost("GetCategoryPagingFilterAdmin")]
        public async Task<DataSourceResult> GetCategoryPagingFilterAdmin([FromBody] DataSourceRequest request)
        {
            var post = await _categoriesServices.GetCategoryPagingFilterAdmin(request);
            return post;
        }

        [HttpPost("GetCategoriesByParentIdAdmin")]
        public async Task<IActionResult> GetCategoriesByParentIdAdmin([FromBody] int parentId)
        {
            var categoryParents = await _categoriesServices.GetCategoriesByParentIdAdmin(parentId);
            if (categoryParents == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(categoryParents);
            }
        }

    }
}
