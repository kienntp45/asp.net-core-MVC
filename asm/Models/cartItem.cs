using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace asm.Models
{
    public class cartItem
    {
        [Key]
        public long ProductId { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Image { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
