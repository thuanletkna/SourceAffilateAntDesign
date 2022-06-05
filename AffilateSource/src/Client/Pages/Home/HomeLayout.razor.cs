using Telerik.Blazor;
using Telerik.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.DataSource;
using System.Linq;
using AffilateSource.Shared.ViewModel.Post;
using AffilateSource.Shared.ViewModel.Filter;
using AffilateSource.Shared.ViewModel;
using AffilateSource.Shared.Kendohelpers;
using AffilateSource.Shared.ViewModel.Status;

namespace AffilateSource.Client.Pages.Home
{
    public partial class HomeLayout
    {
        TelerikGrid<PostHomeViewModel> DataSource { get; set; }
        GridReadEventArgs args = new GridReadEventArgs();
        GridSelectionMode selectionMode { get; set; } = GridSelectionMode.Multiple;
        bool ShowSelectAll => selectionMode == GridSelectionMode.Multiple;
        public IEnumerable<PostHomeViewModel> SelectedItems { get; set; } = Enumerable.Empty<PostHomeViewModel>();
        int PageSize { get; set; } = 50;
        int Page { get; set; } = 1;
        bool Visible { get; set; }
        bool isVisible { get; set; }
        public FilterRequest filtterRequest { get; set; } = new FilterRequest { };
        public class FilterRequest
        {
            public object this[string propertyName]
            {
                get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
                set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
            }
            public string CategoryParentId { get; set; }
            public string StatusId { get; set; } = "1";
            //public string MaCongTy { get; set; } = "CT00001";
            public IEnumerable<StatusVm> dataSelectStatus { get; set; } = new List<StatusVm>();
            public List<SelectModel> dataSelectCategoryFilter { get; set; } = new List<SelectModel>();

        }
       
        #region Lọc dữ liệu Grid
        async Task RefreshGrid()
        {
            await RefreshThroughState();
        }
        async Task UpdateReadData()
        {
            if (Page != 1)
            {
                Page = 1;
                return;
            }
            Page = 1; args.Request.Page = 1;
            await RefreshThroughState();
        }
        async Task RefreshThroughState()
        {
            await Task.Delay(1);
            await DataSource.SetState(DataSource.GetState());
        }
        protected async Task ReadItems(GridReadEventArgs argsz)
        {
            args = argsz;
            Page = args.Request.Page;
            await GetData();
            await GetSelectListValue();
        }

        async Task SelectChangeHandlerCategory(string theUserInput, string paramFilter)
        {
            filtterRequest[paramFilter] = theUserInput;
            StateHasChanged();
            await UpdateReadData();
            StateHasChanged();
        }
        async Task SelectChangeHandlerStatus(string theUserInput, string paramFilter)
        {
            filtterRequest[paramFilter] = theUserInput;
            StateHasChanged();
            await UpdateReadData();
            StateHasChanged();
        }
        #endregion Lọc dữ liệu Grid

        #region Get data DropdownList
        private async Task GetSelectListValue()
        {
            try
            {
                var getDataSelect = await postApi.GetDataSelectFilterAdmin("Admin", "GetSelectListFilter");
                filtterRequest.dataSelectCategoryFilter = getDataSelect.ListCategory;
                filtterRequest.dataSelectStatus = await postApi.GetDataSelectStatus("Status", "GetListSatatus");
                //filtterRequest.dataSelectStatus = await StatusServices.GetStatusAsync();
                var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion Get data DropdownList

        #region Get data Grid
        async Task GetData()
        {
            args.Request.Filters.Clear();
            CompositeFilterDescriptor _filter = new CompositeFilterDescriptor();
            _filter.LogicalOperator = FilterCompositionLogicalOperator.And;

            if (!string.IsNullOrEmpty(filtterRequest.CategoryParentId))
                _filter.FilterDescriptors.Add(new FilterDescriptor("CategoryParentId", FilterOperator.IsEqualTo, filtterRequest.CategoryParentId));
            if (!string.IsNullOrEmpty(filtterRequest.StatusId))
                _filter.FilterDescriptors.Add(new FilterDescriptor("StatusId", FilterOperator.IsEqualTo, filtterRequest.StatusId));

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
            DataEnvelope<PostHomeViewModel> result = await postApi.GetDataPostAdminAsync("Post", "GetPostPagingFilterAdmin", args.Request);
            if (args.Request.Groups.Count > 0)
            {
                var data = GroupDataHelpers.DeserializeGroups<PostHomeViewModel>(result.GroupedData);
                args.Data = data.Cast<object>().ToList();
            }
            else
            {
                args.Data = result.Data.Cast<object>().ToList();
            }
            args.Total = result.Total;
            StateHasChanged();
        }
        #endregion Get data Grid
    }
}