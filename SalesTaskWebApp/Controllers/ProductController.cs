using SalesTaskWebApp.DataContext;
using SalesTaskWebApp.Models;
using SalesTaskWebApp.Models.ViewModel;
using SalesTaskWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SalesTaskWebApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepo;
        private ICategoryRepository _categoryRepo;
        private ISubCategoryRepository _subCategoryRepo;
        private IBrandRepository _brandRepo;
        public ProductController()
        {
            _productRepo = new ProductRepository(new SalesContext());
            _categoryRepo = new CategoryRepository(new SalesContext());
            _subCategoryRepo = new SubCategoryRepository(new SalesContext());
            _brandRepo = new BrandRepository(new SalesContext());
        }
        public ProductController(IProductRepository productRepo,ICategoryRepository categoryRepo, 
            ISubCategoryRepository subCategoryRepo, IBrandRepository brandRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _subCategoryRepo = subCategoryRepo;
            _brandRepo = brandRepo;
        }

        public ActionResult Index()
        {
            IEnumerable<Product> list = _productRepo.GetProductAll();

            return View(list);
        }

        public JsonResult GetProductByCode(string code)
        {
            var result = _productRepo.GetProductByCode(code);

            if (result != null)
                return Json(new { Result = result }, JsonRequestBehavior.AllowGet);

            return Json(new { Result = "null" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductByName(string name)
        {
            var result = _productRepo.GetProductByName(name);

            if (result != null)
                return Json(new { Result = result }, JsonRequestBehavior.AllowGet);

            return Json(new { Result = "null" }, JsonRequestBehavior.AllowGet);
        }

        public void FillDrowpDownList()
        {
            ViewBag.CategoryList = new SelectList(_categoryRepo.GetCategroyAll(), "Id", "Name");
            ViewBag.SubCategoryList = new SelectList(_subCategoryRepo.GetAllSubCategoryByCategoryId(0), "Id", "Name");
            ViewBag.BrandList = new SelectList(_brandRepo.GetBrandAll(), "Id", "Name");
        }

        public ActionResult Create()
        {
            FillDrowpDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code ,Name, Description, Price, SubCategoryId, BrandId")] ProductToCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _productRepo.Create(viewModel);

                return RedirectToAction(nameof(Index));
            }
            
            FillDrowpDownList();

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult CodeIsAlreadyExist(string Code)
        {
            return Json(_productRepo.CheckProductCode(Code));
        }

        [HttpPost]
        public JsonResult NameIsAlreadyExist(string Name)
        {
            return Json(_productRepo.CheckProductName(Name));
        }

        // Get: Get City Data By Governorate Id
        [HttpGet]
        public JsonResult GetSubCategoryByCategoryId(string categoryId = "")
        {
            List<SubCategory> subCategoryList = new List<SubCategory>();
            int ID = 0;
            if (int.TryParse(categoryId, out ID))
            {
                subCategoryList = _subCategoryRepo.GetAllSubCategoryByCategoryId(ID).ToList();
            }

            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = subCategoryList,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = "Not Valid request",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

        }

        [HttpPost]
        public JsonResult GetAllProductByCode(string Prefix)
        {
            IEnumerable<Product> product = _productRepo.GetAllProductAutoCompleteByCode(Prefix);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAllProductByName(string Prefix)
        {
            IEnumerable<Product> product = _productRepo.GetAllProductAutoCompleteByName(Prefix);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}