using AffilateSource.Data.DataEntity;
using AffilateSource.Data.Services.Interface;
using AffilateSource.Shared.ViewModel.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Data.Services.Repository
{
    public class ContactServices : IContactServices
    {
        public ApplicationDbContext context;
        public ContactServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ContactVm>GetContact()
        {
            var data = from a in context.Contacts
                       select new ContactVm { Content = a.Content };
            return (ContactVm)data;
        }
    }
}
