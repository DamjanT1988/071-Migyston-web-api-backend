using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migyston_web_api.Models;
using Migyston_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Migyston_web_api.Controllers
{
    //use framework for API and controller
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        //constructor - empty
        public ProductController(){}

        // GET all action
        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts() =>
        ProductServices.GetAllProducts();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            //save product
            var product = ProductServices.GetProduct(id);
            //check if product exist
            if (product == null)
                return NotFound();
            //return product
            return product;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(Product product)
        {
            //add a product to list and database
            ProductServices.Add(product);
            //return product information
            return CreatedAtAction(nameof(Create), new { id = product.Id }, product);
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            //check if ID exist
            if (id != product.Id)
                return BadRequest();

            //check if product exist
            var existingProduct = ProductServices.GetProduct(id);
            if (existingProduct is null)
                return NotFound();

            //save new information to database
            ProductServices.Update(product);
            //return to caller server information
            return Content("Product article " + id + " is updated");
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //save the specific ID
            var product = ProductServices.GetProduct(id);
            //check if ID exist
            if (product is null)
                return NotFound();
            //delete product from list and database
            ProductServices.Delete(id);
            //return response to caller
            return Content("Product article " + id + " is deleted");
        }
    }
}