using AffilateSource.Shared.ViewModel.Category;
using AffilateSource.Shared.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.Component
{
    public class LeftSideBarViewModel
    {
        public List<CategoryQuickVM> ListcategorySidebar { get; set; }
        public List<CategoryQuickVM> ListcategoryProductSidebar { get; set; }
        public List<CategoryQuickVM> ListcategoryPostSidebar { get; set; }
        public IEnumerable<PostHomeViewModel> ListPostViewCount { get; set; }
    }
}
