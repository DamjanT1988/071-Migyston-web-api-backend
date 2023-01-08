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
        static int nextId = 1;
        static ProductServices()
        {
            Products = new List<Product>
        {};
        }

        public static List<Product> GetAll()
        {
            LoadFile();
            return Products;
        }
        public static Product? Get(int id) => Products.FirstOrDefault(p => p.Id == id);

        public static void Add(Product product)
        {
            product.Id = nextId++;
            product.Date = DateTime.Now;
            Products.Add(product);
            SaveProducts();
        }

        public static void Delete(int id)
        {
            var product = Get(id);
            if (product is null)
                return;

            Products.Remove(product);
        }

        public static void Update(Product product)
        {
            var index = Products.FindIndex(p => p.Id == product.Id);
            if (index == -1)
                return;

            Products[index] = product;
        }
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
                    Date = obj[i].Date
                });
            }
        }
    }
}