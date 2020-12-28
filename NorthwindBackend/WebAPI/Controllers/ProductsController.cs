using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet("getBySearchProductCategory")]
        public IActionResult getBySearchProductCategory(string categoryName, string productName)
        {

            List<ProductCategory> productCategories = new List<ProductCategory>();
            var categories = _categoryService.GetList().Data.ToList();
            var products = _productService.GetList().Data.ToList();
            var result = from category in categories
                         join product in products
                         on category.CategoryId equals product.CategoryId
                         select new
                         ProductCategory
                         {
                             CategoryName = category.CategoryName,
                             ProductName = product.ProductName
                         };
            List<ProductCategory> lst = new List<ProductCategory>();

            if (string.IsNullOrEmpty(productName) && string.IsNullOrEmpty(categoryName))
            {
                return Ok(result);
            }

            if (!string.IsNullOrEmpty(productName))
            {
                lst = result.Where(x => x.CategoryName.Contains(productName)).ToList();
            }
            if (!string.IsNullOrEmpty(categoryName))
            {
                lst = result.Where(x => x.CategoryName.Contains(categoryName)).ToList();
            }
            return Ok(lst);
        }

        [HttpGet("getall")]
        ///  [Authorize(Roles = "Product.List")] ///rol  bazlı ürün listesi
        public IActionResult GetList()
        {
            var result = _productService.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getlistbycategory")]
        public IActionResult GetListByCategory(int categoryId)
        {
            var result = _productService.GetListByCategory(categoryId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Product product)
        {
            var result = _productService.Delete(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

    }
}