using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace asm.Models
{
    [Table("Product")]
    public partial class Product
    {
        [Key]
        public long Id { get; set; }
        [StringLength(250)]
        public string MetaTitle { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [StringLength(250)]
        public string Image { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? PromotionPrice { get; set; }
        public int? Quantity { get; set; }
        public long CategoryId { get; set; }
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }
        public bool? Status { get; set; }
    }
}
