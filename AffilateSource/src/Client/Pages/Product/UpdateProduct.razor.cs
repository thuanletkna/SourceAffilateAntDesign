using Telerik.Blazor;
using Telerik.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using AffilateSource.Shared.ViewModel.Post;
using AffilateSource.Shared.ViewModel.Category;
using AffilateSource.Shared.ViewModel.Product;
using System.Linq;
using AffilateSource.Shared.ViewModel.Filter;
using Telerik.DataSource;
using AffilateSource.Shared.ViewModel;
using AffilateSource.Shared.Kendohelpers;
using AffilateSource.Shared.ViewModel.Status;
using AntDesign;

namespace AffilateSource.Client.Pages.Product
{
    public partial class UpdateProduct
    {
        [Parameter] public int id { get; set; }

        ProductCreateViewModel productUpdateModel = new ProductCreateViewModel();
        public List<CategoriesSelectViewModel> CategoryParent { get; set; } = new List<CategoriesSelectViewModel>();
        public List<StatusVm> StatusViewModel { get; set; } = new List<StatusVm>();
        public FilterRequest filtterRequest { get; set; } = new FilterRequest { };
        public class FilterRequest
        {
            public object this[string propertyName]
            {
                get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
                set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
            }
            public string Category { get; set; }
            public string StatusId { get; set; } = "1";
            //public string MaCongTy { get; set; } = "CT00001";
            //public IEnumerable<Status> dataSelectStatus { get; set; } = new List<Status>();
            public List<SelectModel> dataSelectCategoryFilter { get; set; } = new List<SelectModel>();

        }
        // file upload image
        public string SaveUrl => ToAbsoluteUrl("api/upload/Save");

        public string RemoveUrl => ToAbsoluteUrl("api/upload/remove");

        public string ToAbsoluteUrl(string url)
        {
            return $"{NavigationManager.BaseUri}{url}";
        }
        private async Task OnSuccessHandler(UploadSuccessEventArgs e, string field)
        {
            if (e.Operation == UploadOperationType.Upload)
            {
                string content = e.Request.ResponseText;
                foreach (var file in e.Files)
                {

                    //postCreateViewModel.Detail = content;
                    productUpdateModel.ImageProducts = content;

                    if (!string.IsNullOrEmpty(productUpdateModel.ImageProducts))
                    {
                        productUpdateModel.ImageProducts = "/Uploads/PostImages/" + productUpdateModel.ImageProducts;
                    }
                    //if (field == "images")
                    //{
                    //    postCreateViewModel.Detail = content;

                    //    if (!string.IsNullOrEmpty(postCreateViewModel.Detail))
                    //    {
                    //        postCreateViewModel.Detail = "/upload" + postCreateViewModel.Detail;
                    //    }
                    //}
                }
            }

        }

        private async Task GetSelectListValue()
        {
            try
            {
                var getDataSelect = await postApi.GetDataSelectFilterAdmin("Admin", "GetSelectListFilter");
                filtterRequest.dataSelectCategoryFilter = getDataSelect.ListCategory;
                //filtterRequest.dataSelectStatus = await StatusServices.GetStatusAsync();
                var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        //Update product

        bool isVisible { get; set; }
        void WindowVisibleUpdate(bool currVisible)
        {
            isVisible = currVisible;
        }
        
        protected override async Task OnInitializedAsync()
        {
            await GetSelectListValue();
            productUpdateModel = await postApi.GetDetailProductByIdAdmin("Product", "GetDetailsProductUpdateById", id);
            CategoryParent = await postApi.GetdataSelectCategoryByParentId("Categories", "GetCategoriesByParentId", 2);
            StatusViewModel = await postApi.GetDataSelectStatus("Status", "GetListSatatus");
        }
        async Task UpdateProductAsync()
        {

            //Kiểm tra phiên đăng nhập và phân quyền
            var checkname = await Http.PostAsJsonAsync("/Product/UpdateProduct", productUpdateModel);
            if (checkname.IsSuccessStatusCode)
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Chỉnh sửa sản phẩm thành công",
                    NotificationType = NotificationType.Success
                });
                NavigationManager.NavigateTo("/");

            }
            else
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Lỗi",
                    NotificationType = NotificationType.Error
                });
            }
        }
    }
}