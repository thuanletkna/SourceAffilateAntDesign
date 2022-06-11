using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.Post
{
    public class PostDetailVm
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public string TitleDetail { get; set; }
        public int ProductId { get; set; }
        public int Id { get; set; }
        public int PostId { get; set; }
        public int StatusIdDetail { get; set; }
        public string StatusId { get; set; }
        public int SortDetail { get; set; }
        public string ProductAffilateName { get; set; }
        public int ProductAffilatePrice { get; set; }
        public string ImageProducts { get; set; }
        public string LinkAffilateLazada { get; set; }
        public string LinkAffilateShopee { get; set; }
        public string LinkAffilateOther { get; set; }

    }
}
