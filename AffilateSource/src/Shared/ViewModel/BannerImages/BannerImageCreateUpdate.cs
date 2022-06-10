using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.BannerImages
{
    public class BannerImageCreateUpdate
    {
        public int Id { get; set; }
        public string BannerName { get; set; }
        public string PathImages { get; set; }
        public string Type { get; set; }
        public string StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
