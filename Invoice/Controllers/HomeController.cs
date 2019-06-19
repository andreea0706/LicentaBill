using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Invoice.Core.Entity;
using Invoice.Core.Interfaces.Base;
using Invoice.Core.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace Invoice.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;
        private static readonly HttpClient client = new HttpClient();

        public HomeController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index","home");
        }

        [HttpGet]
        public IActionResult GetInfoAnaf()
        {
            return View();
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(String))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ReturnInfoAnaf(InfoAnafViewModel model)
        {
            model.data = DateTime.Today.ToString("yyyy-MM-dd");
            
            HttpResponseMessage result = await client.PostAsJsonAsync(
            "https://webservicesp.anaf.ro/PlatitorTvaRest/api/v3/ws/tva",
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
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ReturnedInfoAnafViewModel model)
        {
            UserModel newUser = new UserModel
            {
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
                cui = model.cui,
                FirmName = model.FirmName,
                Address = model.Address,
                scpTVA = model.scpTVA
            };
           
            userRepository.Insert(newUser);
            return RedirectToAction("RegistrationConfirmation");
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistrationConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                bool result= userRepository.ValidateUser(user);
                if (result)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email)
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";
                    return View();
                }
            }
            else
                return View(user);
        }
    
    public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Latest()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
