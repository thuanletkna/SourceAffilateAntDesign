using AffilateSource.Data.DataEntity;
using AffilateSource.Data.DataEntity.Entities;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Data.Services.ServicesConfig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AffilateSource.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Bearer")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesServices _categoriesServices;
        //private readonly ISequenceService _sequenceService;
        private readonly ApplicationDbContext _context;
        //private readonly IStorageService _storageService;
        public CategoryController(ICategoriesServices categoriesServices,  ApplicationDbContext context)
        {
            _categoriesServices = categoriesServices;
            //_sequenceService = sequenceService;
            _context = context;
            //_storageService = storageService;
        }
        
        [HttpGet("GetCategoryHome")]
        public async Task<IActionResult> GetCategoryHome()
        {
            var category = await _categoriesServices.GetCategoryHome();
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }

        [HttpGet("GetCategoryParent")]
    
        public async Task<IActionResult> GetCategoryParent()
        {
            var categoryParent = await _categoriesServices.GetCategoryParent();
            if (categoryParent == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(categoryParent);
            }
        }

        [HttpGet("GetCategoryByParentId/{parentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryByParentId(int parentId)
        {
            var category = await _categoriesServices.GetCategoryByParentId(parentId);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }
        
    }
}
