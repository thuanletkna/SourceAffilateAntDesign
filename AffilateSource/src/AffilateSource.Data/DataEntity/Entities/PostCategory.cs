﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AffilateSource.Data.DataEntity.Entities
{
    [Table("PostCategories")]
    public class PostCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        [Required]
        public string SeoAlias { get; set; }

        public string Image { get; set; }

        [MaxLength(500)]
        public string SeoDescription { get; set; }

        [Required]
        public int SortOrder { get; set; }

        public int? ParentId { get; set; }

        public int? NumberOfTickets { get; set; }
    }
}
