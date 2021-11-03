using SalesTaskWebApp.DataContext;
using SalesTaskWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SalesContext _context;

        public CategoryRepository(SalesContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategroyAll()
        {
            IEnumerable<Category> category = _context.Category;

            return category;
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