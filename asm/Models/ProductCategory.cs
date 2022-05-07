using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace asm.Models
{
    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        [Key]
        public long Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [StringLength(50)]
        public string MetaTitle { get; set; }
        public bool? Status { get; set; }
    }
}
