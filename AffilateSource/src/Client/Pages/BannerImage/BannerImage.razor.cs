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
using AffilateSource.Shared.ViewModel.BannerImages;
using Telerik.DataSource;
using AffilateSource.Shared.ViewModel;
using AffilateSource.Shared.Kendohelpers;
using System.Linq;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;

namespace AffilateSource.Client.Pages.BannerImage
{
    public partial class BannerImage
    {
        Telerik.Blazor.TabPosition tabPosition = Telerik.Blazor.TabPosition.Top;
        TelerikGrid<SlideImageVm> DataSource { get; set; }
        GridReadEventArgs args = new GridReadEventArgs();
        int PageSize { get; set; } = 50;
        int Page { get; set; } = 1;
        public SlideImageVm DetailSlide { get; set; }
        [Parameter] public int id { get; set; }

        #region Get data Slide Grid

        GridSelectionMode selectionMode { get; set; } = GridSelectionMode.Multiple;
        bool ShowSelectAll => selectionMode == GridSelectionMode.Multiple;
        public IEnumerable<SlideImageVm> SelectedItems { get; set; } = Enumerable.Empty<SlideImageVm>();

        protected async Task ReadItems(GridReadEventArgs argsz)
        {
            args = argsz;
            Page = args.Request.Page;
            await GetData();
        }
        async Task RefreshThroughState()
        {
            await Task.Delay(1);
            await DataSource.SetState(DataSource.GetState());
        }
        async Task GetData()
        {
            args.Request.Filters.Clear();
            CompositeFilterDescriptor _filter = new CompositeFilterDescriptor();
            _filter.LogicalOperator = FilterCompositionLogicalOperator.And;

            //if (!string.IsNullOrEmpty(filtterRequest.CategoryParentId))
            //    _filter.FilterDescriptors.Add(new FilterDescriptor("CategoryParentId", FilterOperator.IsEqualTo, filtterRequest.CategoryParentId));
            //if (!string.IsNullOrEmpty(filtterRequest.StatusId))
            //    _filter.FilterDescriptors.Add(new FilterDescriptor("StatusId", FilterOperator.IsEqualTo, filtterRequest.StatusId));

            //if (!string.IsNullOrEmpty(filtterRequest.NhanVien))
            //    _filter.FilterDescriptors.Add(new FilterDescriptor("MaNhanVien", FilterOperator.IsEqualTo, filtterRequest.NhanVien));
            //if (!string.IsNullOrEmpty(filtterRequest.PhongBan))
            //    _filter.FilterDescriptors.Add(new FilterDescriptor("MaPhongBan", FilterOperator.IsEqualTo, filtterRequest.PhongBan));
            //if (!string.IsNullOrEmpty(filtterRequest.KhuVuc))
            //    _filter.FilterDescriptors.Add(new FilterDescriptor("MaKhuVuc", FilterOperator.IsEqualTo, filtterRequest.KhuVuc));
            args.Request.Filters.Add(_filter);
            // Sắp xếp
            args.Request.Sorts.Clear();
            args.Request.Sorts.Add(new SortDescriptor("Id", ListSortDirection.Descending));
            args.Request.PageSize = PageSize;
            DataEnvelope<SlideImageVm> result = await postApi.GetDataSlideAdminAsync("BannerImage", "GetBannerSlidePagingFilterAdmin", args.Request);
            if (args.Request.Groups.Count > 0)
            {
                var data = GroupDataHelpers.DeserializeGroups<SlideImageVm>(result.GroupedData);
                args.Data = data.Cast<object>().ToList();
            }
            else
            {
                args.Data = result.Data.Cast<object>().ToList();
            }
            args.Total = result.Total;
            StateHasChanged();
        }
        #endregion Get data Slide Grid


        #region Modal Thêm mới Slide
        bool _visible = false;
        bool loading = false;
        void toggle(bool value) => loading = value;
        private Form<SlideImageVm> _form;
        private SlideImageVm model = new SlideImageVm();
        public TelerikForm RegisterForm { get; set; }
        private void ShowModal()
        {
            _visible = true;
        }

