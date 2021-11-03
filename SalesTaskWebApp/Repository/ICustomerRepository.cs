using SalesTaskWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Repository
{
    public interface ICustomerRepository : IDisposable
    {
        IEnumerable<Customer> GetCustomerAll();
        void Create(Customer customer);
        bool CheckCustomerCode(string code);
    }
}