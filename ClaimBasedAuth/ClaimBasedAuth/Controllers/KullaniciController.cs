using ClaimBasedAuth.Models;
using ClaimBasedAuth.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClaimBasedAuth.Controllers
{
    public class KullaniciController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Giris(string? nerelereGidem = null)
        {
            ViewBag.ReturnUrl = nerelereGidem;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Giris(UserLoginModel userLoginModel, string nerelereGidem)
        {

            if (ModelState.IsValid)
            {
                UserService userService = new UserService();
                var user = userService.ValidateUser(userLoginModel.UserName, userLoginModel.Password);
                if (user != null)
                {
                    Claim[] claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role,user.Role)

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
                    if (!string.IsNullOrEmpty(nerelereGidem) && Url.IsLocalUrl(nerelereGidem))
                    {
                        return Redirect(nerelereGidem);
                    }
                    return Redirect("/");
                }
                ModelState.AddModelError("login", "Kullanıcı adı ya da şifre yanlış");
            }

            return View();
        }

        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult Yasak()
        {
            return View();
        }
    }
}
