using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace asm.Models
{
    public partial class Customer
    {
        [Key]
        public long Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(250)]
        public string User { get; set; }
        [StringLength(250)]
        public string PassWord { get; set; }
        public int? Position { get; set; }
        public bool? Status { get; set; }
    }
}
