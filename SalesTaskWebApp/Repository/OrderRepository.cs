using SalesTaskWebApp.DataContext;
using SalesTaskWebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesTaskWebApp.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesContext _context;

        public OrderRepository(SalesContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderToListViewModel> GetAllOrderByOrderTypeId(uint orderType)
        {
            IEnumerable<OrderToListViewModel> list = _context.OrderHeader.Where(x => x.OrderType == orderType)
                .Select(x => new OrderToListViewModel
                {
                    Id = x.Id,
                    OrderNumber = x.OrderNumber,
                    Customer = x.Customer.Name,
                    Date = x.OrderDate,
                    ProductQuantity = _context.OrderDetail.Where(o => o.OrderHeaderId == x.Id).Sum(o => o.Quantity),
                    TotalPrice = _context.OrderDetail.Where(o => o.OrderHeaderId == x.Id).Sum(o => o.TotalPrice)
                });

            return list;
        }

        public void Create(OrderToCreateViewModel viewModel)
        {
            viewModel.OrderHeader.OrderNumber = GetOrderNumberMaxByOrderType(0);
            _context.OrderHeader.Add(viewModel.OrderHeader);
            _context.OrderDetail.AddRange(viewModel.OrderDetails);

            _context.SaveChanges();
        }

        public int GetOrderNumberMaxByOrderType(int orderType)
        {
            return _context.OrderHeader.Where(x => x.OrderType == orderType).Max(x => x.OrderNumber) + 1;
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