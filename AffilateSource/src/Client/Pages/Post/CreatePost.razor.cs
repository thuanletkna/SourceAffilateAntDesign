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

namespace AffilateSource.Client.Pages.Post
{
    public partial class CreatePost
    {
        public List<PostHomeViewModel> GridData { get; set; }
        public List<CategoriesSelectViewModel> CategoryParent { get; set; } = new List<CategoriesSelectViewModel>();
        public List<ProductSelectViewModel> productSelect { get; set; } = new List<ProductSelectViewModel>();
        PostCreateViewModel postCreateViewModel { get; set; } = new PostCreateViewModel();
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
        private async Task OnSuccessHandler(UploadSuccessEventArgs e, string field)
        {
            if (e.Operation == UploadOperationType.Upload)
            {
                string content = e.Request.ResponseText;
                foreach (var file in e.Files)
                {

                    //postCreateViewModel.Detail = content;
                    postCreateViewModel.ImagePost = content;

                    if (!string.IsNullOrEmpty(postCreateViewModel.ImagePost))
                    {
                        postCreateViewModel.ImagePost = "/Uploads/PostImages/" + postCreateViewModel.ImagePost;
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


        async Task ContentOnChange(string returnValue, string field)
        {
            await Task.Run(() =>
            {
                if (field == "description")
                    postCreateViewModel.Description = returnValue;

                //if (field == "sumary")
                //    model.sumary = returnValue;

            });
        }


        string title = "BasicModal";
        bool _visible = false;

        private async Task HandleOkAsync(MouseEventArgs e)
        {
            await Http.PostAsJsonAsync("/Post/CreatePostDetail", DetailPostsModal);
            await GetPostDetailById();
            _visible = false;
        }

        private void HandleCancel(MouseEventArgs e)
        {
            Console.WriteLine(e);
            _visible = false;
        }

        async Task OnFinishHandler()
        {
            ShowWizard = false;

            await Dialog.AlertAsync("The Registration was submitted successfully", "Done");
            NavigationManager.NavigateTo("/");
        }

        public bool? IsRegistrationValid { get; set; }

        public bool? IsShippingValid { get; set; }

        [CascadingParameter]
        public DialogFactory Dialog { get; set; }

        public bool ShowWizard { get; set; } = true;

        public int Value { get; set; }

        public TelerikForm RegisterForm { get; set; }
        public TelerikForm ShippingForm { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CategoryParent = await postApi.GetdataSelectCategoryByParentId("Categories", "GetCategoriesByParentId",3);
            StatusViewModel = await postApi.GetDataSelectStatus("Status", "GetListSatatus");
        }

        public async Task OnRegistrationStepChangeAsync(WizardStepChangeEventArgs args)
        {
            var isFormValid = RegisterForm.IsValid();
            if (!isFormValid)
            {
                IsRegistrationValid = false;
                args.IsCancelled = true;
            }
            else
            {
                var post = await Http.PostAsJsonAsync("/Post/CreatePost", postCreateViewModel);
                if(post.IsSuccessStatusCode)
                {
                    GridData = await postApi.GetDataIdPostCreated("Post", "GetPostCreatedLast");
                    productSelect = await postApi.GetdataSelectProductByCategoryId("Product", "GetProductSelectByCategoryId", 2);
                    await GetPostDetailById();
                }    
                IsRegistrationValid = true;
            }
        }

        async Task GetPostDetailById()
        {
            DetailPost = await postApi.GetPostDetailById("Post", "GetPostDetailById");
        }
        public void OnShippingStepChange(WizardStepChangeEventArgs args)
        {
            var isPrevious = Value < args.TargetIndex;
            if (Value < args.TargetIndex)
            {
                IsShippingValid = true;
                //var isFormValid = ShippingForm.IsValid();
                //if (!isFormValid)
                //{
                //    IsShippingValid = false;
                //    args.IsCancelled = true;
                //}
                //else
                //{
                //    IsShippingValid = true;
                //}
            }
        }



       
    }
}