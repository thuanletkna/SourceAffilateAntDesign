using AffilateSource.Shared.ViewModel.Contact;
using AffilateSource.Shared.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.Component
{
    public class FooterViewModel
    {
        public IEnumerable<PostHomeViewModel> ListPostViewCount { get; set; }
        public ContactVm getContact { get; set; }
    }
}
