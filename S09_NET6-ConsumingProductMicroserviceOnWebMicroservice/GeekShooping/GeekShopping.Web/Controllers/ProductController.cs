﻿using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //[Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var products = await _productService.FindAllProducts(token);

            return View(products);
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.CreateProduct(model, token);

                if (response is not null) { return RedirectToAction(nameof(ProductIndex)); }
            }

            return View(model);
        }

        public async Task<IActionResult> ProductUpdate(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.FindProductById(id, token);

            if (product is null) { return NotFound(); }

            return View(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductUpdate(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.UpdateProduct(model, token);

                if (response is not null) { return RedirectToAction(nameof(ProductIndex)); }
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.FindProductById(id, token);

            if (product is null) { return NotFound(); }

            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var response = await _productService.DeleteProductById(model.Id, token);

            if (response) { return RedirectToAction(nameof(ProductIndex)); }
            
            return View(model);
        }
    }
}
