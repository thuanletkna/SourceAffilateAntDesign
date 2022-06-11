
using AffilateSource.Shared.ViewModel.BannerImages;
using AffilateSource.Shared.ViewModel.Category;
using AffilateSource.Shared.ViewModel.Contact;
using AffilateSource.Shared.ViewModel.Post;
using AffilateSource.Shared.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AffilateSource.Shared.ViewModel.Home
{
    public class HomeViewModel
    {

        public List<CategoryQuickVM> GetCategoryHome { get; set; }
        public List<CategoryQuickVM> GetcategoryByParentId { get; set; }
        public List<CategoryQuickVM> GetDanhMucKhuyenMai { get; set; }
        public List<CategoryQuickVM> GetDanhMucKihNghiem { get; set; }
        public IEnumerable<ProductHomeViewModel> GetProductHome { get; set; }
        public IEnumerable<ProductHomeViewModel> GetProductByViewCount { get; set; }
        public IEnumerable<PostHomeViewModel> GetPostHome { get; set; }
        public IEnumerable<PostHomeViewModel> GetPostCreateDate { get; set; }
        public BannerImageViewModel Getbanner { get; set; }
        public BannerImageViewModel GetbannerImages { get; set; }
        public ContactVm getContact {get;set;}
        public List<BannerImageCreateUpdate> BannerImageVm { get; set; }

    }
}
