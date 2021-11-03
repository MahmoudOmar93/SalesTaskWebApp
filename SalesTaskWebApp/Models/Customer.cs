using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTaskWebApp.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Remote("CodeIsAlreadyExist", "Customer", HttpMethod = "POST", ErrorMessage = "Code already exists in database.")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}