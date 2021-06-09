using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HrWebApp1.Models;
using HrWebApp1.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HrWebApp1.Controllers
{
    public class AuthController : Controller
    {
        private const string APP_PATH = "http://localhost:44340";
        private static string token;
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Regis() {
            return View();
        }
        public IActionResult EditRegis()
        {
            User user = CheckUser();
            if (user != null)
                return View(user);
            return RedirectToAction("logout");
        }
        public IActionResult GetUser() {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
                var response = client.GetAsync("api/auth").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                //Dictionary<string, string> resDes =
                //    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                ViewBag.Status = response.StatusCode.ToString();

                List<RegisViewModel> tmp = JsonConvert.DeserializeObject<List<RegisViewModel>>(result);
                string res = "";
                foreach (RegisViewModel r in tmp) {
                    res += r.Email + r.Name+ r.Password+"</br> ";
                }
                return Content(res);
            }
            return Content("asd");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44340/");
                    var content = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(model),
                        Encoding.UTF8,
                        "application/json");
                    var response = client.PostAsync("api/auth/signin", content).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    //Dictionary<string, string> resDes =
                    //    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                    ViewBag.Status = response.StatusCode.ToString();
                    if (response.StatusCode.ToString() != "NotFound")
                    {
                        try
                        {
                            User user = JsonConvert.DeserializeObject<User>(result);
                            if (user != null)
                            {
                                //return Content(user.Email + " password: " + user.Password + " name: " + user.Name + " password accepted");
                                
                                    await Authenticate(user); // аутентификация

                                    return RedirectToAction("Index");
                            }
                        }
                        catch (JsonSerializationException)
                        {
                            return null;
                        }
                    }
                    ModelState.AddModelError("", response.StatusCode.ToString());

                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisAsync(RegisViewModel regData) {
            if (ModelState.IsValid) {
                using (var client = new HttpClient())
                {
                    //var myContent = JsonConvert.SerializeObject(regData);
                    //var buffer = Encoding.UTF8.GetBytes(myContent);
                    //var byteContent = new ByteArrayContent(buffer);
                    //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    User user = new User { Email = regData.Email, Name = regData.Name, RoleId = 3 };

                    client.BaseAddress = new Uri("https://localhost:44340/");
                    
                    var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(regData),
                    Encoding.UTF8,
                    "application/json");

                    var response = client.PostAsync("api/auth", content).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    if (response.StatusCode.ToString() == "OK")
                    {
                        await Authenticate(user);

                        return RedirectToAction("Index");
                    }
                }
            }
            ModelState.AddModelError("", "Пожалуйста, заполните все необходимые поля!");
            return View(regData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRegisAsync(User user)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44340/");
                    User user1 = CheckUser();
                    if (user1!=null)
                    {
                        user1.Surname = user.Surname;
                        user1.Name = user.Name;
                        user1.Lastname = user.Lastname;
                        user1.Male = user.Male;
                        user1.BirthDate = user.BirthDate;
                        user1.Phone = user.Phone;
                        user1.Email = user.Email;
                        var content = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(user1),
                        Encoding.UTF8,
                        "application/json");

                        var response1 = client.PutAsync("api/auth/edituser/" + user1.Id, content).Result;
                        ViewBag.Status = response1.StatusCode.ToString();
                        if (response1.StatusCode.ToString() == "NoContent")
                        {
                            await Authenticate(user1);

                            return RedirectToAction("Index");
                        }

                        ModelState.AddModelError("", response1.StatusCode.ToString());
                    }
                    return RedirectToAction("Logout");
                }
                ModelState.AddModelError("", "Клиент не работает");
            }
            ModelState.AddModelError("", "Что то не так");
            return View(user);
        }

        public IActionResult Priv()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
                User user = CheckUser();
                var response = client.GetAsync(string.Format("api/UserPrivs/{0}", user.Id)).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                //Dictionary<string, string> resDes =
                //    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                ViewBag.Status = response.StatusCode.ToString();
                if (response.StatusCode.ToString() != "NotFound")
                {
                    try
                    {
                        PrivViewModel tmp = JsonConvert.DeserializeObject<PrivViewModel>(result);
                        return View(tmp);
                    }
                    catch (JsonSerializationException)
                    {
                        return null;
                    }
                }

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Priv(PrivViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44340/");
                    User user = CheckUser();
                    if (user!=null)
                    {
                        model.userid = user.Id;
                        var content = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(model),
                        Encoding.UTF8,
                        "application/json");

                        var response1 = client.PostAsync("api/UserPrivs", content).Result;
                        ViewBag.Status = response1.StatusCode.ToString();
                        if (response1.StatusCode.ToString() == "Created")
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Response pizda!");
                            return View(model);
                        }
                    }
                }
                ModelState.AddModelError("", "Client pizda!");
            }
            else
            {
                ModelState.AddModelError("", "Что то не так!");
            }
            return View(model);
            //return RedirectToAction("TestResult", model.Children);
        }

        public async Task<IActionResult> TestResult(Childrens model)
        {
            string json = JsonConvert.SerializeObject(model);
            return Content(json);
        }

        private async Task Authenticate(User user)
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId==3?"user":"admin")
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        private User CheckUser()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");

                var response = client.GetAsync(string.Format("api/auth/{0}", User.Identity.Name)).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                //Dictionary<string, string> resDes =
                //    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                ViewBag.Status = response.StatusCode.ToString();
                if (response.StatusCode.ToString() != "NotFound")
                {
                    try
                    {
                        User tmp = JsonConvert.DeserializeObject<User>(result);
                        return tmp;
                    }
                    catch (JsonSerializationException)
                    {
                        return null;
                    }
                }
                return null;
            }
        }
    }
}
