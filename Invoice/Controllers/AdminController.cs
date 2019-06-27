using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Invoice.Core.Entity;
using Invoice.Core.Interfaces;
using Invoice.Core.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Linq;

namespace Invoice.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ISupplierRepository supplierRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStoreSettingRepository storeSettingRepository;
        private readonly ISaleRepository saleRepository;
        private static readonly HttpClient client = new HttpClient();

        public AdminController(ICustomerRepository customerRepository,
                                ISupplierRepository supplierRepository,
                                IProductRepository productRepository, 
                                IStoreSettingRepository storeSettingRepository,
                                ISaleRepository saleRepository)
        {
            this.supplierRepository = supplierRepository;
            this.customerRepository = customerRepository;
            _productRepository = productRepository;
            this.storeSettingRepository = storeSettingRepository;
            this.saleRepository = saleRepository;
        }
        public IActionResult Index()
        {
            var dashboard= new DashboardViewModel
            {

                Customers = customerRepository.All().Count(),
                Products = _productRepository.All().Count(),
                TotalSales = saleRepository.All().Count(),
                TotalSaleValue = saleRepository.All().Sum(x => x.GrandTotal),
                LastFiveSales = saleRepository.All().OrderByDescending(x => x.SalesDate).Take(5).ToList()
            };
            return View(dashboard);
        }
      
        [HttpGet]
        public ActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSupplier(SupplierModel model)
        {
            if (ModelState.IsValid)
            {
                supplierRepository.Insert(model);
                return RedirectToAction("SupplierList");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditSupplier(int supplierId)
        {
            var supplier = supplierRepository.Find(supplierId);
            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSupplier(SupplierModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            supplierRepository.Update(model, model.Id);
            return RedirectToAction("SupplierList");
        }

        public ActionResult DeleteSupplier(int supplierId)
        {
            var supplier = supplierRepository.Find(supplierId);
            if (supplier != null)
            {
                supplierRepository.Delete(supplier);
                return RedirectToAction("SupplierList");
            }
            return RedirectToAction("SupplierList");
        }


        public IActionResult SupplierList()
        {
            var suppliers = supplierRepository.All();
            return View(suppliers);
        }



        #region customer
        public IActionResult CustomerList()
        {
            ShowCustomerViewModel InfoCustomers = new ShowCustomerViewModel
            {
                Customers = customerRepository.All()
            };
            return View(InfoCustomers);
        }


        [HttpGet]
        public ActionResult GetCustomerProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCustomerProfile(ShowCustomerViewModel model)
        {
            var customer = customerRepository.Find(model.Customer.Id);
            model.Customer = customer;
            return View(model);
        }

        [HttpGet]
        public ActionResult AddCustomerAnaf()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomerAnaf(CustomerModel model)
        {
           
                customerRepository.Insert(model);
                return RedirectToAction("CustomerList");
            
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(String))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ReturnInfoAnaf(InfoAnafViewModel model)
        {
            model.data = DateTime.Today.ToString("yyyy-MM-dd");

            HttpResponseMessage result = await client.PostAsJsonAsync("https://webservicesp.anaf.ro/PlatitorTvaRest/api/v3/ws/tva",
                                             new List<InfoAnafViewModel>() { model });
            HttpContent content = result.Content;
            string resultContent = await content.ReadAsStringAsync();
            ReturnedInfoAnafViewModel returnedInfo = new ReturnedInfoAnafViewModel();
            if (resultContent != null)
            {
                dynamic json = JValue.Parse(resultContent);
                dynamic requestedFirm = json.found[0];
                returnedInfo = new ReturnedInfoAnafViewModel
                {
                    cui = requestedFirm.cui,
                    Address = requestedFirm.adresa,
                    FirmName = requestedFirm.denumire,
                    scpTVA = requestedFirm.scpTVA,
                };
            }
            else
            {
                return RedirectToAction("ErrorRegister");
            }

            return View(returnedInfo);
        }



        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Insert(model);
                return RedirectToAction("CustomerList");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditCustomer(int customerId)
        {
            var customer = customerRepository.Find(customerId);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(CustomerModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            customerRepository.Update(model, model.Id);
            return RedirectToAction("CustomerList");
        }

        public ActionResult DeleteCustomer(int customerId)
        {
            var customer = customerRepository.Find(customerId);
            if (customer != null)
            {
                customerRepository.Delete(customer);
                return RedirectToAction("CustomerList");
            }
            return RedirectToAction("CustomerList");
        }
        #endregion

        #region Product
        public IActionResult ProductList()
        {
            var products = _productRepository.All();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _productRepository.Insert(model);
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public IActionResult EditProduct(int productId)
        {
            var product = _productRepository.Find(productId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _productRepository.Update(model, model.Id);
            return RedirectToAction("ProductList");
        }
        public ActionResult DeleteProduct(int productId)
        {
            var product = _productRepository.Find(productId);
            if (product != null)
            {
                _productRepository.Delete(product);
                return RedirectToAction("ProductList");
            }
            return RedirectToAction("ProductList");
        }


       
        #endregion

        #region Sales
        public IActionResult SalesList()
        {
            return View();
        }
        #endregion

        #region store
        [HttpGet]
        public IActionResult Settings()
        {
            var settings = storeSettingRepository.All().FirstOrDefault();
            if (settings != null)
                return View(settings);
            return View(new StoreSettingModel());
        }


        [HttpPost]
        public IActionResult Settings(StoreSettingModel model, IFormFile logoPostedFileBase)
        {
            if (!ModelState.IsValid) return View(model);
            if (logoPostedFileBase != null && logoPostedFileBase.Length > 0)
            {
                var path = Path.Combine(
                 Directory.GetCurrentDirectory(), "wwwroot/Images",
                 logoPostedFileBase.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    logoPostedFileBase.CopyTo(stream);
                }
                model.Logo = $"/Images/{logoPostedFileBase.FileName}";
            }
            else
            {
                var settings = storeSettingRepository.Find(model.Id);
                if (settings != null)
                    model.Logo = settings.Logo;
            }
            storeSettingRepository.Update(model, model.Id);
            return RedirectToAction("Settings");
        }
        #endregion

        #region json
        public JsonResult Products(string query)
        {
            return Json(_productRepository.All().Where(x => x.Name.ToLower().Contains(query.ToLower())));
        }

        #endregion

    }
}