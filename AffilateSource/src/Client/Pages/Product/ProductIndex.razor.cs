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

namespace AffilateSource.Client.Pages.Product
{
    public partial class ProductIndex
    {
        TelerikGrid<ProductHomeViewModel> DataSource { get; set; }
        GridReadEventArgs args = new GridReadEventArgs();
        ProductCreateViewModel productCreateModel = new ProductCreateViewModel();
        GridSelectionMode selectionMode { get; set; } = GridSelectionMode.Multiple;
        bool ShowSelectAll => selectionMode == GridSelectionMode.Multiple;
        public IEnumerable<ProductHomeViewModel> SelectedItems { get; set; } = Enumerable.Empty<ProductHomeViewModel>();
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
            public List<CategoriesSelectViewModel> CategoryProduct { get; set; } = new List<CategoriesSelectViewModel>();
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
        private async Task GetSelectListValue()
        {
            try
            {
                var getDataSelect = await postApi.GetDataSelectFilterAdmin("Admin", "GetSelectListFilter");
                var getDataDanhMuc = await postApi.GetdataSelectCategoryByParentId("Categories", "GetCategoriesByParentId", 2);
                filtterRequest.CategoryProduct = getDataDanhMuc;
                filtterRequest.dataSelectStatus = await postApi.GetDataSelectStatus("Status", "GetListSatatus");
                var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
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
            DataEnvelope<ProductHomeViewModel> result = await postApi.GetDataProductAdminAsync("Product", "GetProductPagingFilterAdmin", args.Request);
            if (args.Request.Groups.Count > 0)
            {
                var data = GroupDataHelpers.DeserializeGroups<ProductHomeViewModel>(result.GroupedData);
                args.Data = data.Cast<object>().ToList();
            }
            else
            {
                args.Data = result.Data.Cast<object>().ToList();
            }
            args.Total = result.Total;
            StateHasChanged();
        }
    }
}