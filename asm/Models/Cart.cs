using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace asm.Models
{
    public partial class Cart
    {
        [Key]
        public long Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
        public long? CustomerId { get; set; }
        public bool? Status { get; set; }
    }
}
