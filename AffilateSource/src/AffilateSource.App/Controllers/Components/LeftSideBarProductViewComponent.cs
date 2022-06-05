using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Component;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AffilateSource.App.Controllers.Components
{
    public class LeftSideBarProductViewComponent : ViewComponent
    {
        private readonly ICategoriesServices _categoryApiClient;
        private readonly IPostServices _postApiClient;
        public LeftSideBarProductViewComponent(ICategoriesServices categoryApiClient, IPostServices postApiClient)
        {
            _categoryApiClient = categoryApiClient;
            _postApiClient = postApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApiClient.GetCategoryHome();
            var categoriesByParentId = await _categoryApiClient.GetCategoryByParentId(2);
            var postViewCount = await _postApiClient.GetPostByViewCount();
            var viewmodel = new LeftSideBarViewModel()
            {
                ListcategorySidebar = (System.Collections.Generic.List<Shared.ViewModel.Category.CategoryQuickVM>)categories,
                ListcategoryProductSidebar = (System.Collections.Generic.List<Shared.ViewModel.Category.CategoryQuickVM>)categoriesByParentId,
                ListPostViewCount = postViewCount,
            };
            return View("Default", viewmodel);
        }
    }
}
