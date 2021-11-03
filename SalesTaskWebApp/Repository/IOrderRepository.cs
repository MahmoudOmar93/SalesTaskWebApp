using SalesTaskWebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Repository
{
    public interface IOrderRepository : IDisposable
    {
        IEnumerable<OrderToListViewModel> GetAllOrderByOrderTypeId(uint orderType);
        void Create(OrderToCreateViewModel viewModel);
        int GetOrderNumberMaxByOrderType(int orderType);
    }
}