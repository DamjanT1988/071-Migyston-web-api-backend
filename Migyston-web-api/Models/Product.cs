using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migyston_web_api.Models
{
    public class Product
    {
        //auto
        public int Id { get; set; }
        public DateTime Date { get; set; }

        //user input
        public string? Product_title { get; set; }
        public string? Ean_number { get; set; }
        public string? Product_description { get; set; }
        public decimal? Price { get; set; }
        public int? Amount_storage { get; set; }
        public string? Expiration_date { get; set; }
        public string? Category { get; set; }
        public bool? IsSwedish { get; set; }


    }
}