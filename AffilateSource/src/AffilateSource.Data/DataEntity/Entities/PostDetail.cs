using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AffilateSource.Data.DataEntity.Entities
{
    [Table("PostDetails")]
    public class PostDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }
        public string TitleDetail { get; set; }
        public int ProductId { get; set; }
        public int PostId { get; set; }
        public int SortDetail { get; set; }
        public int StatusId { get; set; }
        public string ProductAffilateName { get; set; }
        public int ProductAffilatePrice { get; set; }
        public string ImageProducts { get; set; }
        public string LinkAffilateLazada { get; set; }
        public string LinkAffilateShopee { get; set; }
        public string LinkAffilateOther { get; set; }
    }
}
