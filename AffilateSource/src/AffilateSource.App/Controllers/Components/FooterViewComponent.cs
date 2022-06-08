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

        public FooterViewComponent(ICategoriesServices categoryApiClient, IPostServices postApiClient, IContactServices contactServices)
        {
            _categoryApiClient = categoryApiClient;
            _postApiClient = postApiClient;
            _contactServices = contactServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var postViewCount = await _postApiClient.GetPostByViewCount(); //Get 3 post view count
            var contact = await _contactServices.GetContact();
            var viewmodel = new FooterViewModel()
            {
                ListPostViewCount = postViewCount,
                getContact = contact
            };
            return View("Default", viewmodel);
        }
    }
}
