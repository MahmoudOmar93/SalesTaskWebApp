using SalesTaskWebApp.DataContext;
using SalesTaskWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly SalesContext _context;

        public SubCategoryRepository(SalesContext context)
        {
            _context = context;
        }

        public IEnumerable<SubCategory> GetAllSubCategoryByCategoryId(int categoryId)
        {
            IEnumerable<SubCategory> subCategories = _context.SubCategory.Where(x => x.CategoryId == categoryId);

            return subCategories;
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