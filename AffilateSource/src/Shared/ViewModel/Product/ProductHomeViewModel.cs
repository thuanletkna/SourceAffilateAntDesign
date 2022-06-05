using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.Product
{
    public class ProductHomeViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string ImageProducts { get; set; }
        public string StatusName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string SeoAlias { get; set; }
        public string Description { get; set; }

        public string Detail { get; set; }
        public string FilePath { get; set; }
        public string LinkAffilateLazada { get; set; }
        public string LinkAffilateShopee { get; set; }
        public string LinkAffilateTiki { get; set; }
        public string LinkAffilateOther { get; set; }
        public int Price { get; set; }


        public string Labels { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
