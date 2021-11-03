using SalesTaskWebApp.DataContext;
using SalesTaskWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SalesContext _context;

        public CustomerRepository(SalesContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetCustomerAll()
        {
            IEnumerable<Customer> customer = _context.Customer;

            return customer;
        }

        public bool CheckCustomerCode(string code)
        {
            bool result = _context.Customer.Where(x => x.Code == code).FirstOrDefault() != null;

            return !result;
        }

        public void Create(Customer customer)
        {
            _context.Customer.Add(customer);
            _context.SaveChanges();
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