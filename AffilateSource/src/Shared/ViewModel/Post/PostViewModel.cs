using AffilateSource.Shared.ViewModel.BannerImages;
using AffilateSource.Shared.ViewModel.Category;
using System.Collections.Generic;


namespace AffilateSource.Shared.ViewModel.Post
{
    public class PostViewModel
    {
        public DataEnvelope<PostHomeViewModel> PostListAll { get; set; }
        public DataEnvelope<PostHomeViewModel> GetAllPostByCategoryIdPaging { get; set; }
        public List<CategoryQuickVM> GetDanhMucKinhNghiem { get; set; }
        public List<ListPostDetailVm> DetailPostbyId { get; set; }
        public PostHomeViewModel GetPostById { get; set; }
        public List<BannerImageCreateUpdate> BannerImageVm { get; set; }
    }
}
