using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.Contact
{
    public class ContactVm
    {
        public int ID { get; set; }

        public string Content { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FacebookLink { get; set; }
        public string ZaloLink { get; set; }
        public string ContentHome { get; set; }
        public bool? Status { get; set; }
    }
}
