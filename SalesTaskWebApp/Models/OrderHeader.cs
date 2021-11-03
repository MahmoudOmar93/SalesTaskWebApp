using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        public int OrderNumber { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public int OrderType { get; set; } // 0 => For Sales & 1 => For Purchases 
    }
}