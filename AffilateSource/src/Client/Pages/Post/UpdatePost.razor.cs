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
using System.Text.Json;
using AffilateSource.Shared.ViewModel.Filter;
using AffilateSource.Shared.ViewModel.Status;

namespace AffilateSource.Client.Pages.Post
{
    public partial class UpdatePost : ComponentBase
    {
        [Parameter] public int id { get; set; }

        TelerikGrid<PostDetailVm> DataSource { get; set; }
        PostCreateViewModel postEditViewModel { get; set; } = new PostCreateViewModel();
        public List<PostDetailVm> DetailPost { get; set; }
        public PostDetailVm DetailPosts { get; set; }
        PostDetailVm detailUpdate { get; set; } = new PostDetailVm();
        public List<ProductSelectViewModel> productSelect { get; set; } = new List<ProductSelectViewModel>();
        public ProductSelectViewModel productSelectViewModel { get; set; }
        string activeKey { get; set; } = "1";
        public List<StatusVm> StatusViewModel { get; set; } = new List<StatusVm>();

        //Thông báo thành công sau khi chỉnh sửa

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

        protected override async Task OnInitializedAsync()
        {
            await GetSelectListValue();
            DetailPost = await postApi.GetListPostDetailByIdAdmin("Post", "GetPostDetailByIdAdmin", id);
            StatusViewModel = await postApi.GetDataSelectStatus("Status", "GetListSatatus");
        }
        async Task<PostCreateViewModel> OnTabChange(string key)
        {
            postEditViewModel = await postApi.GetPostDetailByIdAdmin("Post", "GetPostByIdAdminEdit", id);
            return postEditViewModel;
        }
        
        //Update bài đăng

        async Task UpdateEmployee()
        {
            var checkname = await Http.PostAsJsonAsync("/Post/UpdatePost", postEditViewModel);
            if (checkname.IsSuccessStatusCode)
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Chỉnh sửa bài đăng thành công",
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


        //Update chi tiết bài đăng
        public bool WindowVisible { get; set; }
        private void HandleCancelDetail(MouseEventArgs e)
        {
            WindowVisible = false;
        }
        async Task<PostDetailVm> btnUpdatePostDetails(int id)
        {

            //Kiểm tra phiên đăng nhập và phân quyền
            WindowVisible = !WindowVisible;
            detailUpdate = await postApi.GetDetailByPostDetailIdAdmin("Post", "GetDetailsByPostDetails", id);
            productSelect = await postApi.GetdataSelectProductByCategoryId("Product", "GetProductSelectByCategoryId", 2);
            return detailUpdate;
        }
        async Task UpdateDetail()
        {

            
            // hub realtime signalR

            var checkname = await Http.PostAsJsonAsync("/Post/UpdatePostDetail", detailUpdate);
            if (checkname.IsSuccessStatusCode)
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Chỉnh sửa bài đăng thành công",
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
        // file upload image
        #region Upload image
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
                    postEditViewModel.ImagePost = content;

                    if (!string.IsNullOrEmpty(postEditViewModel.ImagePost))
                    {
                        postEditViewModel.ImagePost = "/Uploads/PostImages/" + postEditViewModel.ImagePost;
                    }
                  
                }
            }

        }
        #endregion Upload image
    }
}