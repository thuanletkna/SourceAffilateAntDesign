using Telerik.Blazor;
using Telerik.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AffilateSource.Shared.ViewModel.Contact;
using Microsoft.AspNetCore.Components.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using AntDesign;

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


        bool _visibleUpdate = false;
        bool loadingUpdate = false;
        void toggleUpdate(bool value) => loadingUpdate = value;
        private Form<ContactVm> _formUpdate;
        private ContactVm modelUpdate = new ContactVm();
        async Task<ContactVm> btnUpdate(int id)
        {
            modelUpdate = await postApi.GetContactDetailByIdAdmin("Contact", "GetContactById", id);
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
            var checkname = await Http.PostAsJsonAsync("/Contact/UpdateContacts", modelUpdate);
            if (checkname.IsSuccessStatusCode)
            {
                await _notice.Open(new NotificationConfig()
                {
                    Message = "Chỉnh sửa thông tin thành công",
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
    }
}