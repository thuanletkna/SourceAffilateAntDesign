using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Component;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AffilateSource.App.Controllers.Components
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ICategoriesServices _categoryApiClient;
        private readonly IPostServices _postApiClient;
        public FooterViewComponent(ICategoriesServices categoryApiClient, IPostServices postApiClient)
        {
            _categoryApiClient = categoryApiClient;
            _postApiClient = postApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var postViewCount = await _postApiClient.GetPostByViewCount(); //Get 3 post view count
            var viewmodel = new FooterViewModel()
            {
                ListPostViewCount = postViewCount
            };
            return View("Default", viewmodel);
        }
    }
}
