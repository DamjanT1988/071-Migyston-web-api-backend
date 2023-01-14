using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Migyston_web_api.Models;
using System.Text.Json.Serialization;
using System.Runtime.Intrinsics.X86;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;

namespace Migyston_web_api.Services
{
    public static class ProductServices
    {
//---START FIELDS

        //initiate list
        static List<Product> Products { get; set; }
        
        //constructor
        static ProductServices()
        {
            //create new list
            Products = new List<Product> { };
            //get products from database and fill list
            LoadFile();
        }


        //---CRUD METHODS
        //get all products from list
        public static List<Product> GetAllProducts()
        {
            return Products;
        }
        //get specific product from list
        public static Product? GetProduct(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
        //add a new product to list
        public static void Add(Product product)
        {
            //set date in property
            product.Date = DateTime.Now;
            //add to list
            Products.Add(product);
            //save to database
            SaveProducts();
        }
        //update a specific product
        public static void Update(Product product)
        {
            //find product index
            var index = Products.FindIndex(p => p.Id == product.Id);
            if (index == -1)
                return;
            //update product in list
            Products[index] = product;
            //save to database
            SaveProducts();
        }
        //delete product
        public static void Delete(int id)
        {
            //get specific product
            var product = GetProduct(id);
            //check
            if (product is null)
                return;
            //remove product
            Products.Remove(product);
            //save the list to database
            SaveProducts();
        }

        //---DATABASE METHODS

        //save the list to a file
        static void SaveProducts()
        {
            //serialize the list to a variable
            string json = JsonSerializer.Serialize(Products);
            //write the JSON list to a file
            File.WriteAllText(@Directory.GetCurrentDirectory().ToString() + "/productlist.json", json);
        }

        //method to load text from JSON, create the object/list 
        static void LoadFile()
        {
            //read text from file, store in a variable as JSON-string
            string jsonString = File.ReadAllText(Directory.GetCurrentDirectory().ToString() + "/productlist.json");

            //deserialize the JSON-string
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

            //loop through the objects, create new items in the list
            
            for (int i = 0; i < obj.Count; i++)
            {
                //add new item in list by Add-method
                Products.Add(new Product { 
                    Id = obj[i].Id, 
                    Product_title = obj[i].Product_title, 
                    Ean_number = obj[i].Ean_number,
                    Product_description = obj[i].Product_description,
                    Price = obj[i].Price,
                    Amount_storage = obj[i].Amount_storage,
                    Expiration_date = obj[i].Expiration_date,
                    Category = obj[i].Category,
                    IsSwedish = obj[i].IsSwedish,   
                    Date = obj[i].Date
                });
            }
        }
    }
}