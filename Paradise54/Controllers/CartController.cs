using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradise54.Controllers
{
    public class CartController : Controller
    {
        CartManager cm = new CartManager(new EfCartRepository());
        private readonly Context _context;
       
        public CartController(Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int tablenum)
        {
            Cart cart = _context.Carts
                .Where(x => x.TableId == tablenum && x.Active == true)
                .FirstOrDefault();

            if (cart == null)
            {
                Cart newCart = new Cart
                {
                    Status = "YENI",
                    Active = true,
                    TableId = tablenum
                };

                _context.Add(newCart);
                _context.SaveChanges();
            }
            else
            {

                List<CartItem> cartItems = await _context.CartItems
                    .Include(x => x.Cart)
                    .Where(x => x.Cart.TableId == tablenum && x.Cart.Active == true && x.Active == true)
                    .Include(x => x.Food).ToListAsync();



                if (cartItems.Count != 0)
                {
                    ViewData["ToplamFiyat"] = cartItems.Sum(x => x.Food.Price);
                    ViewData["CartID"] = cartItems[0].CartId;
                    return View(cartItems);
                }
            }

            return View();
        }
        public async Task<IActionResult> Order(int? cartId)
        {
            Cart cart = await _context.Carts
                .Where(x => x.Id == cartId)
                .FirstOrDefaultAsync();

            if (cart != null)
            {
                List<CartItem> cartItem = await _context.CartItems
                    .Include(x => x.Food)
                    .Where(x => x.CartId == cartId && x.Active == true)
                    .ToListAsync();

                for (int i = 0; i < cartItem.Count; i++)
                {
                    Food _food = cartItem[i].Food;

                    if (_food.Stock == 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            Food tmpFood = cartItem[i].Food;
                            tmpFood.Stock++;
                            _context.Update(tmpFood);
                        }

                        return RedirectToAction("Index", "Cart");
                    }

                    _food.Stock = _food.Stock - 1;
                    _context.Update(_food);
                }

                cart.Active = false;
                cart.Status = "TAMAMLANDI";
                _context.Update(cart);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Cart");
        }
    }
}
