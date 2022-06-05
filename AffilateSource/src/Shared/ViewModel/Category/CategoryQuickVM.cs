using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.Category
{
    public class CategoryQuickVM
    {
        public int Id { get; set; }
        public string FilePath { get; set; }

        public string CategoryName { get; set; }

        public string SeoAlias { get; set; }

        public string Image { get; set; }

        public string SeoDescription { get; set; }

        public int SortOrder { get; set; }

        public int? ParentId { get; set; }
        public int? Level { get; set; }

        public int? NumberOfTickets { get; set; }
    }
}
