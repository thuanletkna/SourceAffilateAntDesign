using AffilateSource.Data.DataEntity;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Category;
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
        private readonly ApplicationDbContext _context;
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
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CategoryQuickVM slideImageVm)
        {
            var slide = await _categoriesServices.CreateCategory(slideImageVm);
            return Ok(slide);
        }
        [HttpPost("UpdateCategories")]
        public async Task<IActionResult> UpdateCategories([FromBody] CategoryQuickVM request)
        {
            var post = await _categoriesServices.UpdateCategories(request);
            return Ok(post);
        }
        [HttpPost("GetCategoryDetailById")]
        public async Task<IActionResult> GetCategoryDetailById([FromBody] int id)
        {
            var post = await _categoriesServices.GetCategoryDetailById(id);
            return Ok(post);
        }
        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory()
        {
            var category = await _categoriesServices.GetCategoryHome();
            var data = category.Where(x=> x.Level == 1).ToList();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
    }
}
