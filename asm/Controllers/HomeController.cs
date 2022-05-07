using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asm.Models;
using Microsoft.AspNetCore.Http;
namespace asm.Controllers
{
    public class HomeController : Controller
    {
        private readonly fullContext _context ;

        public HomeController(fullContext context)
        {
            _context = context;
        }
        // GET: ProductCategories
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("messAddCart")== "Đăng nhập để mua hàng")
            {
                ViewBag.messAddCart = "Đăng nhập để mua hàng";
            }
            var data = _context.Products.ToList();
            ViewData["product"] = _context.Products.ToList();
            return View();
        }
       
    }
}
