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

        static List<Product> Products { get; set; }
        
        //constructor
        static ProductServices()
        {
            Products = new List<Product> { };
            LoadFile();
        }

        public static List<Product> GetAllProducts()
        {
            return Products;
        }
        public static Product? GetProduct(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public static void Add(Product product)
        {
            int nextId = Products.Count() + 1;
            product.Id = nextId++;
            product.Date = DateTime.Now;
            Products.Add(product);
            SaveProducts();
        }

        public static void Delete(int id)
        {
            var product = GetProduct(id);
            if (product is null)
                return;

            Products.Remove(product);
            SaveProducts();
        }

        public static void Update(Product product)
        {
            var index = Products.FindIndex(p => p.Id == product.Id);
            if (index == -1)
                return;

            Products[index] = product;
            SaveProducts();
        }

        //--METHODS

        //save the list to a file
        public static void SaveProducts()
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