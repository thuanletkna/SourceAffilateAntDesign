using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.Product
{
    public class ProductCreateViewModel
    {

        public int CategoryId { get; set; }
        public int CategoryParentId { get; set; }
        public int Id { get; set; }

        public string Title { get; set; }

        public string SeoAlias { get; set; }
        public string ImageProducts { get; set; }
        public string StatusId { get; set; }
        public string Description { get; set; }

        public string Detail { get; set; }
        public int Price { get; set; }

        public string OwnerUserId { get; set; }

        public string Labels { get; set; }
        public string LinkAffilateLazada { get; set; }
        public string LinkAffilateShopee { get; set; }
        public string LinkAffilateTiki { get; set; }
        public string LinkAffilateOther { get; set; }
        public bool isAffilate { get; set; }
        public DateTime CreateDate { get; set; }
        public IFormFile ProductImage { set; get; }
    }
}
