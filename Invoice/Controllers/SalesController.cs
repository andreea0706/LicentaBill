using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoice.Core.Entity;
using Invoice.Core.Interfaces;
using Invoice.Core.ViewModel;
using Invoice.Data.AppDataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PharmaApp.Web.Reports;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
namespace Invoice.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IProductRepository productRepository;
        private readonly ISaleRepository saleRepository;
        private readonly INirRepository nirRepository;
        private readonly ISupplierRepository supplierRepository;
        private readonly ISaleItemRepository saleItemRepository;
        private readonly INirItemRepository nirItemRepository;
        private readonly IStoreSettingRepository storeSettingRepository;
        private readonly IConfiguration configuration;
        private readonly InvoiceDbContext _context;

        public SalesController(ICustomerRepository customerRepository,
                                ISaleRepository saleRepository,
                                IProductRepository productRepository,
                                INirRepository nirRepository,
                                INirItemRepository nirItemRepository,
                                ISupplierRepository supplierRepository,
                                ISaleItemRepository saleItemRepository,
                                IStoreSettingRepository storeSettingRepository,
                                IConfiguration configuration,
                                InvoiceDbContext context)
        {
            this.nirRepository = nirRepository;
            this.productRepository = productRepository;
            this.nirItemRepository = nirItemRepository;
            this.supplierRepository = supplierRepository;
            this.configuration = configuration;
            this.customerRepository = customerRepository;
            this.saleRepository = saleRepository;
            this.saleItemRepository = saleItemRepository;
            this.storeSettingRepository = storeSettingRepository;
            _context = context;

        }

        public IActionResult Index()
        {
            return View(saleRepository.All());
        }

        public IActionResult ListNir()
        {
            return View(nirRepository.All());
        }


        [HttpGet]
        public IActionResult AddNir()
        {
            ViewBag.suppliers = supplierRepository.GetAllForDropDown();
            return View();
        }


        [HttpPost]
        public IActionResult AddNir([FromBody] NirModel model)
        {
            if (ModelState.IsValid)
            {
                nirRepository.Insert(model);
                foreach (NirItemsModel prod in model.Items)
                {
                    var incProdStoc = productRepository.All().FirstOrDefault(product => product.Name == prod.Name);
                    incProdStoc.Stoc += prod.Quantity;
                    _context.SaveChanges();
                    
                }
                return Json(new { error = false, message = "Sales saved successfully" });
            }
            return Json(new { error = true, message = "failed to save Sales" });
        }


        public IActionResult SendEmail(int saleId)
        {
            var currentSale = saleRepository.Find(saleId);
            var currentUser = customerRepository.Find(currentSale.CustomerId);

            //Instantiate MimeMessage
            var message = new MimeMessage();

            //From Address
            //cum vad clientii adresa mea
            message.From.Add(new MailboxAddress("Admin Online Invoice Generator", "admin@faconl.com"));
            //To Address
            message.To.Add(new MailboxAddress(currentUser.Name, currentUser.Email));

            //Subject
            message.Subject  = "Factura a fost emisa!";
            //Body
            message.Body = new TextPart("plain")
            {
                Text = "Factura a fost emisa"
            };

            //Configure and send the message 
            using(var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                //adresa de pe care se dau mesajele
                client.Authenticate("andreeateodoraionescu@gmail.com", "Matematica11");
                client.Send(message);
                client.Disconnect(true);
            }
            return RedirectToAction("index");
        }


        [HttpGet]
        public IActionResult EditNir(int nirId)
        {
            return View(model: nirId);
        }

        [HttpPost]
        public IActionResult EditNir(NirModel model)
        {
            if (model != null)
            {
                var items = nirItemRepository.All().Where(x => x.NirId == model.Id).ToList();
                if (items.Any())
                {
                    foreach (var item in items)
                    {
                        nirItemRepository.Delete(item);
                    }
                }

                var nir = nirRepository.Find(model.Id);
                nir.Notes = model.Notes;
                nir.PaymentMethod = model.PaymentMethod;
                nir.NirCode = model.NirCode;
                nir.SupplierId = model.SupplierId;
                nir.Total = model.Total;
                nir.NirDate = model.NirDate;
                nir.Status = model.Status;
                nir.Discount = model.Discount;
                nir.GrandTotal = model.GrandTotal;

                foreach (var item in model.Items)
                {
                    nir.Items.Add(new NirItemsModel
                    {
                        Price = item.Price,
                        Amount = item.Amount,
                        Quantity = item.Quantity,
                        Name = item.Name
                    });
                }
                nirRepository.Update(nir, model.Id);
                return Json(new { error = false, message = "Nota intrare Receptie actualizata cu succes!" });
            }
            return Json(new { error = true, message = "Esuare actualizare Nota Intrare Receptie!" });
        }

        public IActionResult DeleteNir(int nirId)
        {
            var item = this.nirRepository.Find(nirId);
            nirRepository.Delete(item);
            return RedirectToAction("index");
        }

        public ActionResult NirDoc(int nirId, int style)
        {
            if (storeSettingRepository.All().Count() == 0)
            {
                TempData["Msg"] = "Seteaza inainte detalii firmei tale";
                return RedirectToAction("Index");
            }
            var store = storeSettingRepository.All().FirstOrDefault();

            var nir = nirRepository.All().Include(x => x.SupplierModel).SingleOrDefault(x => x.Id == nirId);
            nir.Items = nirItemRepository.All().Where(x => x.NirId == nirId).ToList();
            if (nir != null)
            {
                var nirs = new NirReportViewModel
                {
                    company = store,
                    Nirs = nir
                };
                if (style == 1)
                {
                    NirReport paymentReport = new NirReport(configuration);
                    byte[] bytes = paymentReport.CreateReport(nirs);
                    return File(bytes, "application/pdf");
                }
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult AddSale()
        {
            ViewBag.customers = customerRepository.GetAllForDropDown();
            return View();
        }

        [HttpPost]
        public IActionResult AddSale([FromBody] SalesModel model)
        {
            if (ModelState.IsValid)
            {
                saleRepository.Insert(model);
                return Json(new { error = false, message = "Sales saved successfully" });
            }
            return Json(new { error = true, message = "failed to save Sales" });
        }

        [HttpGet]
        public IActionResult EditSale(int saleId)
        {
            return View(model: saleId);
        }
        [HttpPost]
        public IActionResult EditSale(SalesModel model)
        {
            if (model != null)
            {
                var items = saleItemRepository.All().Where(x => x.SalesId == model.Id).ToList();
                if (items.Any())
                {
                    foreach (var item in items)
                    {
                        saleItemRepository.Delete(item);
                    }
                }

                var sale = saleRepository.Find(model.Id);
                sale.Notes = model.Notes;
                sale.PaymentMethod = model.PaymentMethod;
                sale.SaleCode = model.SaleCode;
                sale.CustomerId = model.CustomerId;
                sale.Total = model.Total;
                sale.SalesDate = model.SalesDate;
                sale.Status = model.Status;
                sale.Discount = model.Discount;
                sale.GrandTotal = model.GrandTotal;

                //add again
                foreach (var item in model.Items)
                {
                    sale.Items.Add(new SalesItemsModel
                    {
                        Price = item.Price,
                        Amount = item.Amount,
                        Quantity = item.Quantity,
                        Name = item.Name
                    });
                }
                saleRepository.Update(sale, model.Id);
                return Json(new { error = false, message = "Sales updated successfully" });
            }
            return Json(new { error = true, message = "failed to update Sales" });
        }

        public IActionResult DeleteSale(int saleId)
        {
            var item = this.saleRepository.Find(saleId);
            saleRepository.Delete(item);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult SalesReport(DateTime? From, DateTime? To)
        {
            if (From.HasValue && To.HasValue)
                return View(saleRepository.All().Where(x => x.SalesDate >= From && x.SalesDate <= To));
            return View(new List<SalesModel>());
        }

        public ActionResult SaleInvoice(int saleId, int style)
        {
            if (storeSettingRepository.All().Count() == 0)
            {
                TempData["Msg"] = "Setup store setting first then print sale invoice";
                return RedirectToAction("Index");
            }
            var store = storeSettingRepository.All().FirstOrDefault();

            var sale = saleRepository.All().Include(x => x.CustomerModel).SingleOrDefault(x => x.Id == saleId);
            sale.Items = saleItemRepository.All().Where(x => x.SalesId == saleId).ToList();
            if (sale != null)
            {
                var sales = new SaleReportViewModel
                {
                    company = store,
                    Sales = sale
                };
                if (style == 1)
                {
                    SalesReport paymentReport = new SalesReport(configuration);
                    byte[] bytes = paymentReport.CreateReport(sales);
                    return File(bytes, "application/pdf");
                }
                if (style == 2)
                {
                    SalesReportSmall paymentReport = new SalesReportSmall();
                    byte[] bytes = paymentReport.CreateReport(sales);
                    return File(bytes, "application/pdf");
                }
            }
            return RedirectToAction("index");
        }

        public JsonResult GetCustomers()
        {
            return Json(customerRepository.All());
        }

        public JsonResult GetSuppliers()
        {
            return Json(supplierRepository.All());
        }

        [HttpGet]
        public JsonResult GetSales(int saleId)
        {
            try
            {
                SalesModel sales = saleRepository.All().FirstOrDefault(x => x.Id == saleId);
                sales.Items = saleItemRepository.All().Where(x => x.SalesId == sales.Id).ToList();
              
                return Json(sales);
            }
            catch (Exception e)
            {
                return Json(e.ToString());
            }
        }

        [HttpGet]
        public JsonResult GetNir(int nirId)
        {
            try
            {
                NirModel nirs = nirRepository.All().FirstOrDefault(x => x.Id == nirId);
                nirs.Items = nirItemRepository.All().Where(x => x.NirId == nirs.Id).ToList();

                return Json(nirs);
            }
            catch (Exception e)
            {
                return Json(e.ToString());
            }
        }
    }
}