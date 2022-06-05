using AffilateSource.Data.DataEntity;
using AffilateSource.Data.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            //_sequenceService = sequenceService;
            _context = context;
            //_storageService = storageService;
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
    }
}
