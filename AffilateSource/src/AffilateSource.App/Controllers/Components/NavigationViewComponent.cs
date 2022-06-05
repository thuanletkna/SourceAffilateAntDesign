using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Category;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AffilateSource.App.Controllers.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ICategoriesServices _categoryApiClient;
        public NavigationViewComponent(ICategoriesServices categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApiClient.GetCategoryHome();
            var viewmodel = new NavigationViewModel()
            {
                ListcategoryHome = (List<CategoryQuickVM>)categories,
            };
            return View("Default", viewmodel);
        }
    }
}
