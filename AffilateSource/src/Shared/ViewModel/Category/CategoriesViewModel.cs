using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.Category
{
    public class CategoriesViewModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryParentId { get; set; }
        public string Level { get; set; }
        public List<CategoryQuickVM> categoryQuickVMs { get; set; }
    }
    
}
