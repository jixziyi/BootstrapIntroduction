using AutoMapper;
using ShoppingCart.Models;
using ShoppingCart.Services;
using ShoppingCart.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoppingCart.Controllers.Api
{
    public class CartItemsController:ApiController
    {
        private readonly CartItemService _cartItemService = new CartItemService();

        public CartItemsController()
        {
            Mapper.CreateMap<Cart, CartViewModel>();
            Mapper.CreateMap<CartItem, CartItemViewModel>();
            Mapper.CreateMap<Book, BookViewModel>();
            Mapper.CreateMap<CartItemViewModel, CartItem>();
            Mapper.CreateMap<BookViewModel, Book>();
            Mapper.CreateMap<AuthorViewModel, Author>();
            Mapper.CreateMap<CategoryViewModel, Category>();
        }

        public CartItemViewModel Post(CartItemViewModel cartItem)
        {
            var newCartItem = _cartItemService.AddToCart(
                Mapper.Map<CartItemViewModel, CartItem>(cartItem));

            return Mapper.Map<CartItem, CartItemViewModel>(newCartItem);
        }

        public CartItemViewModel Put(CartItemViewModel cartItem)
        {
            _cartItemService.UpdateCartItem(Mapper.Map<CartItemViewModel, CartItem>(cartItem));
            return cartItem;
        }

        public CartItemViewModel Delete(CartItemViewModel cartItem)
        {
            _cartItemService.DeleteCartItem(Mapper.Map<CartItemViewModel, CartItem>(cartItem));
            return cartItem;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cartItemService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}