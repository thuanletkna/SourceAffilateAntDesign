using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Post;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AffilateSource.App.Controllers
{
    public class PostController : Controller
    {
        public readonly IPostServices _postApiClient;
        public readonly ICategoriesServices _categoryApiClient;
        private readonly IBannerImageServices _bannerImageServices;
        public PostController(IPostServices postApiClient, ICategoriesServices categoryApiClient, IBannerImageServices bannerImageServices)
        {

            _postApiClient = postApiClient;
            _categoryApiClient = categoryApiClient;
            _bannerImageServices = bannerImageServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllPostPaging(int id, int page = 1)
        {
            var pageSize = 12;
            //var category = await _categoryApiClient.GetCategoryById(id);
            var postListAll = await _postApiClient.GetAllPostPaging(page, pageSize);
            var postListAllByCategoryId = await _postApiClient.GetAllPostByCategoryIdPaging(id, page, pageSize);
            var danhmucpost = await _categoryApiClient.GetCategoryByParentId(3);
            var bannerImage = await _bannerImageServices.GetBannerImages();
            var viewmodel = new PostViewModel()
            {
                PostListAll = postListAll,
                GetAllPostByCategoryIdPaging = postListAllByCategoryId,
                GetDanhMucKinhNghiem = (System.Collections.Generic.List<Shared.ViewModel.Category.CategoryQuickVM>)danhmucpost,
                BannerImageVm = bannerImage.BannerImageVm,
            };
            return View(viewmodel);
        }
        public async Task<IActionResult> GetPostById(int id)
        {
            var detailPost = await _postApiClient.GetPostById(id);
            var detailPostbyId = await _postApiClient.GetPostDetailByPostId(detailPost.Id);
            var bannerImage = await _bannerImageServices.GetBannerImages();
            var viewmodel = new PostViewModel()
            {
                GetPostById = detailPost,
                DetailPostbyId = (System.Collections.Generic.List<Shared.ViewModel.Post.ListPostDetailVm>)detailPostbyId,
                BannerImageVm = bannerImage.BannerImageVm,
            };
            return View(viewmodel);
        }
       
    }
}
