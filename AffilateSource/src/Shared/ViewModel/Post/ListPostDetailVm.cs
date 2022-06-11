using AffilateSource.Shared.ViewModel.Product;


namespace AffilateSource.Shared.ViewModel.Post
{
    public class ListPostDetailVm : ProductHomeViewModel
    {
        public string Content { get; set; }
        public int ProductId { get; set; }
        public int PostId { get; set; }
        public int StatusIdDetail { get; set; }
        public int StatusId { get; set; }
        public int SortDetail { get; set; }
        public string ProductAffilateName { get; set; }
        public int ProductAffilatePrice { get; set; }
        public string ImageProducts { get; set; }
        public string LinkAffilateLazada { get; set; }
        public string LinkAffilateShopee { get; set; }
        public string LinkAffilateOther { get; set; }
    }
}
