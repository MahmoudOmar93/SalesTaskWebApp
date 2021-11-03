using SalesTaskWebApp.DataContext;
using SalesTaskWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly SalesContext _context;

        public BrandRepository(SalesContext context)
        {
            _context = context;
        }

        public IEnumerable<Brand> GetBrandAll()
        {
            IEnumerable<Brand> brands = _context.Brand;

            return brands;
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