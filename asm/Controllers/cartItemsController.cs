using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asm.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using asm.Controllers;
namespace asm.Controllers
{
    public class cartItemsController : Controller
    {
        private readonly fullContext _context;

        public cartItemsController(fullContext context)
        {
            _context = context;
        }

        // GET: cartItems
        public async Task<IActionResult> Index()
        {
            List<cartItem> cartItem = new List<cartItem>();
            cartItem = _context.cartItems.ToList();
            string temb = JsonConvert.SerializeObject(cartItem);
            HttpContext.Session.SetString("cartItem", temb);
            ViewBag.Total = cartItem.Sum(p => p.Quantity * p.Price);
            HttpContext.Session.SetString("total", cartItem.Sum(p => p.Quantity * p.Price).ToString());
            return View(await _context.cartItems.ToListAsync());
        }

        // GET: cartItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.cartItems
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }
        public IActionResult cong(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            cartItem cartItem = new cartItem();
            cartItem = _context.cartItems.Where(c => c.ProductId == id).FirstOrDefault();
            if (cartItem == null)
            {
                return NotFound();
            }
            Product product = new Product();
            product = _context.Products.Where(c => c.Id == id).FirstOrDefault();
            if (cartItem.Quantity + 1 > product.Quantity)
            {

                return RedirectToAction(nameof(Index));

            }
            else
            {
                cartItem.Quantity++;
                _context.Update(cartItem);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


        }
        public IActionResult tru(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            cartItem cartItem = new cartItem();
            cartItem = _context.cartItems.Where(c => c.ProductId == id).FirstOrDefault();
            if (cartItem == null)
            {
                return NotFound();
            }
            if (cartItem.Quantity == 1)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                cartItem.Quantity--;
                _context.Update(cartItem);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


        }
        // GET: cartItems/Create
        public IActionResult Create(long id)
        {

            if (id == null)
            {
                return NotFound();
            }
         
            if (HttpContext.Session.GetString("user") == "" || HttpContext.Session.GetString("user") == null)
            {

                HttpContext.Session.SetString("messAddCart", "Đăng nhập để mua hàng");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session.SetString("messAddCart", "");
            }

            cartItem cartItem = new cartItem();
            var cart = _context.Products.Where(p => p.Id == id).FirstOrDefault();
            if (!IsExit(id))
            {
                cartItem.ProductId = id;
                cartItem.Name = cart.Name;
                cartItem.Price = cart.Price;
                cartItem.Image = cart.Image;
                cartItem.Quantity = 1;
                _context.Add(cartItem);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else if (IsExit(id))
            {
                var cartItemUPdate = _context.cartItems.Where(p => p.ProductId == id).FirstOrDefault();
                cartItemUPdate.Quantity++;
                _context.Update(cartItemUPdate);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["NotFound"] = "Not Found Product";
            }
            return RedirectToAction(nameof(Index));
        }
        public bool IsExit(long? id)
        {
            if (_context.cartItems.Where(p => p.ProductId == id).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }
        // POST: cartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: cartItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.cartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return View(cartItem);
        }

        // POST: cartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ProductId,Name,Image,Price,Quantity")] cartItem cartItem)
        {
            if (id != cartItem.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!cartItemExists(cartItem.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cartItem);
        }

        // GET: cartItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.cartItems
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (cartItem == null)
            {
                return NotFound();
            }


            _context.cartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: cartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        private bool cartItemExists(long id)
        {
            return _context.cartItems.Any(e => e.ProductId == id);
        }
    }
}
