using AffilateSource.Shared.ViewModel;
using AffilateSource.Shared.ViewModel.BannerImages;
using AffilateSource.Shared.ViewModel.Category;
using AffilateSource.Shared.ViewModel.Contact;
using AffilateSource.Shared.ViewModel.Filter;
using AffilateSource.Shared.ViewModel.Post;
using AffilateSource.Shared.ViewModel.Product;
using AffilateSource.Shared.ViewModel.Status;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Telerik.DataSource;

namespace AffilateSource.Client.Services
{
    public class PostApiServices
    {
        [Inject]
        private HttpClient Http { get; set; }

        public PostApiServices(HttpClient client)
        {
            Http = client;
        }
        public async Task<DataEnvelope<PostHomeViewModel>> GetDataPostAdminAsync(string controller, string action, DataSourceRequest Request)
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return await response.Content.ReadFromJsonAsync<DataEnvelope<PostHomeViewModel>>();

            throw new Exception($"The service returned with status {response.StatusCode}");
        }
        public async Task<DataEnvelope<ProductHomeViewModel>> GetDataProductAdminAsync(string controller, string action, DataSourceRequest Request)
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return await response.Content.ReadFromJsonAsync<DataEnvelope<ProductHomeViewModel>>();

            throw new Exception($"The service returned with status {response.StatusCode}");
        }
        public async Task<ProductCreateViewModel> GetDetailProductByIdAdmin(string controller, string action, int id)
        {

            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<ProductCreateViewModel>();
                return content;
            }
            else
            {
                return new ProductCreateViewModel();
            }
        }
        public async Task<ListSelectModel> GetDataSelectFilterAdmin(string controller, string action)
        {
            ListSelectModel listSelectModel = new ListSelectModel();
            listSelectModel.ListCategory = new List<SelectModel>();
            var getdataSelect = await Http.GetFromJsonAsync<ListColumnSelectModel>(controller + "/" + action);
            getdataSelect.ListCategoryFilter.ForEach(d => listSelectModel.ListCategory.Add(new SelectModel() { DisplayText = d.CategoryName, ValueText = Convert.ToInt32(d.Id) }));
            return listSelectModel;
        }
        public async Task<List<CategoriesSelectViewModel>> GetdataSelectCategoryByParentId(string controller, string action, int parentId)
        {

            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, parentId);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<List<CategoriesSelectViewModel>>();
                return content;
            }
            else
            {
                return new List<CategoriesSelectViewModel>();
            }
        }
        public async Task<List<ProductSelectViewModel>> GetdataSelectProductByCategoryId(string controller, string action, int categoryId)
        {

            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, categoryId);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<List<ProductSelectViewModel>>();
                return content;
            }
            else
            {
                return new List<ProductSelectViewModel>();
            }
        }
        public async Task<List<PostHomeViewModel>> GetDataIdPostCreated(string controller, string action)
        {
            HttpResponseMessage response = await Http.GetAsync(controller + "/" + action);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<List<PostHomeViewModel>>();
                return content;
            }
            else
            {
                return new List<PostHomeViewModel>();
            }
        }
        public async Task<List<StatusVm>> GetDataSelectStatus(string controller, string action)
        {
            HttpResponseMessage response = await Http.GetAsync(controller + "/" + action);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<List<StatusVm>>();
                return content;
            }
            else
            {
                return new List<StatusVm>();
            }
        }
        public async Task<List<PostDetailVm>> GetPostDetailById(string controller, string action)
        {
            HttpResponseMessage response = await Http.GetAsync(controller + "/" + action);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<List<PostDetailVm>>();
                return content;
            }
            else
            {
                return new List<PostDetailVm>();
            }
        }
        public async Task<PostCreateViewModel> GetPostDetailByIdAdmin(string controller, string action, int id)
        {

            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<PostCreateViewModel>();
                return content;
            }
            else
            {
                return new PostCreateViewModel();
            }
        }
        public async Task<List<PostDetailVm>> GetListPostDetailByIdAdmin(string controller, string action, int id)
        {

            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<List<PostDetailVm>>();
                return content;
            }
            else
            {
                return new List<PostDetailVm>();
            }
        }


        public async Task<PostDetailVm> GetDetailByPostDetailIdAdmin(string controller, string action, int id)
        {

            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<PostDetailVm>();
                return content;
            }
            else
            {
                return new PostDetailVm();
            }
        }

        #region Categories
        public async Task<DataEnvelope<CategoryQuickVM>> GetDataCategoryAdminAsync(string controller, string action, DataSourceRequest Request)
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return await response.Content.ReadFromJsonAsync<DataEnvelope<CategoryQuickVM>>();

            throw new Exception($"The service returned with status {response.StatusCode}");
        }
        public async Task<List<CategoryQuickVM>> GetCategoriesByParentIdAdmin(string controller, string action, int categoryId)
        {

            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, categoryId);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<List<CategoryQuickVM>>();
                return content;
            }
            else
            {
                return new List<CategoryQuickVM>();
            }
        }
        public async Task<List<CategoryQuickVM>> GetListCategories(string controller, string action)
        {

            HttpResponseMessage response = await Http.GetAsync(controller + "/" + action);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<List<CategoryQuickVM>>();
                return content;
            }
            else
            {
                return new List<CategoryQuickVM>();
            }
        }

        public async Task<CategoryQuickVM> GetCategoryDetailByIdAdmin(string controller, string action, int id)
        {

            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<CategoryQuickVM>();
                return content;
            }
            else
            {
                return new CategoryQuickVM();
            }
        }

        #endregion Categories


        #region Slide
        public async Task<DataEnvelope<SlideImageVm>> GetDataSlideAdminAsync(string controller, string action, DataSourceRequest Request)
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return await response.Content.ReadFromJsonAsync<DataEnvelope<SlideImageVm>>();

            throw new Exception($"The service returned with status {response.StatusCode}");
        }
        public async Task<DataEnvelope<BannerImageCreateUpdate>> GetDataBannerImageAdminAsync(string controller, string action, DataSourceRequest Request)
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return await response.Content.ReadFromJsonAsync<DataEnvelope<BannerImageCreateUpdate>>();

            throw new Exception($"The service returned with status {response.StatusCode}");
        }
        public async Task<SlideImageVm> GetSlideDetailByIdAdmin(string controller, string action, int id)
        {

            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<SlideImageVm>();
                return content;
            }
            else
            {
                return new SlideImageVm();
            }
        }
        public async Task<BannerImageCreateUpdate> GetBannerImageById(string controller, string action, int id)
        {

            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<BannerImageCreateUpdate>();
                return content;
            }
            else
            {
                return new BannerImageCreateUpdate();
            }
        }
        #endregion Slide

        public async Task<List<ContactVm>> GetContact(string controller, string action)
        {
            HttpResponseMessage response = await Http.GetAsync(controller + "/" + action);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<List<ContactVm>>();
                return content;
            }
            else
            {
                return new List<ContactVm>();
            }
        }

        public async Task<ContactVm> GetContactDetailByIdAdmin(string controller, string action, int id)
        {

            HttpResponseMessage response = await Http.PostAsJsonAsync(controller + "/" + action, id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadFromJsonAsync<ContactVm>();
                return content;
            }
            else
            {
                return new ContactVm();
            }
        }
    }
}
