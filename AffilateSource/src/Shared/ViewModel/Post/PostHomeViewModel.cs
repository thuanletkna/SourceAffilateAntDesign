using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.Post
{
    public class PostHomeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string StatusName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePost { get; set; }
        public string StatusId { get; set; }

        public string SeoAlias { get; set; }
        public string Description { get; set; }

        public string Detail { get; set; }
        public string FilePath { get; set; }

        public string Labels { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
