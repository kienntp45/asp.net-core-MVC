using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace asm.Models
{
    [Table("CartDetail")]
    public partial class CartDetail
    {
        [Key]
        public long ProductId { get; set; }
        [Key]
        public long CartId { get; set; }
        public int? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Price { get; set; }
        public bool? Status { get; set; }
    }
}
