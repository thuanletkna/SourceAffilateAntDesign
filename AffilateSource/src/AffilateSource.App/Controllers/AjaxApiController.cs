using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Post;
using AffilateSource.Shared.ViewModel.Product;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AffilateSource.App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AjaxApiController : ControllerBase
    {
        private readonly ICategoriesServices _categoryApiClient;
        private readonly IProductServices _productApiClient;
        private readonly IPostServices _postApiClient;
        public AjaxApiController(IProductServices productApiClient, IPostServices postApiClient, ICategoriesServices categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
            _productApiClient = productApiClient;
            _postApiClient = postApiClient;
        }

        //Lấy danh sách sản phẩm trang chủ by danh mục
        [HttpGet("GetProductByCategoryIdAjax")]
        public async Task<IActionResult> GetProductByCategoryIdAjax(int id)
        {
            IEnumerable<ProductHomeViewModel> detailProduct;
            if (id == 0)
            {
                var category = await _categoryApiClient.GetCategoryByParentId(2);  // Danh mục sản phẩm = 2
                var data = category.Select(x => x.Id).First();  // Lấy id danh mục đầu tiên
                detailProduct = await _productApiClient.GetProductByCategoryIdHomeTop(data);  // Lấy danh sách sp của danh mục đầu tiên để hiển thị nếu id truyền vào  = 0
                return Ok(detailProduct);
            }
            detailProduct = await _productApiClient.GetProductByCategoryIdHomeTop(id);
            return Ok(detailProduct);
        }
        [HttpGet("GetProductCategoryById")]
        public async Task<IActionResult> GetProductCategoryById(int id)
        {
            var category = await _categoryApiClient.GetCategoryByParentId(2);
            return Ok(category);
        }
        [HttpGet("GetPostCategoryById")]
        public async Task<IActionResult> GetPostCategoryById(int id)
        {
            var category = await _categoryApiClient.GetCategoryByParentId(3);
            return Ok(category);
        }
        [HttpGet("GetPostByCategoryIdAjax")]
        public async Task<IActionResult> GetPostByCategoryIdAjax(int id)
        {
            IEnumerable<PostHomeViewModel> post;
            if (id == 0)
            {
                var category = await _categoryApiClient.GetCategoryByParentId(3);  // Danh mục sản phẩm = 3
                var data = category.Select(x => x.Id).First();  // Lấy id danh mục đầu tiên
                post = await _postApiClient.GetPostByCreateDate(data);  // Lấy danh sách sp của danh mục đầu tiên để hiển thị nếu id truyền vào  = 0
                return Ok(post);
            }
            post = await _postApiClient.GetPostByCreateDate(id);
            return Ok(post);
        }
    }
}
