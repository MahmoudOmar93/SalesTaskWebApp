using SalesTaskWebApp.DataContext;
using SalesTaskWebApp.Models.ViewModel;
using SalesTaskWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTaskWebApp.Controllers
{
    public class SalesController : Controller
    {
        private IOrderRepository _orderRepo;
        private IProductRepository _productRepo;
        private ICustomerRepository _customerRepo;
        public SalesController()
        {
            _orderRepo = new OrderRepository(new SalesContext());
            _productRepo = new ProductRepository(new SalesContext());
            _customerRepo = new CustomerRepository(new SalesContext());
        }
        public SalesController(IOrderRepository orderRepo, IProductRepository productRepo, ICustomerRepository customerRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _customerRepo = customerRepo;
        }

        // GET: Sales
        public ActionResult Index()
        {
            IEnumerable<OrderToListViewModel> list = _orderRepo.GetAllOrderByOrderTypeId(0);
            return View(list);
        }

        public ActionResult Create()
        {
            ViewBag.CustomerList = new SelectList(_customerRepo.GetCustomerAll(), "Id", "Name");

            int orderNumber = _orderRepo.GetOrderNumberMaxByOrderType(0);
            OrderToCreateViewModel viewModel = new OrderToCreateViewModel()
            {
                OrderNumber = orderNumber
            };
             
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "OrderNumber, ProductCode, ProductName, Price, Discount, Quantity, " +
            "TotalPrice, OrderHeader, OrderDetails")] OrderToCreateViewModel formData)
        {
            if(ModelState.IsValid)
            {
                _orderRepo.Create(formData);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}