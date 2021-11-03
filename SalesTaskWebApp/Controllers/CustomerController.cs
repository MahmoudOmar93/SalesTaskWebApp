using SalesTaskWebApp.DataContext;
using SalesTaskWebApp.Models;
using SalesTaskWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTaskWebApp.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepo;
        public CustomerController()
        {
            _customerRepo = new CustomerRepository(new SalesContext());
        }
        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public ActionResult Index()
        {
            IEnumerable<Customer> list = _customerRepo.GetCustomerAll();

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code ,Name, Phone, Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepo.Create(customer);

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        [HttpPost]
        public JsonResult CodeIsAlreadyExist(string Code)
        {
            return Json(_customerRepo.CheckCustomerCode(Code));
        }

    }
}