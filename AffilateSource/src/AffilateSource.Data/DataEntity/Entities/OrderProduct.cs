using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AffilateSource.Data.DataEntity.Entities
{
    [Table("OrderProducts")]
    public class OrderProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        public int TimeLineId { get; set; }

        public int ProductId { get; set; }
        //public int ServicePriceid { get; set; }
        //public int? ParentId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
