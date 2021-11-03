using SalesTaskWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Repository
{
    public interface ICategoryRepository : IDisposable
    {
        IEnumerable<Category> GetCategroyAll();
    }
}