using asm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asm.Controllers
{
    public class LoginController : Controller
    {
        private readonly fullContext _context;

        public LoginController(fullContext context)
        {
            _context = context;
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult veryfi(string user, string pass)
        {
            Customer customer = new Customer();
            customer = _context.Customers.ToList().Where(c => c.User == user && c.PassWord == pass).FirstOrDefault();
            if (customer != null)
            {
                HttpContext.Session.SetString("position", customer.Position.ToString());
                ViewBag.loginFail = "";
                HttpContext.Session.SetString("user", user);
                ViewBag.user = user;
                HttpContext.Session.SetString("messAddCart", "Đã đăng nhập");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session.SetString("loginFail", "Đăng nhập thất bại");
                ViewBag.loginFail = "Đăng nhập thất bại";
                return View("login");
            }

        }

        public IActionResult lout()
        {

            HttpContext.Session.SetString("user", "");
            HttpContext.Session.SetString("position", "");
            ViewBag.loginFail = "";
            HttpContext.Session.SetString("messAddCart", "");
            return RedirectToAction("Index", "Home");



        }
    }
}
