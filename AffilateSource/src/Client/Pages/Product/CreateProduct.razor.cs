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
using AntDesign;
using AffilateSource.Shared.ViewModel.Status;

namespace AffilateSource.Client.Pages.Product
{
    public partial class CreateProduct
    {


        public List<PostHomeViewModel> GridData { get; set; }
        public List<CategoriesSelectViewModel> CategoryParent { get; set; } = new List<CategoriesSelectViewModel>();
        public List<ProductSelectViewModel> productSelect { get; set; } = new List<ProductSelectViewModel>();
        ProductCreateViewModel productCreateViewModel { get; set; } = new ProductCreateViewModel();
        public List<PostDetailVm> DetailPost { get; set; }
        public PostDetailVm DetailPostsModal { get; set; } = new PostDetailVm();
        public PostDetailVm DetailPosts { get; set; }
        public CategoriesSelectViewModel categoryQuickVM { get; set; }
        public ProductSelectViewModel productSelectViewModel { get; set; }
        public PostHomeViewModel DataPostLast { get; set; }
        public List<StatusVm> StatusViewModel { get; set; } = new List<StatusVm>();
        // file upload image
        public string SaveUrl => ToAbsoluteUrl("api/upload/Save");

        public string RemoveUrl => ToAbsoluteUrl("api/upload/remove");

        public string ToAbsoluteUrl(string url)
        {
            return $"{NavigationManager.BaseUri}{url}";
        }
        // Code tạo mới nhân viên
        #region actionCreateEmployee
        async Task CreateProductAsync()
        {

            //Kiểm tra phiên đăng nhập và phân quyền
            var checkname = await Http.PostAsJsonAsync("/Product/CreateProduct", productCreateViewModel);
            if (checkname.IsSuccessStatusCode)
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Thêm mới sản phẩm thành công",
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
        #endregion actionCreateEmployee

        private async Task OnSuccessHandler(UploadSuccessEventArgs e, string field)
        {
            if (e.Operation == UploadOperationType.Upload)
            {
                string content = e.Request.ResponseText;
                foreach (var file in e.Files)
                {

                    //postCreateViewModel.Detail = content;
                    productCreateViewModel.ImageProducts = content;

                    if (!string.IsNullOrEmpty(productCreateViewModel.ImageProducts))
                    {
                        productCreateViewModel.ImageProducts = "/Uploads/PostImages/" + productCreateViewModel.ImageProducts;
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

        protected override async Task OnInitializedAsync()
        {
            CategoryParent = await postApi.GetdataSelectCategoryByParentId("Categories", "GetCategoriesByParentId", 2);
            StatusViewModel = await postApi.GetDataSelectStatus("Status", "GetListSatatus");
        }
        async Task GetPostDetailById()
        {
            DetailPost = await postApi.GetPostDetailById("Post", "GetPostDetailById");
        }

    }
}