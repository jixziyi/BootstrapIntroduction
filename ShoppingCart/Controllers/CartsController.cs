using ShoppingCart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ShoppingCart.Models;
using ShoppingCart.ViewModels;

namespace ShoppingCart.Controllers
{
    public class CartsController : Controller
    {
        private readonly CartService _cartService = new CartService();

        public CartsController()
        {
            Mapper.CreateMap<Cart, CartViewModel>();
            Mapper.CreateMap<CartItem, CartItemViewModel>();
            Mapper.CreateMap<Book, BookViewModel>();
            Mapper.CreateMap<Author, AuthorViewModel>();
            Mapper.CreateMap<Category, CartItemViewModel>();
        }

        public ActionResult Index()
        {
            var cart = _cartService.GetBySessionId(HttpContext.Session.SessionID);

            return View(Mapper.Map<Cart, CartViewModel>(cart));
        }

        [ChildActionOnly]
        public PartialViewResult Summary()
        {
            var cart = _cartService.GetBySessionId(HttpContext.Session.SessionID);

            return PartialView(Mapper.Map<Cart, CartViewModel>(cart));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cartService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}