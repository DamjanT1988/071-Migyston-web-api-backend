﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migyston_web_api.Models;
using Migyston_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Migyston_web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Product>> GetAll() =>
        ProductServices.GetAll();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = ProductServices.Get(id);

            if (product == null)
                return NotFound();

            return product;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(Product product)
        {
            ProductServices.Add(product);
            return CreatedAtAction(nameof(Create), new { id = product.Id }, product);
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            var existingProduct = ProductServices.Get(id);
            if (existingProduct is null)
                return NotFound();

            ProductServices.Update(product);

            return NoContent();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = ProductServices.Get(id);

            if (product is null)
                return NotFound();

            ProductServices.Delete(id);

            return NoContent();
        }
    }
}