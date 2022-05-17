﻿using BusinessLayer.Concrete;
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
        FoodManager fm = new FoodManager(new EfFoodRepository());
        CartManager cm = new CartManager(new EfCartRepository());
        CartItemManager cim = new CartItemManager(new EfCartItemRepository());


        public IActionResult Index(int tableNum)
        {
            Cart cart = cm.GetCartListFilter(tableNum);


            if (cart == null)
            {
                Cart newCart = new Cart
                {
                    Status = "YENI",
                    Active = true,
                    TableId = tableNum
                };
                cm.TAdd(newCart);

            }
            else
            {

                List<CartItem> cartItems = cim.GetCartItemListwithFoodCartIncludeFilter(tableNum);

                if (cartItems.Count != 0)
                {
                    ViewData["ToplamFiyat"] = cartItems.Sum(x => x.Food.Price);
                    ViewData["CartID"] = cartItems[0].CartId;
                    return View(cartItems);
                }
            }






            return View();
        }
        public IActionResult Order(int cartId)
        {

            Cart cart = cm.GetById(cartId);

            if (cart != null)
            {

                List<CartItem> cartItem = cim.GetListwithFoodCartIdIncludeFilterCartItems(cartId);

                for (int i = 0; i < cartItem.Count; i++)
                {
                    Food _food = cartItem[i].Food;

                    if (_food.Stock == 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            Food tmpFood = cartItem[i].Food;
                            tmpFood.Stock++;

                            fm.TUpdate(tmpFood);
                           
                        }

                        return RedirectToAction("Index", "Cart");
                    }

                    _food.Stock = _food.Stock - 1;
                    fm.TUpdate(_food);

                }

                cart.Active = false;
                cart.Status = "TAMAMLANDI";
                cm.TUpdate(cart);

            }

            return RedirectToAction("Index", "Cart");
        }

        
    }
}
