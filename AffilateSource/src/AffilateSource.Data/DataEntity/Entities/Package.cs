using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AffilateSource.Data.DataEntity.Entities
{
    [Table("Packages")]
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        public string UseTime { get; set; }

        //public int KnowledgeBaseId { get; set; }
        //public int ServicePriceid { get; set; }
        public int? ParentId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
