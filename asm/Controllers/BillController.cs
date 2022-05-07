using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using asm.Models;
namespace asm.Controllers
{
    public class BillController : Controller
    {
        private readonly fullContext _context;

        public BillController(fullContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.messtc = HttpContext.Session.GetString("messtc");
            ViewBag.messtb = HttpContext.Session.GetString("messtb");
            var cart = JsonConvert.DeserializeObject(HttpContext.Session.GetString("cartItem"));
            ViewBag.Bill = cart;
            ViewBag.total = HttpContext.Session.GetString("total");

            return View();
        }
        public IActionResult Pay(string? money)
        {
            if (money == null)
            {
                return NotFound();
            }
            List<Product> product = new List<Product>();
            product = _context.Products.ToList();
            List<cartItem> cartItems = new List<cartItem>();
            cartItems = _context.cartItems.ToList();
            string mon = HttpContext.Session.GetString("total");
            if (Convert.ToInt64(money) - Convert.ToInt64(mon) >= 0)
            {
                Cart cart = new Cart();
                cart.CreateDate = DateTime.Now;
                cart.Status = true;
                _context.Add(cart);
                _context.SaveChanges();
                for (int i = 0; i < cartItems.Count; i++)
                {

                    CartDetail cartDetail = new CartDetail();
                    cartDetail.CartId = cart.Id;
                    cartDetail.ProductId = cartItems[i].ProductId;
                    cartDetail.Price = cartItems[i].Price;
                    cartDetail.Quantity = cartItems[i].Quantity;
                    cartDetail.Status = true;
                    _context.Add(cartDetail);
                }
                for (int i = 0; i < cartItems.Count; i++)
                {
                    for (int j = 0; j < product.Count; j++)
                    {
                        if (cartItems[i].ProductId == product[j].Id)
                        {
                            product[j].Quantity = product[j].Quantity - cartItems[i].Quantity;
                        }
                    }
                }
                foreach (var x in product)
                {
                    _context.Update(x);
                }
                HttpContext.Session.SetString("messtc", "Thanh toán thành công");
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            HttpContext.Session.SetString("messtb", "Thanh toán thất bại");
            return RedirectToAction(nameof(Index));
        }
    }
}
