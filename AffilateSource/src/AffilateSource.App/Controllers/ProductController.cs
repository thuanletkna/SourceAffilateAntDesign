using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Product;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AffilateSource.App.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductServices _productApiClient;
        public readonly ICategoriesServices _categoryApiClient;
        public ProductController(IProductServices productApiClient, ICategoriesServices categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllProductPaging(int id, int page = 1)
        {
            var pageSize = 15;

            //var category = await _categoryApiClient.GetCategoryById(id);
            //var productListAll = await _productApiClient.GetAllProductPaging(page, pageSize);
            var danhmuckhuyenmai = await _categoryApiClient.GetCategoryByParentId(2);
            var productListAllbyCategory = await _productApiClient.GetAllProductByCategoryIdPaging(id, page, pageSize);
            var viewmodel = new ProductViewModel()
            {
                //ProductListAll = productListAll,
                ProductListAllByCategoryid = productListAllbyCategory,
                GetDanhMucKhuyenMai = (System.Collections.Generic.List<Shared.ViewModel.Category.CategoryQuickVM>)danhmuckhuyenmai
            };
            return View(viewmodel);
        }
        public async Task<IActionResult> GetAllProductsPaging(int id, int page = 1)
        {
            var pageSize = 2;

            //var category = await _categoryApiClient.GetCategoryById(id);
            //var productListAll = await _productApiClient.GetAllProductPaging(page, pageSize);
            var danhmuckhuyenmai = await _categoryApiClient.GetCategoryByParentId(2);
            var productListAllbyCategory = await _productApiClient.GetAllProductByCategoryIdPaging(id, page, pageSize);
            var viewmodel = new ProductViewModel()
            {
                //ProductListAll = productListAll,
                ProductListAllByCategoryid = productListAllbyCategory,
                GetDanhMucKhuyenMai = (System.Collections.Generic.List<Shared.ViewModel.Category.CategoryQuickVM>)danhmuckhuyenmai
            };
            return View(viewmodel);
        }
        public async Task<IActionResult> GetProductById(int id)
        {
            var detailProduct = await _productApiClient.GetProductById(id);
            var viewmodel = new ProductViewModel()
            {
                GetProductById = detailProduct,
            };
            return View(viewmodel);
        }
        
    }
}
