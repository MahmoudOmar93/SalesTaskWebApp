using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTaskWebApp.Models.ViewModel
{
    public class ProductToCreateViewModel
    {
        [Required]
        [Remote("CodeIsAlreadyExist", "Product", HttpMethod = "POST", ErrorMessage = "Code already exists in database.")]
        public string Code { get; set; }

        [Required]
        [Remote("NameIsAlreadyExist", "Product", HttpMethod = "POST", ErrorMessage = "Name already exists in database.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Sub Category")]
        public int SubCategoryId { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
    }
}