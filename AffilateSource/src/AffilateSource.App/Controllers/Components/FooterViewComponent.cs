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
        private readonly IContactServices _contactServices;
        private readonly IBannerImageServices _bannerImageServices;

        public FooterViewComponent(ICategoriesServices categoryApiClient, IPostServices postApiClient, IContactServices contactServices, IBannerImageServices bannerImageServices)
        {
            _categoryApiClient = categoryApiClient;
            _postApiClient = postApiClient;
            _contactServices = contactServices;
            _bannerImageServices = bannerImageServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var postViewCount = await _postApiClient.GetPostByViewCount(); //Get 3 post view count
            var contact = await _contactServices.GetContact();
            var bannerImage = await _bannerImageServices.GetBannerImages();
            var viewmodel = new FooterViewModel()
            {
                ListPostViewCount = postViewCount,
                getContact = contact,
                BannerImageVm = bannerImage.BannerImageVm,
            };
            return View("Default", viewmodel);
        }
    }
}