        private void HandleCancel(MouseEventArgs e)
        {
            _visible = false;
        }
        private void HandleOk(MouseEventArgs e)
        {
            _form.Submit();
        }
        async Task OnFinish()
        {
            var checkname = await Http.PostAsJsonAsync("/BannerImage/CreateSlide", model);
            if (checkname.IsSuccessStatusCode)
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Thêm mới Slide thành công",
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
            _visible = false;
        }
        private void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(model)}");
        }
        #endregion Modal Thêm mới Slide


        #region Chỉnh sửa slide
        bool _visibleUpdate = false;
        bool loadingUpdate = false;
        void toggleUpdate(bool value) => loadingUpdate = value;
        private Form<SlideImageVm> _formUpdate;
        private SlideImageVm modelUpdate = new SlideImageVm();
        async Task<SlideImageVm> btnUpdate(int id)
        {
            modelUpdate = await postApi.GetSlideDetailByIdAdmin("BannerImage", "GetDetailSlideUpdateById", id);
            _visibleUpdate = true;
            return modelUpdate;
        }

        private void HandleCancelUpdate(MouseEventArgs e)
        {
            _visibleUpdate = false;
        }
        private void HandleOkUpdate(MouseEventArgs e)
        {
            _formUpdate.Submit();
        }
        async Task OnFinishUpdate()
        {
            var checkname = await Http.PostAsJsonAsync("/BannerImage/UpdateSlide", modelUpdate);
            if (checkname.IsSuccessStatusCode)
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Chỉnh sửa slide thành công",
                    NotificationType = NotificationType.Success
                });
                await RefreshThroughState();
                _visibleUpdate = false;

            }
            else
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Lỗi",
                    NotificationType = NotificationType.Error
                });
            }
            _visibleUpdate = false;
        }
        private void OnFinishFailedUpdate(EditContext editContext)
        {
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(modelUpdate)}");
        }
        private async Task OnSuccessHandlerUpdate(UploadSuccessEventArgs e, string field)
        {
            if (e.Operation == UploadOperationType.Upload)
            {
                string content = e.Request.ResponseText;
                foreach (var file in e.Files)
                {

                    //postCreateViewModel.Detail = content;
                    modelUpdate.ImageSlide = content;

                    if (!string.IsNullOrEmpty(modelUpdate.ImageSlide))
                    {
                        modelUpdate.ImageSlide = "/Uploads/PostImages/" + modelUpdate.ImageSlide;
                    }

                }
            }
            await Task.CompletedTask;

        }
        #endregion Chỉnh sửa slide


        #region Upload Ảnh

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
                    model.ImageSlide = content;

                    if (!string.IsNullOrEmpty(model.ImageSlide))
                    {
                        model.ImageSlide = "/Uploads/PostImages/" + model.ImageSlide;
                    }

                }
            }
            await Task.CompletedTask;
        }
        private async Task OnSuccessHandlerBannerImage(UploadSuccessEventArgs e, string field)
        {
            if (e.Operation == UploadOperationType.Upload)
            {
                string content = e.Request.ResponseText;
                foreach (var file in e.Files)
                {

                    //postCreateViewModel.Detail = content;
                    modelBannerImage.PathImages = content;

                    if (!string.IsNullOrEmpty(modelBannerImage.PathImages))
                    {
                        modelBannerImage.PathImages = "/Uploads/PostImages/" + modelBannerImage.PathImages;
                    }

                }
            }
            await Task.CompletedTask;
        }

        #endregion Upload Ảnh


        #region Get Banner Image
        TelerikGrid<BannerImageCreateUpdate> DataSourceBanner { get; set; }
        GridSelectionMode selectionModeBanner { get; set; } = GridSelectionMode.Multiple;
        bool ShowSelectAllBanner => selectionModeBanner == GridSelectionMode.Multiple;
        public IEnumerable<BannerImageCreateUpdate> SelectedItemsBanner { get; set; } = Enumerable.Empty<BannerImageCreateUpdate>();

        protected async Task ReadItemsBanner(GridReadEventArgs argsz)
        {
            args = argsz;
            Page = args.Request.Page;
            await GetDataBanner();
        }
        async Task RefreshThroughStateBanner()
        {
            await Task.Delay(1);
            await DataSourceBanner.SetState(DataSourceBanner.GetState());
        }
        async Task GetDataBanner()
        {
            args.Request.Filters.Clear();
            CompositeFilterDescriptor _filter = new CompositeFilterDescriptor();
            _filter.LogicalOperator = FilterCompositionLogicalOperator.And;

            //if (!string.IsNullOrEmpty(filtterRequest.CategoryParentId))
            //    _filter.FilterDescriptors.Add(new FilterDescriptor("CategoryParentId", FilterOperator.IsEqualTo, filtterRequest.CategoryParentId));
            //if (!string.IsNullOrEmpty(filtterRequest.StatusId))
            //    _filter.FilterDescriptors.Add(new FilterDescriptor("StatusId", FilterOperator.IsEqualTo, filtterRequest.StatusId));

            //if (!string.IsNullOrEmpty(filtterRequest.NhanVien))
            //    _filter.FilterDescriptors.Add(new FilterDescriptor("MaNhanVien", FilterOperator.IsEqualTo, filtterRequest.NhanVien));
            //if (!string.IsNullOrEmpty(filtterRequest.PhongBan))
            //    _filter.FilterDescriptors.Add(new FilterDescriptor("MaPhongBan", FilterOperator.IsEqualTo, filtterRequest.PhongBan));
            //if (!string.IsNullOrEmpty(filtterRequest.KhuVuc))
            //    _filter.FilterDescriptors.Add(new FilterDescriptor("MaKhuVuc", FilterOperator.IsEqualTo, filtterRequest.KhuVuc));
            args.Request.Filters.Add(_filter);
            // Sắp xếp
            args.Request.Sorts.Clear();
            args.Request.Sorts.Add(new SortDescriptor("Id", ListSortDirection.Descending));
            args.Request.PageSize = PageSize;
            DataEnvelope<BannerImageCreateUpdate> result = await postApi.GetDataBannerImageAdminAsync("BannerImage", "GetBannerImagePagingFilterAdmin", args.Request);
            if (args.Request.Groups.Count > 0)
            {
                var data = GroupDataHelpers.DeserializeGroups<BannerImageCreateUpdate>(result.GroupedData);
                args.Data = data.Cast<object>().ToList();
            }
            else
            {
                args.Data = result.Data.Cast<object>().ToList();
            }
            args.Total = result.Total;
            StateHasChanged();

        }
        #endregion Get Banner Image


        bool _visibleBannerImage = false;
        bool loadingBannerImage = false;
        void toggleBannerImage(bool value) => loading = value;
        private Form<BannerImageCreateUpdate> _formBannerImage;
        private BannerImageCreateUpdate modelBannerImage = new BannerImageCreateUpdate();
        public TelerikForm RegisterFormBannerImage { get; set; }
        private void ShowModalBannerImage()
        {
            _visibleBannerImage = true;
        }

        private void HandleCancelBannerImage(MouseEventArgs e)
        {
            _visibleBannerImage = false;
        }
        private void HandleOkBannerImage(MouseEventArgs e)
        {
            _formBannerImage.Submit();
        }
        async Task OnFinishBannerImage()
        {
            var checkname = await Http.PostAsJsonAsync("/BannerImage/CreateBannerImage", modelBannerImage);
            if (checkname.IsSuccessStatusCode)
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Thêm mới Slide thành công",
                    NotificationType = NotificationType.Success
                });
                _visibleBannerImage = false;

            }
            else
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Lỗi",
                    NotificationType = NotificationType.Error
                });
                _visibleBannerImage = true;
            }
           
        }
        private void OnFinishFailedBannerImage(EditContext editContext)
        {
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(modelBannerImage)}");
        }


        #region Chỉnh sửa Banner Image
        bool _visibleUpdateBannerImage = false;
        bool loadingUpdateBannerImage = false;
        void toggleUpdateBannerImage(bool value) => loadingUpdateBannerImage = value;
        private Form<BannerImageCreateUpdate> _formUpdateBannerImage;
        private BannerImageCreateUpdate modelUpdateBannerImage = new BannerImageCreateUpdate();
        async Task<BannerImageCreateUpdate> btnUpdateBannerImage(int id)
        {
            modelUpdateBannerImage = await postApi.GetBannerImageById("BannerImage", "GetBannerImageById", id);
            _visibleUpdateBannerImage = true;
            return modelUpdateBannerImage;
        }

        private void HandleCancelUpdateBannerImage(MouseEventArgs e)
        {
            _visibleUpdateBannerImage = false;
        }
        private void HandleOkUpdateBannerImage(MouseEventArgs e)
        {
            _formUpdateBannerImage.Submit();
        }
        async Task OnFinishUpdateBannerImage()
        {
            var checkname = await Http.PostAsJsonAsync("/BannerImage/UpdateBannerImage", modelUpdateBannerImage);
            if (checkname.IsSuccessStatusCode)
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Chỉnh sửa Banner thành công",
                    NotificationType = NotificationType.Success
                });
                await RefreshThroughState();
                _visibleUpdateBannerImage = false;

            }
            else
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Lỗi",
                    NotificationType = NotificationType.Error
                });
            }
            _visibleUpdateBannerImage = false;
        }
        private void OnFinishFailedUpdateBannerImage(EditContext editContext)
        {
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(modelUpdateBannerImage)}");
        }
        private async Task OnSuccessHandlerUpdateBannerImage(UploadSuccessEventArgs e, string field)
        {
            if (e.Operation == UploadOperationType.Upload)
            {
                string content = e.Request.ResponseText;
                foreach (var file in e.Files)
                {

                    //postCreateViewModel.Detail = content;
                    modelUpdateBannerImage.PathImages = content;

                    if (!string.IsNullOrEmpty(modelUpdateBannerImage.PathImages))
                    {
                        modelUpdateBannerImage.PathImages = "/Uploads/PostImages/" + modelUpdateBannerImage.PathImages;
                    }

                }
            }
            await Task.CompletedTask;

        }
        #endregion Chỉnh sửa Banner Image
    }
}