using SalesTaskWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Repository
{
    public interface IBrandRepository : IDisposable
    {
        IEnumerable<Brand> GetBrandAll();
    }
}