using SalesTaskWebApp.DataContext;
using SalesTaskWebApp.Models;
using SalesTaskWebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SalesContext _context;

        public ProductRepository(SalesContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductAll()
        {
            IEnumerable<Product> product = _context.Product;

            return product;
        }

        public void Create(ProductToCreateViewModel viewModel)
        {
            Product product = new Product()
            {
                Code = viewModel.Code,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                SubCategoryId = viewModel.SubCategoryId,
                BrandId = viewModel.BrandId
            };
            _context.Product.Add(product);
            _context.SaveChanges();
        }

        public bool CheckProductCode(string code)
        {
            bool result = _context.Product.Where(x => x.Code == code).FirstOrDefault() != null;

            return !result;
        }

        public bool CheckProductName(string name)
        {
            bool result = _context.Product.Where(x => x.Name == name).FirstOrDefault() != null;

            return !result;
        }

        public Product GetProductByCode(string code)
        {
            Product product = _context.Product.Where(x => x.Code == code).FirstOrDefault();

            return product;
        }

        public Product GetProductByName(string name)
        {
            Product product = _context.Product.Where(x => x.Name == name).FirstOrDefault();

            return product;
        }

        public IEnumerable<Product> GetAllProductAutoCompleteByCode(string code)
        {
            IEnumerable<Product> product = _context.Product.Where(x => x.Code.StartsWith(code));

            return product;
        }

        public IEnumerable<Product> GetAllProductAutoCompleteByName(string name)
        {
            IEnumerable<Product> product = _context.Product.Where(x => x.Name.StartsWith(name));

            return product;
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}