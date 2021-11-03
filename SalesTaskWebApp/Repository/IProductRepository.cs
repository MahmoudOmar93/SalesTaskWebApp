using SalesTaskWebApp.Models;
using SalesTaskWebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Repository
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Product> GetProductAll();
        Product GetProductByCode(string code);
        Product GetProductByName(string Name);
        void Create(ProductToCreateViewModel viewModel);
        bool CheckProductCode(string code);
        bool CheckProductName(string name);
        IEnumerable<Product> GetAllProductAutoCompleteByCode(string code);
        IEnumerable<Product> GetAllProductAutoCompleteByName(string name);
    }
}