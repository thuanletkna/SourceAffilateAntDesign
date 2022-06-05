using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Component;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AffilateSource.App.Controllers.Components
{
    public class LeftSideBarViewComponent : ViewComponent
    {
        private readonly ICategoriesServices _categoryApiClient;
        private readonly IPostServices _postApiClient;
        public LeftSideBarViewComponent(ICategoriesServices categoryApiClient, IPostServices postApiClient)
        {
            _categoryApiClient = categoryApiClient;
            _postApiClient = postApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApiClient.GetCategoryHome();
            var categoriesPost = await _categoryApiClient.GetCategoryByParentId(3);
            var postViewCount = await _postApiClient.GetPostByViewCount();
            var viewmodel = new LeftSideBarViewModel()
            {
                ListcategorySidebar = (System.Collections.Generic.List<Shared.ViewModel.Category.CategoryQuickVM>)categories,
                ListcategoryPostSidebar = (System.Collections.Generic.List<Shared.ViewModel.Category.CategoryQuickVM>)categoriesPost,
                ListPostViewCount = postViewCount,
            };
            return View("Default", viewmodel);
        }
    }
}
