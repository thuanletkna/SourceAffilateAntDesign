using Telerik.Blazor;
using Telerik.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AffilateSource.Shared.ViewModel.Contact;

namespace AffilateSource.Client.Pages.Contact
{
    public partial class Contact
    {
        TelerikGrid<ContactVm> DataSource { get; set; }
        GridReadEventArgs args = new GridReadEventArgs();
       
        #region Lọc dữ liệu Grid
        async Task RefreshGrid()
        {
            await RefreshThroughState();
        }
        
        async Task RefreshThroughState()
        {
            await Task.Delay(1);
            await DataSource.SetState(DataSource.GetState());
        }
        protected async Task ReadItems(GridReadEventArgs argsz)
        {
            GridData = await postApi.GetContact("Contact", "GetListContact");

        }

       
        #endregion Lọc dữ liệu Grid

        public List<ContactVm> GridData { get; set; }
        
        protected override async Task OnParametersSetAsync()
        {
            await Task.Delay(1000);//simulate database delay, remove this in a real app
            GridData = await postApi.GetContact("Contact", "GetListContact");
        }
    }
}