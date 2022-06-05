using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Product;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateViewModel request)
        {
            var product = await _productServices.CreateProduct(request);
            return Ok(product);
        }
        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductCreateViewModel request)
        {
            var product = await _productServices.UpdateProduct(request);
            return Ok(product);
        }
        [HttpPost("GetProductPagingFilterAdmin")]
        public async Task<DataSourceResult> GetProductPagingFilterAdmin([FromBody] DataSourceRequest request)
        {
            var productGrid = await _productServices.GetProductPagingFilterAdmin(request);
            return productGrid;
        }
        [HttpPost("GetProductSelectByCategoryId")]
        public async Task<IActionResult> GetProductSelectByCategoryId([FromBody] int categoryId)
        {
            var categoryParents = await _productServices.GetProductSelectByCategoryId(categoryId);
            if (categoryParents == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(categoryParents);
            }
        }
        [HttpPost("GetDetailsProductUpdateById")]
        public async Task<IActionResult> GetDetailsProductUpdateById([FromBody] int id)
        {
            var productDetail = await _productServices.GetDetailsProductUpdateById(id);
            if (productDetail == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(productDetail);
            }
        }
    }
}
