using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AffilateSource.App.Models;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Home;

namespace AffilateSource.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoriesServices _categoryApiClient;
        private readonly IProductServices _productApiClient;
        private readonly IPostServices _postApiClient;
        public HomeController(ILogger<HomeController> logger, IProductServices productApiClient, IPostServices postApiClient, ICategoriesServices categoryApiClient)
        {
            _logger = logger;
            _categoryApiClient = categoryApiClient;
            _productApiClient = productApiClient;
            _postApiClient = postApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var categoryHome = await _categoryApiClient.GetCategoryHome();
            var productHome = await _productApiClient.GetProductHome();
            var productbyViewCount = await _productApiClient.GetProductByViewCount();
            var postHome = await _postApiClient.GetPostHome();
            var postCreateDate = await _postApiClient.GetPostByCreateDate(40);  //Get bài đăng mới nhất của danh mục chăm sóc cây có id là 40
            var magiamgia = await _categoryApiClient.GetCategoryByParentId(6);
            var danhmuckhuyenmai = await _categoryApiClient.GetCategoryByParentId(2);
            var danhmucpost = await _categoryApiClient.GetCategoryByParentId(3);
            var viewmodel = new HomeViewModel()
            {
                GetcategoryByParentId = (List<Shared.ViewModel.Category.CategoryQuickVM>)magiamgia,
                GetDanhMucKhuyenMai = (List<Shared.ViewModel.Category.CategoryQuickVM>)danhmuckhuyenmai,
                GetDanhMucKihNghiem = (List<Shared.ViewModel.Category.CategoryQuickVM>)danhmucpost,
                GetCategoryHome = (List<Shared.ViewModel.Category.CategoryQuickVM>)categoryHome,
                GetProductHome = productHome,
                GetPostHome = postHome,
                GetProductByViewCount = productbyViewCount,
                GetPostCreateDate = postCreateDate,
            };
            return View(viewmodel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
