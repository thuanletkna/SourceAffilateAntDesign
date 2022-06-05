using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.Category
{
    public class CategoryCreateViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public string SeoAlias { get; set; }

        public string Image { get; set; }

        public string SeoDescription { get; set; }

        public int SortOrder { get; set; }

        public int? ParentId { get; set; }
        public int StatusId { get; set; }

        public int? NumberOfTickets { get; set; }
        public IFormFile CategoryImage { set; get; }
    }
}
