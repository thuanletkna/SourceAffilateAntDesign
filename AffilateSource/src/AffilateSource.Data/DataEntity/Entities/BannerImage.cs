using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffilateSource.Data.DataEntity.Entities
{
    [Table("BannerImages")]
    public class BannerImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BannerName { get; set; }
        public string PathImages { get; set; }
        public string Type { get; set; }
        public string StatusId { get; set; }
    }
}
