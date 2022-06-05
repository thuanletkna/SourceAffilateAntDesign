using AffilateSource.Shared.ViewModel.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Data.Services.Interface
{
    public interface IContactServices
    {
        public Task<ContactVm> GetContact();
    }
}
