using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Models.ViewModel
{
    public class OrderToListViewModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Customer { get; set; }
        public DateTime Date { get; set; }
        public int ProductQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}